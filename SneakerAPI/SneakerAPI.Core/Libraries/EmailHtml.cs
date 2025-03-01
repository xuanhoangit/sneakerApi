namespace SneakerAPI.Core.Libraries;
public static class EmailTemplateHtml
{
public static string RenderEmailBody(string username,string OPT){
return  $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Xác nhận Email</title>
</head>
<body style='margin: 0; padding: 0; background-color: #f4f4f4;'>
    <table role='presentation' cellspacing='0' cellpadding='0' border='0' width='100%' bgcolor='#f4f4f4'>
        <tr>
            <td align='center'>
                <table role='presentation' cellspacing='0' cellpadding='0' border='0' width='600' style='background: #ffffff; margin: 20px auto; padding: 20px; border-radius: 8px;'>
                    <!-- Header -->
                    <tr>
                        <td align='center' style='padding: 20px 0;'>
                            <h1 style='margin: 0; font-family: Arial, sans-serif; color: #333;'>Chào mừng bạn!</h1>
                        </td>
                    </tr>
                    <!-- Body -->
                    <tr>
                        <td style='padding: 20px; font-family: Arial, sans-serif; color: #555;'>
                            <p>Chào <strong>{username}</strong>,</p>
                            <p>Cảm ơn bạn đã đăng ký tài khoản. Để hoàn tất quá trình đăng ký, vui lòng nhấp vào nút dưới đây để xác nhận email:</p>
                            <p style='text-align: center;'>
                                Mã OTP của bạn là: <strong>{OPT}</strong>
                            </p>
                            <p>Nếu bạn không thực hiện yêu cầu này, vui lòng bỏ qua email này.</p>
                        </td>
                    </tr>
                    <!-- Footer -->
                    <tr>
                        <td align='center' style='padding: 20px; font-size: 14px; color: #777; font-family: Arial, sans-serif;'>
                            <p>&copy; 2025 Company Name. All rights reserved.</p>
                            <p><a href='#' style='color: #007bff; text-decoration: none;'>Chính sách bảo mật</a> | <a href='#' style='color: #007bff; text-decoration: none;'>Điều khoản dịch vụ</a></p>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>";
} 

}