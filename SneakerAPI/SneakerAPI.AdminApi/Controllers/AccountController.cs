using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using SneakerAPI.Core.DTOs;
using SneakerAPI.Core.Models;

namespace SneakerAPI.AdminApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityAccount> _accountManager;
        private readonly SignInManager<IdentityAccount> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<IdentityAccount> accountManager,
                                 SignInManager<IdentityAccount> signInManager,
                                 IEmailSender emailSender)
        {
            _accountManager = accountManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        // Đăng ký
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model,string role="Staff")
        {   


            if(model.Password!=model.PasswordComfirm){
                return BadRequest("Password and password confirm was not match");
            }
            var account = new IdentityAccount { UserName = model.Email, Email = model.Email };
            var result = await _accountManager.CreateAsync(account, model.Password);
            if (result.Succeeded)
            {   
                await _accountManager.AddToRoleAsync(account, role);


                // Sinh token xác thực email
                var token = await _accountManager.GenerateEmailConfirmationTokenAsync(account);
                // Tạo link xác thực (lưu ý: cần mã hóa token nếu cần)
                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { userId = account.Id, token }, Request.Scheme);
                
                // Gửi email xác thực
                await _emailSender.SendEmailAsync(model.Email, "Xác nhận Email", 
                    $"Vui lòng nhấp vào link sau để xác nhận email: <a href='{confirmationLink}'>Xác nhận Email</a>");
                
                return Ok("Đăng ký thành công. Vui lòng kiểm tra email để xác thực tài khoản.");
            }
            return BadRequest(result.Errors);
        }

        // Xác nhận Email
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string accountId, string token)
        {
            if (accountId == null || token == null)
                return BadRequest("Yêu cầu xác nhận không hợp lệ.");

            var account = await _accountManager.FindByIdAsync(accountId);
            if (account == null)
                return BadRequest("Không tìm thấy người dùng.");

            var result = await _accountManager.ConfirmEmailAsync(account, token);
            if (result.Succeeded)
                return Ok("Xác nhận email thành công.");
            return BadRequest("Có lỗi trong quá trình xác nhận email.");
        }

        // Đăng nhập
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var account = await _accountManager.FindByEmailAsync(model.Email);
            if (account == null)
                return BadRequest("Người dùng không tồn tại.");

            if (!await _accountManager.IsEmailConfirmedAsync(account))
                return BadRequest("Vui lòng xác nhận email trước khi đăng nhập.");

            var result = await _signInManager.PasswordSignInAsync(account, model.Password, false, false);
            if (result.Succeeded)
            {
                // Ở đây bạn có thể tạo JWT token hoặc session tùy ý
                return Ok("Đăng nhập thành công.");
            }
            return BadRequest("Thông tin đăng nhập không hợp lệ.");
        }

        // Quên mật khẩu
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto model)
        {
            var account = await _accountManager.FindByEmailAsync(model.Email);
            if (account == null || !await _accountManager.IsEmailConfirmedAsync(account))
            {
                // Không tiết lộ thông tin chi tiết về việc người dùng không tồn tại
                return Ok("Vui lòng kiểm tra email để thực hiện đặt lại mật khẩu.");
            }

            // Sinh token reset mật khẩu
            var token = await _accountManager.GeneratePasswordResetTokenAsync(account);
            var resetLink = Url.Action(nameof(ResetPassword), "Account", new { token, email = model.Email }, Request.Scheme);

            await _emailSender.SendEmailAsync(model.Email, "Đặt lại mật khẩu", 
                $"Đặt lại mật khẩu bằng cách nhấp vào link sau: <a href='{resetLink}'>Đặt lại mật khẩu</a>");
            
            return Ok("Vui lòng kiểm tra email để đặt lại mật khẩu.");
        }

        // Đặt lại mật khẩu
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            var account = await _accountManager.FindByEmailAsync(model.Email);
            if (account == null)
                return Ok("Đặt lại mật khẩu thành công."); // Không tiết lộ thông tin người dùng không tồn tại

            var result = await _accountManager.ResetPasswordAsync(account, model.Token, model.NewPassword);
            if (result.Succeeded)
                return Ok("Mật khẩu đã được thay đổi thành công.");
            return BadRequest(result.Errors);
        }
    }
}
