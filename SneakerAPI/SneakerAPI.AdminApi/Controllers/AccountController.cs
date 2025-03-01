using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using SneakerAPI.Core.Dtos;
using SneakerAPI.Core.DTOs;
using SneakerAPI.Core.Libraries;
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
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model, string role = "Staff")
        {
            if (model.Password != model.PasswordComfirm)
            {
                return BadRequest("Password and password confirm do not match");
            }

            var account = new IdentityAccount
            {
                UserName = model.Email,
                Email = model.Email,
                // IsEmailConfirmed = false
            };

            var result = await _accountManager.CreateAsync(account, model.Password);
            if (result.Succeeded)
            {
                await _accountManager.AddToRoleAsync(account, role);

                // Sinh OTP và lưu vào MemoryCache (hết hạn sau 5 phút)
                var otpCode = HandleString.GenerateVerifyCode(4);
                _cache.Set(model.Email, otpCode, TimeSpan.FromMinutes(5));

                // Gửi OTP qua email
                await _emailSender.SendEmailAsync(model.Email, "Xác nhận Email",
                    EmailTemplateHtml.RenderEmailBody(model.Email, otpCode));

                return Ok("Đăng ký thành công. Vui lòng kiểm tra email để nhập mã xác thực.");
            }
            return BadRequest(result.Errors);
        }
        [HttpPost("resend-email-confirmation")]
        public async Task<IActionResult> ResendEmailConfirmation(string email)
        {
            var account = await _accountManager.FindByEmailAsync(email);
            if (account == null)
            {
                return BadRequest("Email không tồn tại.");
            }

            if (await _accountManager.IsEmailConfirmedAsync(account))
            {
                return BadRequest("Email đã được xác thực trước đó.");
            }

            if (!_cache.TryGetValue(email, out string storedOtp))
            {   
                // Sinh OTP và lưu vào MemoryCache (hết hạn sau 5 phút)
                var otpCode = HandleString.GenerateVerifyCode(4);
                _cache.Set(email, otpCode, TimeSpan.FromMinutes(5));

                // Gửi OTP qua email
                await _emailSender.SendEmailAsync(email, "Xác nhận Email",
                EmailTemplateHtml.RenderEmailBody(email, otpCode));
                return Ok("Vui lòng kiểm tra email để nhập mã xác thực.");
            }


            return BadRequest("Hãy thử lại sau vài phút.");
        }
        [HttpPost("confirm-email")]
        public IActionResult ConfirmEmail(ConfirmEmailDto model)
        {
            if (!_cache.TryGetValue(model.Email, out string storedOtp))
            {
                return BadRequest("Mã OTP không hợp lệ hoặc đã hết hạn.");
            }

            if (storedOtp != model.OtpCode)
            {
                return BadRequest("Mã OTP không chính xác.");
            }

            var account = _accountManager.FindByEmailAsync(model.Email).Result;
            if (account == null)
            {
                return BadRequest("Email không tồn tại.");
            }

            if (account.EmailConfirmed)
            {
                return BadRequest("Email đã được xác thực trước đó.");
            }

            // Cập nhật trạng thái xác thực email
            account.EmailConfirmed = true;
            _accountManager.UpdateAsync(account);

            // Xóa OTP khỏi cache sau khi xác nhận thành công
            _cache.Remove(model.Email);

            return Ok("Email đã được xác thực thành công!");
        }
        // Đăng nhập
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var account = await _accountManager.FindByEmailAsync(model.Email);

            if (account == null)
                return BadRequest("Người dùng không tồn tại.");

            if(model.Password != account.PasswordHash)
                return Unauthorized("Sai mật khẩu");

            var roles=await _accountManager.GetRolesAsync(account);
            if(!roles.Any())
                return BadRequest("Người dùng không có quyền truy cập");

            if (!await _accountManager.IsEmailConfirmedAsync(account))
                return BadRequest("Vui lòng xác nhận email trước khi đăng nhập.");

            var result = await _signInManager.PasswordSignInAsync(account, model.Password, false, false);
            if (result.Succeeded)
            {   
                return Ok(new {
                        token=GenerateJwtToken(account.UserName,roles[0]),
                        Message = "Đăng nhập thành công." 
                        });
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
