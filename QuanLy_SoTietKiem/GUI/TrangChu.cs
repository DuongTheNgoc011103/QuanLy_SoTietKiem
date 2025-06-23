using QuanLy_SoTietKiem.BLL;
using QuanLy_SoTietKiem.DAL;
using QuanLy_SoTietKiem.DTO;
using QuanLy_SoTietKiem.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLy_SoTietKiem.GUI
{
    public partial class TrangChu: Form
    {
        private System.Windows.Forms.Timer daoHanTimer;

        private Form currentFormChild;
        private TaiKhoanDTO taiKhoan;
        public TrangChu(TaiKhoanDTO taiKhoanDTO)
        {
            InitializeComponent();
            this.taiKhoan = taiKhoanDTO;
            HienThiThongTinNguoiDung();

            SetupDaoHanTimer(); // Gọi khi khởi tạo form
        }

        private void SetupDaoHanTimer()
        {
            daoHanTimer = new System.Windows.Forms.Timer();
            daoHanTimer.Interval = 60 * 60 * 1000; // Kiểm tra mỗi 1 giờ (có thể điều chỉnh)
                                                   // daoHanTimer.Interval = 10 * 1000; // Để test, chạy mỗi 10 giây
            daoHanTimer.Tick += DaoHanTimer_Tick;
            daoHanTimer.Start();

            // Chạy kiểm tra ngay khi khởi động
            DaoHanTimer_Tick(null, null);
        }

        private void DaoHanTimer_Tick(object sender, EventArgs e)
        {
            // Kiểm tra và cập nhật trạng thái sổ đáo hạn
            List<int> updatedSoMos = SoTietKiemBLL.CapNhatSoDaoHan();

            if (updatedSoMos.Count > 0)
            {
                //MessageBox.Show($"Đã có {updatedSoMos.Count} sổ tiết kiệm đáo hạn và được cập nhật trạng thái.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Gửi email cho các sổ vừa đáo hạn
                foreach (int maSo in updatedSoMos)
                {
                    SoTietKiemDTO soChiTiet = SoTietKiemBLL.GetSoTietKiemChiTiet(maSo);
                    if (soChiTiet != null && !string.IsNullOrEmpty(KhachHangBLL.GetEmailByMaKH(soChiTiet.MaKH)))
                    {
                        // Gọi hàm gửi mail
                        SendMaturityReminderEmail(soChiTiet);
                    }
                }
            }
        }

        // Trong TrangChu.cs (tiếp nối phần DaoHanTimer_Tick)

        private void SendMaturityReminderEmail(SoTietKiemDTO soTietKiem)
        {
            string subject = $"THÔNG BÁO: Sổ tiết kiệm của quý khách đã ĐÁO HẠN - Mã sổ: {soTietKiem.MaSo}";

            string tenLoai = LoaiSoTietKiemBLL.GetTenLoaiByMaLoai(soTietKiem.MaLoai);
            string email = KhachHangBLL.GetEmailByMaKH(soTietKiem.MaKH);

            // Tạo nội dung email HTML
            StringBuilder bodyBuilder = new StringBuilder();
            bodyBuilder.AppendLine($"<p>Kính gửi Khách hàng: <strong>{soTietKiem.HoTen}</strong>,</p>");
            bodyBuilder.AppendLine("<p>Chúng tôi xin thông báo sổ tiết kiệm của quý khách với các thông tin sau đã chính thức đáo hạn:</p>");
            bodyBuilder.AppendLine("<ul>");
            bodyBuilder.AppendLine($"<li><strong>Mã sổ:</strong> {soTietKiem.MaSo}</li>");
            bodyBuilder.AppendLine($"<li><strong>Loại sổ:</strong> {tenLoai}</li>");
            bodyBuilder.AppendLine($"<li><strong>Ngày mở:</strong> {soTietKiem.NgayMo:dd/MM/yyyy}</li>");
            bodyBuilder.AppendLine($"<li><strong>Kỳ hạn:</strong> {soTietKiem.KyHan} tháng</li>");
            bodyBuilder.AppendLine($"<li><strong>Ngày đáo hạn:</strong> {soTietKiem.NgayDaoHan:dd/MM/yyyy}</li>");
            bodyBuilder.AppendLine($"<li><strong>Số tiền gốc:</strong> {string.Format(new CultureInfo("vi-VN"), "{0:#,##0.00} ₫", soTietKiem.SoTienGoc)}</li>");
            bodyBuilder.AppendLine("</ul>");
            bodyBuilder.AppendLine("<p>Để được tư vấn về các lựa chọn tiếp theo (tái tục, tất toán, gửi mới), quý khách vui lòng liên hệ với chúng tôi tại chi nhánh gần nhất hoặc qua điện thoại.</p>");
            bodyBuilder.AppendLine("<p>Trân trọng cảm ơn!</p>");
            bodyBuilder.AppendLine("<p><strong>Hệ thống Quản lý Sổ Tiết Kiệm</strong></p>");

            if (EmailHelper.SendEmail(email, subject, bodyBuilder.ToString()))
            {
                Console.WriteLine($"Đã gửi email nhắc nhở đáo hạn cho khách hàng {soTietKiem.HoTen} ({email}) về sổ {soTietKiem.MaSo}.");
            }
            else
            {
                Console.WriteLine($"Không thể gửi email nhắc nhở đáo hạn cho khách hàng {soTietKiem.HoTen} ({email}) về sổ {soTietKiem.MaSo}.");
            }
        }

        private void OpenChildForm(Form childForm)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            pnlMain.Controls.Add(childForm);
            pnlMain.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void HienThiThongTinNguoiDung()
        {
            mnuTaiKhoan.Text = taiKhoan.HoTen;
            mnuQuyenHan.Text = "Quyền Hạn: " + taiKhoan.QuyenHan;
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Xác nhận thoát chương trình?", "Thông báo.", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                // Lưu lịch sử
                LichSuThaoTacDTO lichSu = new LichSuThaoTacDTO
                {
                    MaTaiKhoan = taiKhoan.MaTaiKhoan,
                    ThoiGian = DateTime.Now,
                    ThaoTac = "Thoát hệ thống",
                    DoiTuong = "Tài khoản: " + taiKhoan.TenDangNhap
                };
                LichSuThaoTacBLL.ThemLichSuThaoTac(lichSu);

                Application.Exit();
            }
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Lưu lịch sử
            LichSuThaoTacDTO lichSu = new LichSuThaoTacDTO
            {
                MaTaiKhoan = taiKhoan.MaTaiKhoan,
                ThoiGian = DateTime.Now,
                ThaoTac = "Đăng xuất hệ thống",
                DoiTuong = "Tài khoản: " + taiKhoan.TenDangNhap
            };
            LichSuThaoTacBLL.ThemLichSuThaoTac(lichSu);

            DangNhap login = new DangNhap();
            login.Show();
            this.Hide();
        }

        private void TrangChu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoiMatKhau changePW = new DoiMatKhau(taiKhoan.TenDangNhap);
            changePW.ShowDialog(); 
        }

        private void mnuQLNhanVIen_Click(object sender, EventArgs e)
        {
            OpenChildForm(new GUI.QuanLy_NhanVien(taiKhoan));

        }

        private void mnuQLChiNhanh_Click(object sender, EventArgs e)
        {
            OpenChildForm(new GUI.QuanLy_ChiNhanh(taiKhoan));
        }

        private void mnuQLTaiKhoan_Click(object sender, EventArgs e)
        {
            OpenChildForm(new GUI.QuanLy_TaiKhoan(taiKhoan));
        }

        private void mnu_DSKhachHang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new GUI.QuanLy_DSKhachHang(taiKhoan));  
        }

        private void mnu_LoaiSTK_Click(object sender, EventArgs e)
        {
            OpenChildForm(new GUI.QuanLy_LoaiSoTietKiem(taiKhoan));
        }

        private void mnu_DSSoTietKiem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new GUI.QuanLy_SoTietKiem(taiKhoan.MaTaiKhoan));
        }

        private void mnu_PhanLoai_KH_Click(object sender, EventArgs e)
        {
            OpenChildForm(new GUI.PhanLoai_KhachHang(taiKhoan));
        }

        private void mnu_NhatKyThaoTac_Click(object sender, EventArgs e)
        {
            OpenChildForm(new GUI.LichSu_ThaoTac(taiKhoan));
        }

        private void mnuGuiTien_Click(object sender, EventArgs e)
        {
            OpenChildForm(new GUI.GiaoDich_GuiTien(taiKhoan));
        }

        private void TrangChu_Load(object sender, EventArgs e)
        {

        }

        private void mnu_RutTien_Click(object sender, EventArgs e)
        {
            OpenChildForm(new GUI.GiaoDich_RutTien(taiKhoan));
        }
    }
}
