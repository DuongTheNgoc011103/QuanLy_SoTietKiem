using QuanLy_SoTietKiem.BLL;
using QuanLy_SoTietKiem.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLy_SoTietKiem
{
    public partial class DangNhap: Form
    {
        public DangNhap()
        {
            InitializeComponent();
            txtTenDN.Text = "admin";
            txtMK.Text = "1";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if(DialogResult.Yes == MessageBox.Show("Xác nhận thoát chương trình?","Thông báo.", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                Application.Exit();
            }
        }

        private void chk_ShowPW_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_ShowPW.Checked)
            {
                txtMK.PasswordChar = '\0';
            }
            else 
            { 
                txtMK.PasswordChar = '*';
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtTenDN.Text.Trim() == "" || txtMK.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenDN = txtTenDN.Text.Trim();
            string matKhau = txtMK.Text;

            TaiKhoanDTO taiKhoan = TaiKhoanBLL.DangNhapLayThongTin(tenDN, matKhau);

            if (!taiKhoan.TrangThai)
            {
                MessageBox.Show("Tài khoản đã bị khóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (taiKhoan != null)
            {
                // Lưu lịch sử đăng nhập
                LichSuThaoTacDTO lichSu = new LichSuThaoTacDTO
                {
                    MaTaiKhoan = taiKhoan.MaTaiKhoan,
                    ThoiGian = DateTime.Now,
                    ThaoTac = "Đăng nhập thành công",
                    DoiTuong = "Tài khoản: " + taiKhoan.TenDangNhap
                };
                LichSuThaoTacBLL.ThemLichSuThaoTac(lichSu);

                // Truyền thông tin sang form TrangChu
                GUI.TrangChu trangChu = new GUI.TrangChu(taiKhoan);
                trangChu.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi");
            }
        }


        private void lbQuenMK_Click(object sender, EventArgs e)
        {
            GUI.QuenMatKhau quenMK = new GUI.QuenMatKhau();
            quenMK.Show();
            this.Hide();
        }
    }
}
