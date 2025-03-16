
using System.Security.Claims;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;


using SneakerAPI.Core.DTOs;
using SneakerAPI.Core.Interfaces.UserInterfaces;
using SneakerAPI.Core.Libraries;
using SneakerAPI.Core.Models;
using SneakerAPI.Core.Models.UserEntities;
using SneakerAPI.Infrastructure.Repositories;


namespace SneakerAPI.Api.Controllers.UserControllers
{   
    public class GoogleLoginRequest
{
    public required string Credential { get; set; }
}
    [ApiController]
    [Route("[Area]/api/[controller]")]
    [Area("Client")]
    public class AccountController : BaseController
    {   
        private static Dictionary<string,string> _refreshtoken= new ();
        private readonly UserManager<IdentityAccount> _accountManager;
        private readonly SignInManager<IdentityAccount> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _config;
        private readonly IMemoryCache _cache;
        private readonly IJwtService _jwtService;
        private readonly UnitOfWork _uow;

        public AccountController(UserManager<IdentityAccount> accountManager,
                                 SignInManager<IdentityAccount> signInManager,
                                 IEmailSender emailSender,
                                 IConfiguration config,
                                 IMemoryCache cache,
                                 IJwtService jwtService,
                                 UnitOfWork uow):base (uow)
        {
            _accountManager = accountManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _config = config;
            _cache = cache;
            _jwtService = jwtService;
            _uow = uow;
        }
        private object CurrentUser()
        {   
            try
            {
                
            var account = HttpContext.User;

            if (account == null || !account.Identity.IsAuthenticated)
            {
                return Unauthorized("User is not authenticated.");
            }

            var sub = account.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email=account.FindFirst(ClaimTypes.Email)?.Value;
            var roles = account.Claims
                .Where(c => c.Type == ClaimTypes.Role || c.Type.EndsWith("role"))
                .Select(c => c.Value)
                .ToList();
            return new CurrentUser
            {
                AccountId = sub,
                Email = email,
                Roles = roles,
            };
            }
        
            catch (System.Exception)
            {
                
                throw;
            }
        }
        [Authorize(Roles = RolesName.Customer)]
        [HttpGet("current-user")]
        public IActionResult GetCurrentUser()
        {
            
            return Ok(CurrentUser());
        }          
    
            [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] TokenResponse model)
    {   
        try
        {
 
        var username = _refreshtoken.FirstOrDefault(predicate: x => x.Value == model.RefreshToken).Key;
        if (string.IsNullOrEmpty(username))
            return Unauthorized("Refresh Token is not valid!");
        var account=await _accountManager.FindByEmailAsync(username);
        var roles=await _accountManager.GetRolesAsync(account);
        var newTokens = (TokenResponse)_jwtService.GenerateJwtToken(username,roles);
        
        // Cập nhật refresh token mới
        _refreshtoken[username] = newTokens.RefreshToken;

        return Ok(newTokens);
                   
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }

    private bool AutoCreateInfo(int account_id){
        return _uow.CustomerInfo.Add(new CustomerInfo{
                            CustomerInfo__AccountId=account_id,
                            CustomerInfo__Avatar="default.jpg"
                        });
    }
        [HttpPost("set-password")]
        public async Task<IActionResult> SetPassword(ChangePasswordDto model)
        {
            try
            {
            var currentAccount= (CurrentUser)CurrentUser();;
                           
            if(model.ConfirmNewPassword != model.NewPassword)
                return BadRequest("Your new password and confirm password do not match.");

            var account = await _accountManager.FindByEmailAsync(currentAccount.Email);
            if (account == null)
                return BadRequest("Anomalous behavior was detected"); 

            account.PasswordHash = _accountManager.PasswordHasher.HashPassword(account, model.NewPassword);
            var result=await _accountManager.UpdateAsync(account);
            // var result = await _accountManager.ChangePasswordAsync(account, model.Password, model.NewPassword);
    
            if (result.Succeeded){
                var content="You just changed your password at "+ DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                await _emailSender.SendEmailAsync(currentAccount.Email, "Password changed",
                EmailTemplateHtml.RenderEmailNotificationBody(currentAccount.Email,"Security Warning!", content));
                return Ok("Password changed successfully.");
            }
            return BadRequest(result.Errors);
                    
            }
            catch (System.Exception)
            {
                
                throw new Exception("An error has occurred while resetting password");
            }
        }
        [HttpPost("signin-google")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Credential))
                    return BadRequest("Missing Google Token");

                var payload = await GoogleJsonWebSignature.ValidateAsync(request.Credential, new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { "429739552674-jgb94p4nggngssiksimcgqith8evpo98.apps.googleusercontent.com" }
                });

                var account = await _accountManager.FindByEmailAsync(payload.Email);

                if (account == null)
                {  
                    var newAccount=new IdentityAccount{
                        Email=payload.Email,
                        UserName=payload.Email,
                        EmailConfirmed=true
                    };
                    var result=await _accountManager.CreateAsync(newAccount);
                    if(result.Succeeded)
                    {
                        await _accountManager.AddToRoleAsync(newAccount, RolesName.Customer);
                        //Auto create customer info
                        AutoCreateInfo(newAccount.Id);
                        await _signInManager.SignInAsync(newAccount, isPersistent: true);

                        await _emailSender.SendEmailAsync(newAccount.Email, "Notification",
                            EmailTemplateHtml.RenderEmailNotificationBody(newAccount.Email,"Login Notice", "You have just successfully logged into the Sneaker Luxury Store app."));
                        return Ok(
                                _jwtService.GenerateJwtToken(newAccount.UserName, new List<string> { RolesName.Customer })
                               );
                    }
                }

                // Đăng nhập user vào hệ thống
                var roles=await _accountManager.GetRolesAsync(account);
                if(!roles.Contains(RolesName.Customer)){
                    var result=await _accountManager.AddToRoleAsync(account,RolesName.Customer);
                    if(result.Succeeded){
                        //Auto create customer info
                        AutoCreateInfo(account.Id);
                    }
                }
                await _signInManager.SignInAsync(account, isPersistent: true);
                return Ok(_jwtService.GenerateJwtToken(account.UserName,roles));
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Google token is not valid!", message = ex.Message });
            }
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            try
            {
            if(!ModelState.IsValid){
                return BadRequest("Invalid your input");
            } 
            var accountExist = await _accountManager.FindByEmailAsync(model.Email);
            if (accountExist != null)
            {   
                var roles=await _accountManager.GetRolesAsync(accountExist);
                if(roles.Contains(RolesName.Customer))
                    return BadRequest("Email already exists. You are granted access as a customer");

                if(!roles.Contains(RolesName.Customer)){
                    var rs=await _accountManager.AddToRoleAsync(accountExist,RolesName.Customer);
                    if(rs.Succeeded){
                        //Auto create customer info
                        AutoCreateInfo(accountExist.Id);
                    }
                }
                return Ok("Email already exists. You are granted access as a customer");
            }
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
                await _accountManager.AddToRoleAsync(account, RolesName.Customer);
                //Auto create customer info
                AutoCreateInfo(account.Id);
                // Sinh OTP và lưu vào MemoryCache (hết hạn sau 5 phút)
                var otpCode = HandleString.GenerateVerifyCode();
                _cache.Set(model.Email, otpCode, TimeSpan.FromMinutes(5));

                // Gửi OTP qua email
                await _emailSender.SendEmailAsync(model.Email, "Confirm email",
                    EmailTemplateHtml.RenderEmailRegisterBody(model.Email, otpCode));

                return Ok(new {result=true, msg="Register successfully. Please check your email for verification code."});
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
                var otpCode = HandleString.GenerateVerifyCode();
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
            if(!roles[0].Contains(RolesName.Customer))
                return BadRequest("User does not have access");

            if (!await _accountManager.IsEmailConfirmedAsync(account))
                return BadRequest("Please confirm email before logging in.");

            var result = await _signInManager.PasswordSignInAsync(account, model.Password, false, false);
            if (result.Succeeded)
            {   
                
                return  Ok(_jwtService.GenerateJwtToken(account.UserName,roles));
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
            var currentAccount= (CurrentUser)CurrentUser();;
            if(model.ConfirmNewPassword != model.NewPassword)
                return BadRequest("Your new password and confirm password do not match.");

            var account = await _accountManager.FindByEmailAsync(currentAccount.Email);
            if (account == null)
                return Ok("Wrong account name or password"); // Không tiết lộ thông tin người dùng không tồn tại

            var result = await _accountManager.ChangePasswordAsync(account, model.Password, model.NewPassword);
    
            if (result.Succeeded){
                var content="You just changed your password at"+ DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                await _emailSender.SendEmailAsync(currentAccount.Email, "Password changed",
                EmailTemplateHtml.RenderEmailNotificationBody(currentAccount.Email,"Security Warning!", content));
                return Ok("Password changed successfully.");
            }
            return BadRequest("Wrong account name or password");
                    
            }
            catch (System.Exception)
            {
                
                throw new Exception("An error has occurred while resetting password");
            }
        }
    }
}
