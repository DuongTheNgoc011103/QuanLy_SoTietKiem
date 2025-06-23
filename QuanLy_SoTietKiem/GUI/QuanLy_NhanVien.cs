using QuanLy_SoTietKiem.BLL;
using QuanLy_SoTietKiem.DAL;
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

namespace QuanLy_SoTietKiem.GUI
{
    public partial class QuanLy_NhanVien: Form
    {
        private string oldDienThoai = "";
        private string oldCMND_CCCD = "";
        private string oldEmail = "";

        int currentPage = 1;
        int pageSize = 15;
        int totalRows = 0;
        int totalPages = 0;

        List<DTO.NhanVienDTO> fullData = new List<DTO.NhanVienDTO>();

        public void LoadFullData()
        {
            fullData = BLL.NhanVienBLL.GetAllNhanVien();
            totalRows = fullData.Count;
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
        }

        private void DisplayPage(int page)
        {
            int start = (page - 1) * pageSize;
            var pageData = fullData.Skip(start).Take(pageSize).ToList();
            dgvDSNhanVien.DataSource = pageData;

            txtPage.Text = currentPage.ToString();
            txtTotalPages.Text = totalPages.ToString();
            txtTotalNV.Text = totalRows.ToString();
        }
        private TaiKhoanDTO taiKhoan;
        public QuanLy_NhanVien(TaiKhoanDTO taiKhoan)
        {
            InitializeComponent();
            this.taiKhoan = taiKhoan;
        }

        private void QuanLy_NhanVien_Load(object sender, EventArgs e)
        {
            LoadDSNhanVien();

            LoadFullData(); // Tải toàn bộ dữ liệu chi nhánh
            DisplayPage(currentPage);

            LoadChiNhanh(); // Tải danh sách chi nhánh vào ComboBox
        }

        //Load DSNhanVien
        public void LoadDSNhanVien()
        {
            dgvDSNhanVien.AutoGenerateColumns = false; // Không tự động sinh cột
            dgvDSNhanVien.DataSource = BLL.NhanVienBLL.GetAllNhanVien();
        }

        // Clear input
        private void ClearInput()
        {
            txtMaNV.Text = "";
            txtHoTen.Text = "";
            dtpNgaySinh.Value = DateTime.Now;
            txtEmail.Text = "";
            txtCMND_CCCD.Text = "";
            txtDiaChi.Text = "";
            txtChucVu.Text = "";
            txtDienThoai.Text = "";
            txtPhongBan.Text = "";
            cbxCN.SelectedIndex = -1; // Đặt lại chỉ mục của ComboBox chi nhánh
            txtSearch.Text = ""; // Xóa dữ liệu trong ô tìm kiếm
        }

        private void btnReLoad_Click(object sender, EventArgs e)
        {
            LoadFullData();                  // Tải lại toàn bộ dữ liệu để phân trang
            currentPage = 1;                 // Quay về trang đầu tiên
            DisplayPage(currentPage);       // Hiển thị lại dữ liệu theo trang
            ClearInput();                    // Xóa ô nhập
        }

        //load danh sách chi nhánh vào ComboBox
        public void LoadChiNhanh()
        {
            cbxCN.DataSource = BLL.ChiNhanhBLL.GetAllChiNhanh();
            cbxCN.DisplayMember = "TenChiNhanh"; // Hiển thị tên chi nhánh
            cbxCN.ValueMember = "MaCN"; // Lưu mã chi nhánh
        }

        private void dgvDSNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvDSNhanVien.Rows.Count && !dgvDSNhanVien.Rows[e.RowIndex].IsNewRow)
            {
                DataGridViewRow row = dgvDSNhanVien.Rows[e.RowIndex];

                txtMaNV.Text = row.Cells["MaNV"]?.Value?.ToString() ?? string.Empty;
                txtHoTen.Text = row.Cells["HoTen"]?.Value?.ToString() ?? string.Empty;

                if (row.Cells["NgaySinh"]?.Value != null && DateTime.TryParse(row.Cells["NgaySinh"].Value.ToString(), out DateTime ngaySinh))
                {
                    dtpNgaySinh.Value = ngaySinh;
                }
                else
                {
                    dtpNgaySinh.Value = DateTime.Now;
                }

                txtEmail.Text = row.Cells["Email"]?.Value?.ToString() ?? string.Empty;
                oldEmail = txtEmail.Text; // Lưu email cũ để so sánh khi sửa

                txtCMND_CCCD.Text = row.Cells["CMND_CCCD"]?.Value?.ToString() ?? string.Empty;
                oldCMND_CCCD = txtCMND_CCCD.Text; // Lưu CMND/CCCD cũ để so sánh khi sửa

                txtDiaChi.Text = row.Cells["DiaChi"]?.Value?.ToString() ?? string.Empty;
                txtChucVu.Text = row.Cells["ChucVu"]?.Value?.ToString() ?? string.Empty;

                txtDienThoai.Text = row.Cells["DienThoai"]?.Value?.ToString() ?? string.Empty;
                oldDienThoai = txtDienThoai.Text; // Lưu số điện thoại cũ để so sánh khi sửa

                txtPhongBan.Text = row.Cells["PhongBan"]?.Value?.ToString() ?? string.Empty;
                cbxCN.SelectedValue = row.Cells["MaCN"]?.Value;
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
            List<NhanVienDTO> searchResults = NhanVienBLL.SearchNhanVien(keyword);
            if (searchResults.Count == 0)
            {
                MessageBox.Show("Không tìm thấy chi nhánh nào với từ khóa: " + keyword, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvDSNhanVien.DataSource = null; // Xóa dữ liệu trong DataGridView
            }
            else
            {
                dgvDSNhanVien.DataSource = searchResults;
                ClearInput(); // Xóa dữ liệu trong các ô nhập
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Yêu cầu nhập liệu
            if(string.IsNullOrEmpty(txtHoTen.Text) || string.IsNullOrEmpty(txtCMND_CCCD.Text) || string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin nhân viên");
                return;
            }

            // Kiểm tra CMND_CCCD
            if (txtCMND_CCCD.Text.Length != 9 && txtCMND_CCCD.Text.Length != 12)
            {
                MessageBox.Show("CMND/CCCD phải có 9 hoặc 12 chữ số");
                txtCMND_CCCD.Focus();
                return;
            }

            // Kiểm tra CMND_CCCD đã tồn tại chưa
            if (NhanVienBLL.GetNhanVienByCMND_CCCD(txtCMND_CCCD.Text.Trim()))
            {
                MessageBox.Show("CMND_CCCD đã tồn tại trong hệ thống");
                txtCMND_CCCD.Focus();
                return;
            }

            // Kiểm tra email
            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MessageBox.Show("Email không hợp lệ");
                txtEmail.Focus();
                return;
            }

            // Kiểm tra email đã tồn tại chưa
            if (NhanVienBLL.GetNhanVienByEmail(txtEmail.Text.Trim()))
            {
                MessageBox.Show("Email đã tồn tại trong hệ thống");
                txtEmail.Focus();
                return;
            }

            // Kiểm tra ngày sinh
            if (dtpNgaySinh.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày sinh không hợp lệ");
                dtpNgaySinh.Focus();
                return;
            }

            // Kiểm tra SDT
            if (!string.IsNullOrEmpty(txtDienThoai.Text) && (txtDienThoai.Text.Length < 10 || txtDienThoai.Text.Length > 11 || !txtDienThoai.Text.All(char.IsDigit)))
            {
                MessageBox.Show("Số điện thoại không hợp lệ");
                txtDienThoai.Focus();
                return;
            }

            // Kiểm tra xem số điện thoại đã tồn tại chưa
            if (NhanVienBLL.GetNhanVienBySDT(txtDienThoai.Text))
            {
                MessageBox.Show("Số điện thoại đã tồn tại trong hệ thống");
                txtDienThoai.Focus();
                return;
            }

            // Kiểm tra xem người dùng có chắc chắn muốn thêm chi nhánh không
            if (DialogResult.OK == MessageBox.Show("Xác nhận thêm mới nhân viên " + txtHoTen.Text, "Thông báo.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                // Tạo đối tượng nhân viên mới
                NhanVienDTO newNhanVien = new NhanVienDTO
                {
                    HoTen = txtHoTen.Text,
                    NgaySinh = dtpNgaySinh.Value,
                    CMND_CCCD = txtCMND_CCCD.Text,
                    DiaChi = txtDiaChi.Text ?? null,
                    DienThoai = txtDienThoai.Text ?? null,
                    Email = txtEmail.Text,
                    ChucVu = txtChucVu.Text ?? null,
                    PhongBan = txtPhongBan.Text ?? null,
                    MaCN = Convert.ToInt32(cbxCN.SelectedValue.ToString()) // Lấy mã chi nhánh từ ComboBox
                };

                if (NhanVienBLL.AddNhanVien(newNhanVien))
                {
                    // Lưu lịch sử
                    LichSuThaoTacDTO lichSu = new LichSuThaoTacDTO
                    {
                        MaTaiKhoan = taiKhoan.MaTaiKhoan,
                        ThoiGian = DateTime.Now,
                        ThaoTac = "Thêm nhân viên mới",
                        DoiTuong = "Nhân viên: " + newNhanVien
                    };
                    LichSuThaoTacBLL.ThemLichSuThaoTac(lichSu);

                    MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Thêm thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                btnReLoad_Click(sender, e); // Tải lại dữ liệu sau khi thêm mới
            }            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Yêu cầu nhập liệu
            if (string.IsNullOrEmpty(txtHoTen.Text) || string.IsNullOrEmpty(txtCMND_CCCD.Text) || string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin nhân viên");
                return;
            }

            // Kiểm tra CMND_CCCD
            if (txtCMND_CCCD.Text.Length != 9 && txtCMND_CCCD.Text.Length != 12)
            {
                MessageBox.Show("CMND/CCCD phải có 9 hoặc 12 chữ số");
                txtCMND_CCCD.Focus();
                return;
            }

            // Kiểm tra CMND_CCCD đã tồn tại chưa (trong trường hợp sửa thông tin)
            if (NhanVienBLL.CMND_CCCD_Edit(txtCMND_CCCD.Text.Trim(), oldCMND_CCCD))
            {
                MessageBox.Show("CMND_CCCD đã tồn tại trong hệ thống");
                txtCMND_CCCD.Focus();
                return;
            }

            // Kiểm tra email
            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MessageBox.Show("Email không hợp lệ");
                txtEmail.Focus();
                return;
            }

            // Kiểm tra email đã tồn tại chưa (trong trường hợp sửa thông tin)
            if (NhanVienBLL.Email_Edit(txtEmail.Text.Trim(), oldEmail))
            {
                MessageBox.Show("Email đã tồn tại trong hệ thống");
                txtEmail.Focus();
                return;
            }

            // Kiểm tra ngày sinh
            if (dtpNgaySinh.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày sinh không hợp lệ");
                dtpNgaySinh.Focus();
                return;
            }

            // Kiểm tra SDT
            if (!string.IsNullOrEmpty(txtDienThoai.Text) && (txtDienThoai.Text.Length < 10 || txtDienThoai.Text.Length > 11 || !txtDienThoai.Text.All(char.IsDigit)))
            {
                MessageBox.Show("Số điện thoại không hợp lệ");
                txtDienThoai.Focus();
                return;
            }

            // Kiểm tra xem số điện thoại đã tồn tại chưa
            if (NhanVienDAL.DienThoai_Edit(txtDienThoai.Text.Trim(), oldDienThoai))
            {
                MessageBox.Show("Số điện thoại đã tồn tại! Vui lòng nhập số khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienThoai.Focus();
                return;
            }

            // Kiểm tra xem người dùng có chắc chắn muốn thêm chi nhánh không
            if (DialogResult.OK == MessageBox.Show("Xác nhận sửa thông tin nhân viên " + txtHoTen.Text, "Thông báo.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                // Tạo đối tượng nhân viên mới
                NhanVienDTO newNhanVien = new NhanVienDTO
                {
                    HoTen = txtHoTen.Text,
                    NgaySinh = dtpNgaySinh.Value,
                    CMND_CCCD = txtCMND_CCCD.Text,
                    DiaChi = txtDiaChi.Text ?? null,
                    DienThoai = txtDienThoai.Text ?? null,
                    Email = txtEmail.Text,
                    ChucVu = txtChucVu.Text ?? null,
                    PhongBan = txtPhongBan.Text ?? null,
                    MaCN = Convert.ToInt32(cbxCN.SelectedValue.ToString()), // Lấy mã chi nhánh từ ComboBox
                    MaNV = Convert.ToInt32(txtMaNV.Text) // Lấy mã nhân viên từ ô nhập
                };

                if (NhanVienBLL.EditNhanVien(newNhanVien))
                {
                    // Lưu lịch sử
                    LichSuThaoTacDTO lichSu = new LichSuThaoTacDTO
                    {
                        MaTaiKhoan = taiKhoan.MaTaiKhoan,
                        ThoiGian = DateTime.Now,
                        ThaoTac = "Cập nhật nhân viên",
                        DoiTuong = "Nhân viên: " + newNhanVien
                    };
                    LichSuThaoTacBLL.ThemLichSuThaoTac(lichSu);
                    MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Cập nhật thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnReLoad_Click(sender, e); // Tải lại dữ liệu sau khi thêm mới
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNV.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa");
                return;
            }

            // Kiểm tra xem người dùng có chắc chắn muốn xóa nhân viên không
            if (DialogResult.OK == MessageBox.Show("Xác nhận xóa nhân viên " + txtHoTen.Text, "Thông báo.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                int maNV = Convert.ToInt32(txtMaNV.Text);
                if (NhanVienBLL.DeleteNhanVien(maNV))
                {
                    // Lưu lịch sử
                    LichSuThaoTacDTO lichSu = new LichSuThaoTacDTO
                    {
                        MaTaiKhoan = taiKhoan.MaTaiKhoan,
                        ThoiGian = DateTime.Now,
                        ThaoTac = "Xóa nhân viên",
                        DoiTuong = "Nhân viên: " + maNV
                    };
                    LichSuThaoTacBLL.ThemLichSuThaoTac(lichSu);

                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Xóa thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                btnReLoad_Click(sender, e); // Tải lại dữ liệu sau khi xóa
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            Reports.NhanVien.NhanVienReport frm = new Reports.NhanVien.NhanVienReport();
            frm.ShowDialog();
        }
    }
}
