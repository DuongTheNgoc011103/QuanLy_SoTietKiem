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

namespace QuanLy_SoTietKiem.GUI
{
    public partial class QuanLy_LoaiSoTietKiem : Form
    {
        int currentPage = 1;
        int pageSize = 15;
        int totalRows = 0;
        int totalPages = 0;

        int currentPage_STK = 1;
        int pageSize_STK = 20;
        int totalRows_STK = 0;
        int totalPages_STK = 0;

        List<DTO.LoaiSoTietKiemDTO> fullData = new List<DTO.LoaiSoTietKiemDTO>();

        public void LoadFullData()
        {
            fullData = BLL.LoaiSoTietKiemBLL.GetAllLoaiSoTietKiem();
            totalRows = fullData.Count;
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
        }

        private void DisplayPage(int page)
        {
            int start = (page - 1) * pageSize;
            var pageData = fullData.Skip(start).Take(pageSize).ToList();

            dgvDSLoaiSoTietKiem.AutoGenerateColumns = false;
            dgvDSLoaiSoTietKiem.DataSource = pageData;

            txtPage.Text = currentPage.ToString();
            txtTotalPages.Text = totalPages.ToString();
            txtTotal.Text = totalRows.ToString();
        }


        /// <summary>
        /// Load data Sổ Tiết Kiệm
        /// </summary>
        // Danh sách đầy đủ nhân viên chưa có tài khoản để phân trang
        List<SoTietKiemDTO> fullData_STK = new List<SoTietKiemDTO>();

        public void LoadFullData_STK()
        {
            fullData_STK = SoTietKiemBLL.GetSoTietKiemByMaLoai(Convert.ToInt32(txtMaLoai.Text.Trim()));
            totalRows_STK = fullData_STK.Count;
            totalPages_STK = (int)Math.Ceiling((double)totalRows_STK / pageSize_STK);
        }

        private void DisplayPage_STK(int page)
        {
            int start = (page - 1) * pageSize_STK;
            var pageData = fullData_STK.Skip(start).Take(pageSize_STK).ToList();

            dgvDS_SoTietKiem.AutoGenerateColumns = false;
            dgvDS_SoTietKiem.DataSource = pageData;

            txtPage_STK.Text = currentPage_STK.ToString();
            txtTotalPages_STK.Text = totalPages_STK.ToString();
            txtTotal_STK.Text = totalRows_STK.ToString();
        }

        private TaiKhoanDTO taiKhoan;
        public QuanLy_LoaiSoTietKiem(TaiKhoanDTO taiKhoan)
        {
            InitializeComponent();
            this.taiKhoan = taiKhoan;
        }

        private void QuanLy_LoaiSoTietKiem_Load(object sender, EventArgs e)
        {
            LoadFullData();
            DisplayPage(currentPage);
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

        private void dgvDSLoaiSoTietKiem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < fullData.Count)
            {
                DataGridViewRow selectedRow = dgvDSLoaiSoTietKiem.Rows[e.RowIndex];

                txtMaLoai.Text = selectedRow.Cells["MaLoai"]?.Value?.ToString();
                txtTenLoai.Text = selectedRow.Cells["TenLoai"]?.Value?.ToString();
                num_LaiSuatCB.Text = selectedRow.Cells["LaiSuatCoBan"]?.Value?.ToString();
                num_PhiRutTruocHan.Text = selectedRow.Cells["PhiRutTruocHan"]?.Value?.ToString();
                txtMoTa.Text = selectedRow.Cells["MoTa"]?.Value?.ToString();
                num_KyHanYeuCau.Text = selectedRow.Cells["KyHanYeuCau"]?.Value?.ToString();

                LoadFullData_STK();
                DisplayPage_STK(currentPage_STK); // Hiển thị dữ liệu sổ tiết kiệm theo loại đã chọn
            }
        }

        // Clear input
        private void ClearInput()
        {
            txtMaLoai.Clear();
            txtTenLoai.Clear();
            num_LaiSuatCB.Value = 0;
            num_PhiRutTruocHan.Value = 0;
            txtMoTa.Clear();

            txtSearch.Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearInput();
        }

        private void btnReLoad_Click(object sender, EventArgs e)
        {
            LoadFullData();                  // Tải lại toàn bộ dữ liệu để phân trang
            currentPage = 1;                 // Quay về trang đầu tiên
            DisplayPage(currentPage);       // Hiển thị lại dữ liệu theo trang
            ClearInput();                    // Xóa ô nhập

            dgvDS_SoTietKiem.DataSource = null; // Xóa dữ liệu trong DataGridView danh sách sổ tiết kiệm
            txtPage_STK.Text = string.Empty; // Đặt lại trang hiện tại của sổ tiết kiệm
            txtTotalPages_STK.Text = string.Empty; // Đặt lại tổng số trang của sổ tiết kiệm
            txtTotal_STK.Text = string.Empty; // Đặt lại tổng số sổ tiết kiệm
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm");
                return;
            }
            string keyword = txtSearch.Text.Trim();
            List<LoaiSoTietKiemDTO> searchResults = LoaiSoTietKiemBLL.SearchLoaiSoTietKiemByName(keyword);
            if (searchResults.Count == 0)
            {
                MessageBox.Show("Không tìm thấy loại sổ nào với từ khóa: " + keyword, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvDSLoaiSoTietKiem.DataSource = null; // Xóa dữ liệu trong DataGridView
            }
            else
            {
                dgvDSLoaiSoTietKiem.DataSource = searchResults;
                ClearInput(); // Xóa dữ liệu trong các ô nhập
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaLoai.Text))
            {
                MessageBox.Show("Vui lòng chọn loại sổ cần xóa");
                return;
            }

            // Kiểm tra xem người dùng có chắc chắn muốn xóa loại sổ không
            if (DialogResult.OK == MessageBox.Show("Xác nhận xóa loại sổ " + txtTenLoai.Text, "Thông báo.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                int maLoai = Convert.ToInt32(txtMaLoai.Text);
                if (LoaiSoTietKiemBLL.DeleteLoaiSoTietKiem(maLoai))
                {
                    // Lưu lịch sử
                    LichSuThaoTacDTO lichSu = new LichSuThaoTacDTO
                    {
                        MaTaiKhoan = taiKhoan.MaTaiKhoan,
                        ThoiGian = DateTime.Now,
                        ThaoTac = "Xóa loại sổ tiết kiệm",
                        DoiTuong = "Loại sổ tiết kiệm: " + txtTenLoai.Text
                    };
                    LichSuThaoTacBLL.ThemLichSuThaoTac(lichSu);

                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Xóa thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnReLoad_Click(sender, e); // Tải lại dữ liệu sau khi xóa
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng đã nhập đầy đủ thông tin chưa
            if (string.IsNullOrEmpty(txtTenLoai.Text))
            {
                MessageBox.Show("Vui lòng nhập tên loại sổ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenLoai.Focus();
                return;
            }
            if (num_LaiSuatCB.Value <= 0)
            {
                MessageBox.Show("Lãi suất cơ bản phải lớn hơn 0", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                num_LaiSuatCB.Focus();
                return;
            }
            if (num_PhiRutTruocHan.Value < 0)
            {
                MessageBox.Show("Phí rút trước hạn không được âm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                num_PhiRutTruocHan.Focus();
                return;
            }

            // Kiểm tra xem người dùng có chắc chắn muốn thêm chi nhánh không
            if (DialogResult.OK == MessageBox.Show("Xác nhận thêm thông tin sổ " + txtTenLoai.Text, "Thông báo.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                LoaiSoTietKiemDTO newLoaiSo = new LoaiSoTietKiemDTO
                {
                    TenLoai = txtTenLoai.Text.Trim(),
                    LaiSuatCoBan = (float)num_LaiSuatCB.Value,
                    PhiRutTruocHan = (float)num_PhiRutTruocHan.Value,
                    MoTa = txtMoTa.Text.Trim() ?? string.Empty,
                    KyHanYeuCau = (int)num_KyHanYeuCau.Value // Thêm trường kỳ hạn yêu cầu nếu cần
                };

                if (LoaiSoTietKiemBLL.AddLoaiSoTietKiem(newLoaiSo))
                {
                    // Lưu lịch sử
                    LichSuThaoTacDTO lichSu = new LichSuThaoTacDTO
                    {
                        MaTaiKhoan = taiKhoan.MaTaiKhoan,
                        ThoiGian = DateTime.Now,
                        ThaoTac = "Thêm loại sổ tiết kiệm mới",
                        DoiTuong = "Loại sổ tiết kiệm: " + txtTenLoai.Text
                    };
                    LichSuThaoTacBLL.ThemLichSuThaoTac(lichSu);

                    MessageBox.Show("Thêm loại sổ thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnReLoad_Click(sender, e); // Tải lại dữ liệu sau khi thêm
                }
                else
                {
                    MessageBox.Show("Thêm loại sổ thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn loại sổ cần sửa chưa
            if (string.IsNullOrEmpty(txtMaLoai.Text))
            {
                MessageBox.Show("Vui lòng chọn loại sổ cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Kiểm tra xem người dùng đã nhập đầy đủ thông tin chưa
            if (string.IsNullOrEmpty(txtTenLoai.Text))
            {
                MessageBox.Show("Vui lòng nhập tên loại sổ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenLoai.Focus();
                return;
            }
            if (num_LaiSuatCB.Value <= 0)
            {
                MessageBox.Show("Lãi suất cơ bản phải lớn hơn 0", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                num_LaiSuatCB.Focus();
                return;
            }
            if (num_PhiRutTruocHan.Value < 0)
            {
                MessageBox.Show("Phí rút trước hạn không được âm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                num_PhiRutTruocHan.Focus();
                return;
            }
            // Kiểm tra xem người dùng có chắc chắn muốn sửa loại sổ không
            if (DialogResult.OK == MessageBox.Show("Xác nhận sửa thông tin loại sổ " + txtTenLoai.Text, "Thông báo.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                LoaiSoTietKiemDTO updatedLoaiSo = new LoaiSoTietKiemDTO
                {
                    MaLoai = Convert.ToInt32(txtMaLoai.Text),
                    TenLoai = txtTenLoai.Text.Trim(),
                    LaiSuatCoBan = (float)num_LaiSuatCB.Value,
                    PhiRutTruocHan = (float)num_PhiRutTruocHan.Value,
                    MoTa = txtMoTa.Text.Trim() ?? string.Empty,
                    KyHanYeuCau = (int)num_KyHanYeuCau.Value // Thêm trường kỳ hạn yêu cầu nếu cần
                };
                if (LoaiSoTietKiemBLL.UpdateLoaiSoTietKiem(updatedLoaiSo))
                {
                    // Lưu lịch sử
                    LichSuThaoTacDTO lichSu = new LichSuThaoTacDTO
                    {
                        MaTaiKhoan = taiKhoan.MaTaiKhoan,
                        ThoiGian = DateTime.Now,
                        ThaoTac = "Cập nhật loại sổ tiết kiệm",
                        DoiTuong = "Loại sổ tiết kiệm: " + txtTenLoai.Text
                    };
                    LichSuThaoTacBLL.ThemLichSuThaoTac(lichSu);

                    MessageBox.Show("Sửa loại sổ thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnReLoad_Click(sender, e); // Tải lại dữ liệu sau khi sửa
                }
                else
                {
                    MessageBox.Show("Sửa loại sổ thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnPrev_STK_Click(object sender, EventArgs e)
        {
            if (currentPage_STK > 1)
            {
                currentPage_STK--;
                DisplayPage_STK(currentPage_STK);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (currentPage_STK < totalPages_STK)
            {
                currentPage_STK++;
                DisplayPage_STK(currentPage_STK);
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            Reports.LoaiSoTietKiem.LoaiSoTietKiemReport frm = new Reports.LoaiSoTietKiem.LoaiSoTietKiemReport();
            frm.ShowDialog();
        }

        private void dgvDS_SoTietKiem_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridViewRow row = dgvDS_SoTietKiem.Rows[e.RowIndex];

            if (row.Cells["TrangThai"].Value != null)
            {
                string trangThai = row.Cells["TrangThai"].Value.ToString().Trim();

                if (trangThai == "Ngưng hoạt động" || trangThai == "Đã đóng")
                {
                    row.DefaultCellStyle.BackColor = Color.Gray; // Có thể đổi sang Color.LightGray
                    row.DefaultCellStyle.ForeColor = Color.White;
                }
                else if (row.Cells["TrangThai"].Value != null && row.Cells["TrangThai"].Value.ToString().Trim() == "Đáo hạn")
                {
                    row.DefaultCellStyle.ForeColor = Color.Red; // chữ trắng cho nổi bật
                }
            }
        }
    }
}
