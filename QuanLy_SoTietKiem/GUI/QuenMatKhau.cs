using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.Mail;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

using QuanLy_SoTietKiem.DAL;
using QuanLy_SoTietKiem.Utils;

namespace QuanLy_SoTietKiem.GUI
{
    public partial class QuenMatKhau: Form
    {
        public QuenMatKhau()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DangNhap login = new DangNhap();
            login.Show();
            this.Close();
        }

        public bool GuiEmail(string toEmail, string matKhauMoi)
        {
            try
            {
                // Cấu hình thông tin gửi
                string fromEmail = "duongthengoc01112003@gmail.com";
                string password = "plvz fszq ewbb gbzf"; // Lưu ý: App password, không phải mật khẩu Gmail thường
                string subject = "Khôi phục mật khẩu - Ngân hàng tiết kiệm";
                string body = $"Xin chào,\n\nMật khẩu mới của bạn là: {matKhauMoi}\nHãy đăng nhập và thay đổi mật khẩu ngay.";

                // Cấu hình SMTP
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential(fromEmail, password);

                MailMessage message = new MailMessage(fromEmail, toEmail, subject, body);
                smtp.Send(message);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi gửi email: " + ex.Message);
                return false;
            }
        }

        public string TaoMatKhauNgauNhien(int length = 8)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        private void btnGui_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập email.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            string email = txtEmail.Text.Trim();

            // Tạo mật khẩu mới và mã hóa
            string matKhauMoi = TaoMatKhauNgauNhien();
            string matKhauMaHoa = MaHoa.MaHoaMD5(matKhauMoi);

            // Gửi email
            if (GuiEmail(email, matKhauMoi))
            {
                // Cập nhật CSDL
                bool capNhatThanhCong = TaiKhoanDAL.CapNhatMatKhau(email, matKhauMaHoa);
                if (capNhatThanhCong)
                {
                    MessageBox.Show("Mật khẩu mới đã được gửi và cập nhật thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Email không tồn tại trong hệ thống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
