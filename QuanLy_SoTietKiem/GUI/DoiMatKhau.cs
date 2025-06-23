using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using QuanLy_SoTietKiem.BLL;
using QuanLy_SoTietKiem.DTO;
using QuanLy_SoTietKiem.Utils;

namespace QuanLy_SoTietKiem.GUI
{
    public partial class DoiMatKhau: Form
    {
        private string tenDangNhap;
        public DoiMatKhau(string tenDN)
        {
            InitializeComponent();
            this.tenDangNhap = tenDN;
        }

        private void btnGui_Click(object sender, EventArgs e)
        {
            string mkCu = txtOldPW.Text.Trim();
            string mkMoi = txtnewPW.Text.Trim();
            string nhapLai = txtrepeatPW.Text.Trim();

            if (string.IsNullOrEmpty(mkCu) || string.IsNullOrEmpty(mkMoi) || string.IsNullOrEmpty(nhapLai))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (mkMoi != nhapLai)
            {
                MessageBox.Show("Mật khẩu mới và xác nhận mật khẩu không khớp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra mật khẩu cũ đúng không
            string mkCuMaHoa = MaHoa.MaHoaMD5(mkCu);
            bool hopLe = TaiKhoanBLL.DangNhap(tenDangNhap, mkCu); // Kiểm tra mật khẩu cũ đúng

            if (!hopLe)
            {
                MessageBox.Show("Mật khẩu cũ không đúng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mã hóa và cập nhật mật khẩu mới
            string mkMoiMaHoa = MaHoa.MaHoaMD5(mkMoi);
            bool doiThanhCong = TaiKhoanBLL.CapNhatMatKhau(tenDangNhap, mkMoiMaHoa);

            if (doiThanhCong)
            {
                // Lưu lịch sử đăng nhập
                LichSuThaoTacDTO lichSu = new LichSuThaoTacDTO
                {
                    MaTaiKhoan = TaiKhoanBLL.LayDanhSachTaiKhoan().FirstOrDefault(tk => tk.TenDangNhap == tenDangNhap)?.MaTaiKhoan ?? 0,
                    ThaoTac = "Đổi mật khẩu",
                    ThoiGian = DateTime.Now,
                    DoiTuong = "Tài khoản: " + tenDangNhap
                };
                LichSuThaoTacBLL.ThemLichSuThaoTac(lichSu);

                MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Đổi mật khẩu thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {            
            this.Hide();
        }
    }
}
