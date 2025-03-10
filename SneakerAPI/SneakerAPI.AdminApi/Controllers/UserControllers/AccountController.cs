using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

using SneakerAPI.Core.DTOs;
using SneakerAPI.Core.Interfaces.UserInterfaces;
using SneakerAPI.Core.Libraries;
using SneakerAPI.Core.Models;


namespace SneakerAPI.AdminApi.Controllers.UserController
{   
    [ApiController]
    [Route("[area]/api/[controller]")]
    [Area("Dashboard")]
    [Authorize(Roles=RolesName.Manager)]
    public class ManagerController : ControllerBase {
        private readonly UserManager<IdentityAccount> _accountManager;
        private readonly IEmailSender _emailSender;
        private readonly IMemoryCache _cache;


        public ManagerController(UserManager<IdentityAccount> accountManager,
                                 IEmailSender emailSender,
                                 IMemoryCache cache)
        {
            _accountManager = accountManager;
            _emailSender = emailSender;
            _cache = cache;
        }
        [HttpGet("get-staffs")]
        public async Task<IActionResult> GetStaffAccount(int take){
            try
            {
                var accounts= (await _accountManager.GetUsersInRoleAsync(RolesName.Staff)).Take(take);
                if(accounts==null)
                    return BadRequest("Have not an account!");
                return Ok(accounts);
            }
            catch (System.Exception)
            {
                
                throw new Exception("An error has occurred while getting account");
            }
            
        }
        [Authorize(Roles=RolesName.Manager)]
        [HttpPost("create-staff")]
        public async Task<IActionResult> Register(RegisterDto model )
        {
            try
            {
                
            if (model.Password != model.PasswordComfirm)
            {
                return BadRequest("Password and password confirm do not match");
            }
                        var account=await _accountManager.FindByEmailAsync(model.Email);
            if(account!=null)
            {
                var isInRole=await _accountManager.IsInRoleAsync(account,RolesName.Staff);
                if(isInRole)
                    return BadRequest("Account has access");
                await _accountManager.AddToRoleAsync(account,RolesName.Staff);
                return Ok("Granted permission to Manager successfully");
            }
            var newAccount = new IdentityAccount
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = false
            };

            var result = await _accountManager.CreateAsync(newAccount, model.Password);
            if (result.Succeeded)
            {   
                await _accountManager.AddToRoleAsync(newAccount, RolesName.Staff);

                // Sinh OTP và lưu vào MemoryCache (hết hạn sau 5 phút)
                var otpCode = HandleString.GenerateVerifyCode();
                _cache.Set(model.Email, otpCode, TimeSpan.FromMinutes(5));

                // Gửi OTP qua email
                await _emailSender.SendEmailAsync(model.Email, "Confirm email",
                    EmailTemplateHtml.RenderEmailRegisterBody(model.Email, otpCode));

                return Ok("Create account successfully. Please check your email for verification code.");
            }
            return BadRequest(result.Errors);
             }
            catch (System.Exception)
            {
                
                throw new Exception("An error has occurred while registering");
            }
        }
    }
    //--------------------------------------------------------------------------------------------------------------
    [ApiController]
    [Route("[area]/api/[controller]")]
    [Area("Dashboard")]
    [Authorize(Roles=RolesName.Admin)]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<IdentityAccount> _accountManager;
        private readonly IEmailSender _emailSender;
        private readonly IMemoryCache _cache;


        public AdminController(UserManager<IdentityAccount> accountManager,
                                 IEmailSender emailSender,
                                 IMemoryCache cache)
        {
            _accountManager = accountManager;
            _emailSender = emailSender;
            _cache = cache;
        }

        
        [HttpPut("unlock-account/{email}")]
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
       
 
        [HttpGet("get-accounts-by-role/{role}")]
        public async Task<IActionResult> GetAccountsByRole(string role,int take){
            try
            {
                var accounts= (await _accountManager.GetUsersInRoleAsync(role)).Take(take);
                if(accounts==null)
                    return BadRequest("Have not an account!");
                return Ok(accounts);
            }
            catch (System.Exception)
            {
                
                throw new Exception("An error has occurred while getting account");
            }
            
        }

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

        [HttpPost("create-manager")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            try
            {
                
            if (model.Password != model.PasswordComfirm)
            {
                return BadRequest("Password and password confirm do not match");
            }
            var account=await _accountManager.FindByEmailAsync(model.Email);
            if(account!=null)
            {
                var isInRole=await _accountManager.IsInRoleAsync(account,RolesName.Manager);
                if(isInRole)
                    return BadRequest("Account has access");
                await _accountManager.AddToRoleAsync(account,RolesName.Manager);
                return Ok("Granted permission to Manager successfully");
            }

            var newAccount = new IdentityAccount
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = false
            };

            var result = await _accountManager.CreateAsync(newAccount, model.Password);
            if (result.Succeeded)
            {   
                await _accountManager.AddToRoleAsync(newAccount, RolesName.Manager);

                // Sinh OTP và lưu vào MemoryCache (hết hạn sau 5 phút)
                var otpCode = HandleString.GenerateVerifyCode();
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
    }
    [ApiController]
    [Route("[area]/api/[controller]")]
    [Area("Dashboard")]
    public class AccountController : ControllerBase{

        private static Dictionary<string,string> _refreshtoken= new ();
        private readonly UserManager<IdentityAccount> _accountManager;
        private readonly SignInManager<IdentityAccount> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IMemoryCache _cache;
        private readonly IJwtService _jwtService;

        public AccountController(UserManager<IdentityAccount> accountManager,
                                 SignInManager<IdentityAccount> signInManager,
                                 IEmailSender emailSender,
                                 IMemoryCache cache,
                                 IJwtService jwtService)
        {
            _accountManager = accountManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _cache = cache;
            _jwtService = jwtService;
        }
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] TokenResponse model)
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
        // AcccountId = accountID,
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
[Authorize]
[HttpGet("current-user")]
public IActionResult GetCurrentUser()
{
    
    return Ok(CurrentUser());
}   

        [HttpPost("resend-email-confirmation")]
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
            if (account == null || !await _accountManager.CheckPasswordAsync(account, model.Password))
                return Unauthorized("Invalid email or password");
          
            var roles=await _accountManager.GetRolesAsync(account);
            if(!roles.Any())
                return Unauthorized("Account does not have access");

            if (!await _accountManager.IsEmailConfirmedAsync(account))
                return Unauthorized("Please confirm email before logging in.");

            var result = await _signInManager.PasswordSignInAsync(account, model.Password, false, false);
            if (result.Succeeded)
            {   
                var token = (TokenResponse)_jwtService.GenerateJwtToken(model.Email,roles);
                _refreshtoken[account.Email]=token.RefreshToken;
                return Ok(token);
            }
            return Unauthorized("Invalid login information.");
                     
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
            if(role.Count()==1 && role[0]==RolesName.Customer)
                return BadRequest("You do not have access");

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
        [Authorize(Roles=$"{RolesName.Admin},{RolesName.Manager},{RolesName.Staff}")]
        [HttpPost("change-password")]
        public async Task<IActionResult> ResetPassword(ChangePasswordDto model)
        {
            try
            {
            CurrentUser currentAccount=(CurrentUser)CurrentUser();

            if(model.ConfirmNewPassword != model.NewPassword)
                return BadRequest("Your new password and confirm password do not match.");

            var account = await _accountManager.FindByEmailAsync(currentAccount.Email);


            if(currentAccount.Roles.Count()==1 && currentAccount.Roles[0]==RolesName.Customer)
                return BadRequest("You do not have access");

            if (account == null)
                
                return BadRequest("Anomalous behavior was detected"); // Không tiết lộ thông tin người dùng không tồn tại

            var result = await _accountManager.ChangePasswordAsync(account, model.Password, model.NewPassword);
    
            if (result.Succeeded){
                var content="You just changed your password at"+ DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                await _emailSender.SendEmailAsync(currentAccount.Email, "Password changed",
                EmailTemplateHtml.RenderEmailNotificationBody(currentAccount.Email,"Security Warning!", content));
                return Ok("Password changed successfully.");
            }
            return Ok("Wrong account name or password");
                    
            }
            catch (System.Exception)
            {
                
                throw new Exception("An error has occurred while resetting password");
            }
        }
    }
}
