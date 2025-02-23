
using System.Net;
using System.Net.Mail;
using SneakerAPI.Core.Interfaces;
namespace SneakerAPI.Infrastructure.Repositories;
public class EmailSender : IEmailSender
{
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        // Sử dụng SMTP hoặc dịch vụ email khác (ví dụ: SendGrid)
        // Ví dụ sử dụng SmtpClient (cần thêm các cấu hình)
        using (var client = new SmtpClient("http://localhost:5025"))
        {
            client.Credentials = new NetworkCredential("0965972715a@gmail.com", "Txhoang11!");
            var mailMessage = new MailMessage("0368154633a@gmail.com", email, subject, htmlMessage)
            {
                IsBodyHtml = true
            };
            await client.SendMailAsync(mailMessage);
        }
    }
}