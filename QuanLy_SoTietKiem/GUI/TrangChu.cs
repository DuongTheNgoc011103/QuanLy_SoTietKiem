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
        
        private Form currentFormChild;
        private TaiKhoanDTO taiKhoan;
        public TrangChu(TaiKhoanDTO taiKhoanDTO)
        {
            InitializeComponent();
            this.taiKhoan = taiKhoanDTO;
            HienThiThongTinNguoiDung();

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
            if(taiKhoan.QuyenHan == "NhanVien")
            {
                mnu_QuanLyNhanSu.Visible = false; // Ẩn menu Quản lý nhân sự
                mnu_LichSuHeThong.Visible = false; // Ẩn menu Nhật ký hệ thống
                mnu_ThongKeBaoCao.Visible = false; // Ẩn menu Thống kê báo cáo
            }
        }

        private void mnu_RutTien_Click(object sender, EventArgs e)
        {
            OpenChildForm(new GUI.GiaoDich_RutTien(taiKhoan));
        }

        private void hướngDẫnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new GUI.HuongDan());
        }

        private void mnu_ThongKe_GDTheoNgay_Click(object sender, EventArgs e)
        {
            OpenChildForm(new GUI.ThongKe_GiaoDich_TheoNgay());
        }

        private void mnu_ThongKe_SoTheoChiNhanh_Click(object sender, EventArgs e)
        {
            OpenChildForm(new GUI.ThongKe_So_TheoChiNhanh());
        }

        private void mnu_ThongKe_TongHopTienGui_Click(object sender, EventArgs e)
        {
            OpenChildForm(new GUI.ThongKe_TongHop_TienGui());
        }
    }
}
