using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using QuanLy_SoTietKiem.DTO;
using QuanLy_SoTietKiem.BLL;

namespace QuanLy_SoTietKiem.GUI
{
    public partial class QuanLy_TaiKhoan: Form
    {
        private string oldTenDN = string.Empty; // Biến lưu tên đăng nhập cũ để kiểm tra khi sửa tài khoản

        int currentPage = 1;
        int pageSize = 10;
        int totalRows = 0;
        int totalPages = 0;

        int currentPage_NV = 1;
        int pageSize_NV = 20;
        int totalRows_NV = 0;
        int totalPages_NV = 0;

        // Danh sách đầy đủ tài khoản để phân trang
        List<TaiKhoanDTO> fullData = new List<TaiKhoanDTO>();

        public void LoadFullData()
        {
            fullData = BLL.TaiKhoanBLL.LayDanhSachTaiKhoan();
            totalRows = fullData.Count;
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
        }

        private void DisplayPage(int page)
        {
            int start = (page - 1) * pageSize;
            var pageData = fullData.Skip(start).Take(pageSize).ToList();
            
            dgvDSTaiKhoan.AutoGenerateColumns = false;
            dgvDSTaiKhoan.DataSource = pageData;

            txtPage.Text = currentPage.ToString();
            txtTotalPages.Text = totalPages.ToString();
            txtToTal.Text = totalRows.ToString();
        }

        // Danh sách đầy đủ nhân viên chưa có tài khoản để phân trang
        List<NhanVienDTO> fullData_NV = new List<NhanVienDTO>();

        public void LoadFullData_NV()
        {
            fullData_NV = BLL.NhanVienBLL.GetNhanVienChuaCoTaiKhoan();
            totalRows_NV = fullData_NV.Count;
            totalPages_NV = (int)Math.Ceiling((double)totalRows_NV / pageSize_NV);
        }

        private void DisplayPage_NV(int page)
        {
            int start = (page - 1) * pageSize_NV;
            var pageData = fullData_NV.Skip(start).Take(pageSize_NV).ToList();
            
            dgvDS_NhanVien.AutoGenerateColumns = false;
            dgvDS_NhanVien.DataSource = pageData;

            txtPage_NV.Text = currentPage_NV.ToString();
            txtTotalPages_NV.Text = totalPages_NV.ToString();
            txtTotal_NV.Text = totalRows_NV.ToString();
        }
        private TaiKhoanDTO taikhoan; // Biến lưu thông tin tài khoản đăng nhập
        public QuanLy_TaiKhoan(TaiKhoanDTO taiKhoan)
        {
            InitializeComponent();
            this.taikhoan = taiKhoan; // Lưu thông tin tài khoản đăng nhập
        }

        private void QuanLy_TaiKhoan_Load(object sender, EventArgs e)
        {
            LoadFullData();
            DisplayPage(currentPage);

            LoadFullData_NV();
            DisplayPage_NV(currentPage_NV);

            LoadQuyenHanAndTrangThai(); // Tải danh sách quyền hạn và trạng thái vào ComboBox
        }

        // Clear input
        private void ClearInput()
        {
            txtMaTK.Text = string.Empty;
            txtTenDN.Text = string.Empty;
            txtMK.Text = string.Empty;

            txtNV.Text = string.Empty; // Xóa ô nhập mã nhân viên
            txtSearch.Text = string.Empty; // Xóa ô tìm kiếm

            cbxQuyenHan.SelectedIndex = -1;
            cbxTrangThai.SelectedIndex = -1;
        }


        private void btnReLoad_Click(object sender, EventArgs e)
        {
            LoadFullData();                  // Tải lại toàn bộ dữ liệu để phân trang
            currentPage = 1;                 // Quay về trang đầu tiên
            DisplayPage(currentPage);       // Hiển thị lại dữ liệu theo trang

            LoadFullData_NV();              // Tải lại danh sách nhân viên chưa có tài khoản
            currentPage_NV = 1;              // Quay về trang đầu tiên
            DisplayPage_NV(currentPage_NV); // Hiển thị lại dữ liệu theo trang

            ClearInput();                    // Xóa ô nhập
        }

        // Load danh sách quyền hạn và trạng thái
        private void LoadQuyenHanAndTrangThai()
        {
            // Tải danh sách quyền hạn
            cbxQuyenHan.DataSource = new List<string> { "Admin", "NhanVien" };
            cbxQuyenHan.SelectedIndex = -1; // Không chọn mặc định
            // Tải danh sách trạng thái
            cbxTrangThai.DataSource = new List<string> { "True", "False" };
            cbxTrangThai.SelectedIndex = -1; // Không chọn mặc định
        }

        private void dgvDSTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvDSTaiKhoan.Rows.Count)
            {
                DataGridViewRow row = dgvDSTaiKhoan.Rows[e.RowIndex];

                // Hiển thị thông tin tài khoản được chọn vào các ô nhập
                txtMaTK.Text = row.Cells["MaTaiKhoan"]?.Value?.ToString();
                txtTenDN.Text = row.Cells["TenDangNhap"]?.Value?.ToString();
                oldTenDN = txtTenDN.Text; // Lưu tên đăng nhập cũ để kiểm tra khi sửa

                txtMK.Text = row.Cells["MatKhau"]?.Value?.ToString();
                cbxQuyenHan.SelectedItem = row.Cells["QuyenHan"]?.Value?.ToString();
                cbxTrangThai.SelectedItem = row.Cells["TrangThai"]?.Value?.ToString();
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                DisplayPage(currentPage);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                DisplayPage(currentPage);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearInput(); // Xóa dữ liệu trong các ô nhập
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm");
                return;
            }
            string keyword = txtSearch.Text.Trim();
            List<TaiKhoanDTO> searchResults = TaiKhoanBLL.TimKiemTaiKhoan(keyword);
            if (searchResults.Count == 0)
            {
                MessageBox.Show("Không tìm thấy tài khoản nào với từ khóa: " + keyword, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvDSTaiKhoan.DataSource = null; // Xóa dữ liệu trong DataGridView
            }
            else
            {
                dgvDSTaiKhoan.DataSource = searchResults;
                ClearInput(); // Xóa dữ liệu trong các ô nhập
            }
        }

        private void dgvDS_NhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvDS_NhanVien.Rows.Count)
            {
                DataGridViewRow row = dgvDS_NhanVien.Rows[e.RowIndex];
                // Hiển thị thông tin nhân viên được chọn vào các ô nhập
                txtNV.Text = row.Cells["MaNV"]?.Value?.ToString();
            }
        }

        private void btnPrev_NV_Click(object sender, EventArgs e)
        {
            if (currentPage_NV > 1)
            {
                currentPage_NV--;
                DisplayPage_NV(currentPage_NV);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (currentPage_NV < totalPages_NV)
            {
                currentPage_NV++;
                DisplayPage_NV(currentPage_NV);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Kiểm tra các ô nhập
            if (string.IsNullOrEmpty(txtTenDN.Text) || string.IsNullOrEmpty(txtMK.Text) || cbxQuyenHan.SelectedIndex == -1 || cbxTrangThai.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Kiểm tra mã nhân viên đã được chọn
            if (string.IsNullOrEmpty(txtNV.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên để tạo tài khoản", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Kiểm tra tên đăng nhập đã tồn tại chưa
            if (TaiKhoanBLL.KiemTraTenDangNhapTonTai(txtTenDN.Text.Trim()))
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại, vui lòng chọn tên khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDN.Focus(); // Đặt con trỏ vào ô tên đăng nhập
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thêm tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // Tạo đối tượng TaiKhoanDTO
                TaiKhoanDTO taiKhoan = new TaiKhoanDTO
                {
                    TenDangNhap = txtTenDN.Text.Trim(),
                    MatKhau = txtMK.Text.Trim(),
                    QuyenHan = cbxQuyenHan.SelectedItem.ToString(),
                    TrangThai = Convert.ToBoolean(cbxTrangThai.SelectedItem),
                    MaNV = Convert.ToInt32(txtNV.Text.Trim())
                };
                // Thêm tài khoản mới
                if (TaiKhoanBLL.ThemTaiKhoan(taiKhoan))
                {
                    // Lưu lịch sử
                    LichSuThaoTacDTO lichSu = new LichSuThaoTacDTO
                    {
                        MaTaiKhoan = taikhoan.MaTaiKhoan,
                        ThoiGian = DateTime.Now,
                        ThaoTac = "Thêm tài khoản mới",
                        DoiTuong = "Tài khoản: " + txtTenDN.Text.Trim()
                    };
                    LichSuThaoTacBLL.ThemLichSuThaoTac(lichSu);

                    MessageBox.Show("Thêm tài khoản thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btnReLoad_Click(sender, e); // Tải lại danh sách tài khoản và nhân viên
                }
                else
                {
                    MessageBox.Show("Thêm tài khoản thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Kiểm tra các ô nhập
            if (string.IsNullOrEmpty(txtMaTK.Text) || string.IsNullOrEmpty(txtTenDN.Text) || string.IsNullOrEmpty(txtMK.Text) || cbxQuyenHan.SelectedIndex == -1 || cbxTrangThai.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Kiểm tra tên đăng nhập đã thay đổi hay chưa
            if (oldTenDN != txtTenDN.Text.Trim() && TaiKhoanBLL.KiemTraTenDangNhapTonTai(txtTenDN.Text.Trim()))
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại, vui lòng chọn tên khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDN.Focus(); // Đặt con trỏ vào ô tên đăng nhập
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn sửa tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // Tạo đối tượng TaiKhoanDTO
                TaiKhoanDTO taiKhoan = new TaiKhoanDTO
                {
                    MaTaiKhoan = Convert.ToInt32(txtMaTK.Text.Trim()),
                    TenDangNhap = txtTenDN.Text.Trim(),
                    MatKhau = txtMK.Text.Trim(),
                    QuyenHan = cbxQuyenHan.SelectedItem.ToString(),
                    TrangThai = Convert.ToBoolean(cbxTrangThai.SelectedItem),
                };
                // Sửa tài khoản
                if (TaiKhoanBLL.SuaTaiKhoan(taiKhoan))
                {
                    // Lưu lịch sử
                    LichSuThaoTacDTO lichSu = new LichSuThaoTacDTO
                    {
                        MaTaiKhoan = taikhoan.MaTaiKhoan,
                        ThoiGian = DateTime.Now,
                        ThaoTac = "Cập nhật tài khoản",
                        DoiTuong = "Tài khoản: " + txtTenDN.Text.Trim()
                    };
                    LichSuThaoTacBLL.ThemLichSuThaoTac(lichSu);

                    MessageBox.Show("Sửa tài khoản thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btnReLoad_Click(sender, e); // Tải lại danh sách tài khoản và nhân viên
                }
                else
                {
                    MessageBox.Show("Sửa tài khoản thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Kiểm tra ô nhập mã tài khoản
            if (string.IsNullOrEmpty(txtMaTK.Text))
            {
                MessageBox.Show("Vui lòng chọn tài khoản để xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Xác nhận xóa tài khoản
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                int maTaiKhoan = Convert.ToInt32(txtMaTK.Text.Trim());
                // Xóa tài khoản
                if (TaiKhoanBLL.XoaTaiKhoan(maTaiKhoan))
                {
                    // Lưu lịch sử
                    LichSuThaoTacDTO lichSu = new LichSuThaoTacDTO
                    {
                        MaTaiKhoan = taikhoan.MaTaiKhoan,
                        ThoiGian = DateTime.Now,
                        ThaoTac = "Xóa tài khoản",
                        DoiTuong = "Tài khoản: " + txtTenDN.Text.Trim()
                    };
                    LichSuThaoTacBLL.ThemLichSuThaoTac(lichSu);

                    MessageBox.Show("Xóa tài khoản thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnReLoad_Click(sender, e); // Tải lại danh sách tài khoản và nhân viên
                }
                else
                {
                    MessageBox.Show("Xóa tài khoản thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvDSTaiKhoan_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridViewRow row = dgvDSTaiKhoan.Rows[e.RowIndex];

            if (row.Cells["TrangThai"].Value != null && row.Cells["TrangThai"].Value.ToString().Trim() == "False")
            {
                row.DefaultCellStyle.BackColor = Color.Gray; // hoặc Color.Red nếu bạn muốn đậm hơn
                row.DefaultCellStyle.ForeColor = Color.White; // chữ trắng cho nổi bật
            }
        }
    }
}
