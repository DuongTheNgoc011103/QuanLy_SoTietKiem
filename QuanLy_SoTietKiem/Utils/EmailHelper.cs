using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Mail;
using System.Text;

namespace QuanLy_SoTietKiem.Utils
{
    public class EmailHelper
    {
        private const string SmtpServer = "smtp.gmail.com"; 
        private const int SmtpPort = 587; 
        private const string SenderEmail = "duongthengoc01112003@gmail.com"; // Email của bạn
        private const string SenderPassword = "plvz fszq ewbb gbzf"; // Mật khẩu ứng dụng nếu dùng Gmail, hoặc mật khẩu email thông thường

        public static bool SendEmail(string recipientEmail, string subject, string body)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(SenderEmail, "Hệ thống Quản lý Sổ Tiết Kiệm"); // Tên hiển thị
                    mail.To.Add(recipientEmail);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true; // Cho phép nội dung HTML
                    mail.BodyEncoding = Encoding.UTF8;
                    mail.SubjectEncoding = Encoding.UTF8;

                    using (SmtpClient smtp = new SmtpClient(SmtpServer, SmtpPort))
                    {
                        smtp.Credentials = new NetworkCredential(SenderEmail, SenderPassword);
                        smtp.EnableSsl = true; // Bật SSL/TLS
                        smtp.Send(mail);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi gửi email: {ex.Message}");
                // Log lỗi chi tiết hơn nếu cần
                return false;
            }
        }
    }
}
