using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
// using SneakerAPI.Core.Dtos;
using SneakerAPI.Core.DTOs;
using SneakerAPI.Core.Libraries;
using SneakerAPI.Core.Models;


namespace SneakerAPI.AdminApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Area("Dashboard")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityAccount> _accountManager;
        private readonly SignInManager<IdentityAccount> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _config;
        private readonly IMemoryCache _cache;


        public AccountController(UserManager<IdentityAccount> accountManager,
                                 SignInManager<IdentityAccount> signInManager,
                                 IEmailSender emailSender,
                                 IConfiguration config,
                                 IMemoryCache cache)
        {
            _accountManager = accountManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _config = config;
            _cache = cache;
        }

        private string GenerateJwtToken(string username, string role)
        {
            try
            {
                
            var jwtSettings = _config.GetSection("JWT");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpireMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
             }
            catch (System.Exception)
            {
                
                throw new Exception("An error occured while generating token");
            }
        }
        
        
        [HttpPut("unlock-account/{email}")]
        [Authorize(Roles=RolesName.Admin)]
        public async Task<IActionResult> UnlockAccount(string email)
        {
            try
            {
                var account =await  _accountManager.FindByEmailAsync(email);
                if (account == null)
                    return BadRequest("Account does not exist.");

                account.LockoutEnabled = false;
                account.LockoutEnd = null;
                await _accountManager.UpdateAsync(account);
                return Ok("Account has been unlocked.");
            }
            catch (System.Exception)
            {
                
                throw new Exception("An error has occurred while blocking account");
            }
        }
       
        [HttpPut("block-account/{email}")]
        [Authorize(Roles=RolesName.Admin)]
        public async Task<IActionResult> BlockAccount(string email)
        {
            try
            {
                var account =await  _accountManager.FindByEmailAsync(email);
                if (account == null)
                    return BadRequest("Account does not exist.");

                account.LockoutEnabled = true;
                account.LockoutEnd = DateTimeOffset.MaxValue;
                await _accountManager.UpdateAsync(account);
                return Ok("Account has been blocked.");
            }
            catch (System.Exception)
            {
                
                throw new Exception("An error has occurred while blocking account");
            }
        }
       
        [Authorize(Roles=RolesName.Admin)]
        [HttpGet("get-all-accounts/{role}")]
        public async Task<IActionResult> GetAccountsByRole(string role){
            try
            {
                var accounts= (await _accountManager.GetUsersInRoleAsync(role)).Take(10);
                if(accounts==null)
                    return BadRequest("Have not an account!");
                return Ok(accounts);
            }
            catch (System.Exception)
            {
                
                throw new Exception("An error has occurred while getting account");
            }
            
        }
        [Authorize(Roles=RolesName.Admin)]
        [HttpGet("get-account-by-email/{email}")]
        public async Task<IActionResult> GetAccount(string email){
            try
            {
                var account=await  _accountManager.FindByEmailAsync(email);
                if(account==null)
                    return BadRequest("Account not found");
                return Ok(account);
            }
            catch (System.Exception)
            {
                
                throw new Exception("An error has occurred while getting account");
            }
            
        }
        [Authorize(Roles=RolesName.Admin)]
        [HttpGet("get-account-by-id/{accountId}")]
        public async Task<IActionResult> GetAccount(int accountId){
            try
            {
                var account=await  _accountManager.FindByIdAsync(accountId.ToString());
                if(account==null)
                    return BadRequest("Account not found");
                return Ok(account);
            }
            catch (System.Exception)
            {
                
                throw new Exception("An error has occurred while getting account");
            }
            
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model, string role = RolesName.Staff)
        {
            try
            {
                
            if (model.Password != model.PasswordComfirm)
            {
                return BadRequest("Password and password confirm do not match");
            }

            var account = new IdentityAccount
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = false
            };

            var result = await _accountManager.CreateAsync(account, model.Password);
            if (result.Succeeded)
            {
                await _accountManager.AddToRoleAsync(account, role);

                // Sinh OTP và lưu vào MemoryCache (hết hạn sau 5 phút)
                var otpCode = HandleString.GenerateVerifyCode(4);
                _cache.Set(model.Email, otpCode, TimeSpan.FromMinutes(5));

                // Gửi OTP qua email
                await _emailSender.SendEmailAsync(model.Email, "Confirm email",
                    EmailTemplateHtml.RenderEmailRegisterBody(model.Email, otpCode));

                return Ok("Register successfully. Please check your email for verification code.");
            }
            return BadRequest(result.Errors);
             }
            catch (System.Exception)
            {
                
                throw new Exception("An error has occurred while registering");
            }
        }
        [HttpPost("resend-email-confirmation/{email}")]
        public async Task<IActionResult> ResendEmailConfirmation(string email)
        {   
            try
            {
           
            var account = await _accountManager.FindByEmailAsync(email);
            if (account == null )
            {
                return BadRequest("Email does not exist.");
            }

            if (await _accountManager.IsEmailConfirmedAsync(account))
            {
                return BadRequest("Your email has been previously verified.");
            }

            if (!_cache.TryGetValue(email, out string storedOtp))
            {   
                // Sinh OTP và lưu vào MemoryCache (hết hạn sau 5 phút)
                var otpCode = HandleString.GenerateVerifyCode(4);
                _cache.Set(email, otpCode, TimeSpan.FromMinutes(5));

                // Gửi OTP qua email
                await _emailSender.SendEmailAsync(email, "Confirm email",
                EmailTemplateHtml.RenderEmailRegisterBody(email, otpCode));
                return Ok("Please check your email for verification code.");
            }


            return BadRequest("Please try again in a few minutes.");
                 
            }
            catch (System.Exception)
            {
                
                throw new Exception("An error has occurred while resending email confirmation");
            }
        }
        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailDto model)
        {   
            try
            {
           
            if (!_cache.TryGetValue(model.Email, out string storedOtp))
            {
                return BadRequest("Mã OTP không hợp lệ hoặc đã hết hạn.");
            }

            if (storedOtp != model.OtpCode)
            {
                return BadRequest("OTP code is incorrect.");
            }

            var account = _accountManager.FindByEmailAsync(model.Email).Result;
            if (account == null)
            {
                return BadRequest("Email does not exist.");
            }

            if (account.EmailConfirmed)
            {
                return BadRequest("Email has been previously verified.");
            }

            // Cập nhật trạng thái xác thực email
            account.EmailConfirmed = true;
            var a=await _accountManager.UpdateAsync(account);

            // Xóa OTP khỏi cache sau khi xác nhận thành công
            _cache.Remove(model.Email);

            return Ok("Email has been successfully verified!");
                 
            }
            catch (System.Exception)
            {
                
                throw new Exception("An error has occurred while confirming email");
            }
        }
        // Đăng nhập
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            try
            {
       
            var account = await _accountManager.FindByEmailAsync(model.Email);

            if (account == null)
                return BadRequest("Account does not exist.");

          
            var roles=await _accountManager.GetRolesAsync(account);
            if(!roles.Any())
                return BadRequest("User does not have access");

            if (!await _accountManager.IsEmailConfirmedAsync(account))
                return BadRequest("Please confirm email before logging in.");

            var result = await _signInManager.PasswordSignInAsync(account, model.Password, false, false);
            if (result.Succeeded)
            {   
                return Ok(new {
                        token=GenerateJwtToken(account.UserName,roles[0]),
                        Message = "Login successful." 
                        });
            }
            return BadRequest("Invalid login information.");
                     
            }
            catch (System.Exception)
            {
                
                throw new Exception("An error has occurred while logging in");
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto model)
        {
            try
            {
         
            var account = await _accountManager.FindByEmailAsync(model.Email);
            var role= await _accountManager.GetRolesAsync(account);
            // if(!role.Contains(RolesName.Customer))
            //     return BadRequest("You do not have access");

            if (account == null)
            {
                // Không tiết lộ thông tin tài khoản có tồn tại hay không
                return Ok("Password has been emailed to youuu");
            }
            var password = HandleString.GenerateRandomString(16);
            // cập nhật account với mật khẩu mới
            account.PasswordHash = _accountManager.PasswordHasher.HashPassword(account, password);
            await _accountManager.UpdateAsync(account);

            await _emailSender.SendEmailAsync(model.Email, "Reset password",
                EmailTemplateHtml.RenderEmailForgotPasswordBody(model.Email, password));
            return Ok("Password has been emailed to you");
                   
            }
            catch (System.Exception)
            {
                
                throw new Exception("An error has occurred while sending email");
            }
        }

        // Đặt lại mật khẩu
        [HttpPost("change-password")]
        public async Task<IActionResult> ResetPassword(ChangePasswordDto model)
        {
            try
            {
        
            if(model.ConfirmNewPassword != model.NewPassword)
                return BadRequest("Your new password and confirm password do not match.");

            var account = await _accountManager.FindByEmailAsync(model.Email);
            if (account == null)
                return Ok("Wrong account name or password"); // Không tiết lộ thông tin người dùng không tồn tại

            var result = await _accountManager.ChangePasswordAsync(account, model.Password, model.NewPassword);
    
            if (result.Succeeded){
                var content="You just changed your password at"+ DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                await _emailSender.SendEmailAsync(model.Email, "Password changed",
                EmailTemplateHtml.RenderEmailNotificationBody(model.Email, content));
                return Ok("Password changed successfully.");
            }
            return BadRequest(result.Errors);
                    
            }
            catch (System.Exception)
            {
                
                throw new Exception("An error has occurred while resetting password");
            }
        }
    }
}
