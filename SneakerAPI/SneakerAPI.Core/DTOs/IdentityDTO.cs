namespace SneakerAPI.Core.DTOs;
public enum Roles{
    Admin,
    Staff,
    Customer,
    Manager
}
public static class RolesName
{
    public const string Admin = "Admin"; // ✅ Là hằng số
    public const string Staff = "Staff"; // ✅ Là hằng số
    public const string Customer = "Customer"; // ✅ Là hằng số
    public const string Manager = "Manager"; // ✅ Là hằng số
}
public class RegisterDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string PasswordComfirm { get; set; }
}

public class LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class ForgotPasswordDto
{
    public string Email { get; set; }
}

public class ChangePasswordDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmNewPassword { get; set; }
}
