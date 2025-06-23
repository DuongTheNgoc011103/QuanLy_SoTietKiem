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
using QuanLy_SoTietKiem.DAL;

namespace QuanLy_SoTietKiem.GUI
{
    public partial class QuanLy_ChiNhanh: Form
    {
        int currentPage = 1;
        int pageSize = 15;
        int totalRows = 0;
        int totalPages = 0;
        
        List<ChiNhanhDTO> fullData = new List<ChiNhanhDTO>();

        private void LoadFullData()
        {
            // Giả sử bạn có phương thức GetAllBranches() trả về toàn bộ danh sách chi nhánh
            fullData = ChiNhanhBLL.GetAllChiNhanh();
            totalRows = fullData.Count;
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
        }

        private void DisplayPage(int page)
        {
            int start = (page - 1) * pageSize;
            var pageData = fullData.Skip(start).Take(pageSize).ToList();
            dgvDSChiNhanh.DataSource = pageData;

            txtPage.Text = currentPage.ToString();
            txtTotalPages.Text = totalPages.ToString();
            txtToTalCN.Text = totalRows.ToString();
        }

        private TaiKhoanDTO taiKhoan;
        public QuanLy_ChiNhanh(TaiKhoanDTO taiKhoan)
        {
            InitializeComponent();
            LoadDS(); //Load DS khi form khởi tạo
            LoadFullData(); // Tải toàn bộ dữ liệu chi nhánh
            DisplayPage(currentPage);
            this.taiKhoan = taiKhoan;
        }

        //Load DS
        public void LoadDS()
        {
            dgvDSChiNhanh.AutoGenerateColumns = false; //Không tự động sinh cột
            dgvDS_NhanVien.AutoGenerateColumns = false; //Không tự động sinh cột

            dgvDSChiNhanh.DataSource = ChiNhanhBLL.GetAllChiNhanh();
        }

        //Clear input
        private void ClearInput()
        {
            txtMaCN.Text = "";
            txtTenCN.Text = "";
            txtDiaChi.Text = "";
            txtNguoiQuanLy.Text = "";
            groupDSNhanVien.Text = "Danh Sách Nhân Viên Của Chi Nhánh:";
            dgvDS_NhanVien.DataSource = null; // Xóa dữ liệu trong DataGridView danh sách nhân viên

            txtSearch.Text = ""; // Xóa dữ liệu trong ô tìm kiếm
        }

        private void btnReLoad_Click(object sender, EventArgs e)
        {
            LoadFullData();                  // Tải lại toàn bộ dữ liệu để phân trang
            currentPage = 1;                 // Quay về trang đầu tiên
            DisplayPage(currentPage);       // Hiển thị lại dữ liệu theo trang
            dgvDS_NhanVien.DataSource = null;
            ClearInput();                    // Xóa ô nhập
        }


        private void dgvDSChiNhanh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu chỉ số hàng hợp lệ và không phải header
            if (e.RowIndex >= 0 && dgvDSChiNhanh.Rows[e.RowIndex] != null)
            {
                DataGridViewRow row = dgvDSChiNhanh.Rows[e.RowIndex];

                // Kiểm tra ô không null trước khi truy cập giá trị
                txtMaCN.Text = row.Cells["MaCN"]?.Value?.ToString() ?? string.Empty;
                txtTenCN.Text = row.Cells["TenChiNhanh"]?.Value?.ToString() ?? string.Empty;
                txtDiaChi.Text = row.Cells["DiaChi"]?.Value?.ToString() ?? string.Empty;
                txtNguoiQuanLy.Text = row.Cells["NguoiQuanLy"]?.Value?.ToString() ?? string.Empty;

                groupDSNhanVien.Text = "Danh Sách Nhân Viên Của Chi Nhánh " + txtMaCN.Text + ":";

                dgvDS_NhanVien.DataSource = NhanVienBLL.GetNhanVienByMaCN(txtMaCN.Text);
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaCN.Text) || string.IsNullOrEmpty(txtTenCN.Text) || string.IsNullOrEmpty(txtDiaChi.Text) || string.IsNullOrEmpty(txtNguoiQuanLy.Text))
            {
                MessageBox.Show("Vui lòng chọn chi nhánh cần sửa thông tin");
                return;
            }
            // Kiểm tra xem người dùng có chắc chắn muốn sửa thông tin chi nhánh không
            if (DialogResult.OK == MessageBox.Show("Xác nhận sửa thông tin chi nhánh "+ txtTenCN.Text, "Thông báo.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                ChiNhanhDTO cn = new ChiNhanhDTO(int.Parse(txtMaCN.Text), txtTenCN.Text, txtDiaChi.Text, txtNguoiQuanLy.Text);
                if (ChiNhanhBLL.EditChiNhanh(cn))
                {
                    // Lưu lịch sử
                    LichSuThaoTacDTO lichSu = new LichSuThaoTacDTO
                    {
                        MaTaiKhoan = taiKhoan.MaTaiKhoan,
                        ThoiGian = DateTime.Now,
                        ThaoTac = "Cập nhật thông tin chi nhánh",
                        DoiTuong = "Chi nhánh: " + txtTenCN.Text
                    };
                    LichSuThaoTacBLL.ThemLichSuThaoTac(lichSu);

                    MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Cập nhật thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                btnReLoad_Click(sender, e); // Tải lại dữ liệu sau khi thêm mới
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtSearch.Text))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm");
                return;
            }
            string keyword = txtSearch.Text.Trim();
            List<ChiNhanhDTO> searchResults = ChiNhanhBLL.SearchChiNhanh(keyword);
            if (searchResults.Count == 0)
            {
                MessageBox.Show("Không tìm thấy chi nhánh nào với từ khóa: " + keyword, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvDSChiNhanh.DataSource = null; // Xóa dữ liệu trong DataGridView
            }
            else
            {
                dgvDSChiNhanh.DataSource = searchResults;
                dgvDS_NhanVien.DataSource = null; // Xóa dữ liệu trong DataGridView danh sách nhân viên
                ClearInput(); // Xóa dữ liệu trong các ô nhập
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenCN.Text) || string.IsNullOrEmpty(txtDiaChi.Text) || string.IsNullOrEmpty(txtNguoiQuanLy.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin chi nhánh");
                return;
            }
            // Kiểm tra xem người dùng có chắc chắn muốn thêm chi nhánh không
            if (DialogResult.OK == MessageBox.Show("Xác nhận thêm chi nhánh " + txtTenCN.Text, "Thông báo.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                ChiNhanhDTO cn = new ChiNhanhDTO(0, txtTenCN.Text, txtDiaChi.Text, txtNguoiQuanLy.Text);
                if (ChiNhanhBLL.AddChiNhanh(cn))
                {
                    // Lưu lịch sử
                    LichSuThaoTacDTO lichSu = new LichSuThaoTacDTO
                    {
                        MaTaiKhoan = taiKhoan.MaTaiKhoan,
                        ThoiGian = DateTime.Now,
                        ThaoTac = "Thêm chi nhánh mới",
                        DoiTuong = "Chi nhánh: " + txtTenCN.Text
                    };
                    LichSuThaoTacBLL.ThemLichSuThaoTac(lichSu);

                    MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }                    
                else
                    MessageBox.Show("Thêm thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnReLoad_Click(sender, e);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaCN.Text))
            {
                MessageBox.Show("Vui lòng chọn chi nhánh cần xóa");
                return;
            }
            // Kiểm tra xem người dùng có chắc chắn muốn xóa chi nhánh không
            if (DialogResult.OK == MessageBox.Show("Xác nhận xóa chi nhánh " + txtTenCN.Text, "Thông báo.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                var ds_NhanVien = NhanVienBLL.GetNhanVienByMaCN(txtMaCN.Text);
                if (ds_NhanVien.Count > 0)
                {
                    MessageBox.Show("Không thể xóa chi nhánh này vì có nhân viên đang làm việc tại đây.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (ChiNhanhBLL.DeleteChiNhanh(Convert.ToInt32(txtMaCN.Text)))
                {
                    // Lưu lịch sử
                    LichSuThaoTacDTO lichSu = new LichSuThaoTacDTO
                    {
                        MaTaiKhoan = taiKhoan.MaTaiKhoan,
                        ThoiGian = DateTime.Now,
                        ThaoTac = "Xóa chi nhánh",
                        DoiTuong = "Chi nhánh: " + txtTenCN.Text
                    };
                    LichSuThaoTacBLL.ThemLichSuThaoTac(lichSu);
                    
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Xóa thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                btnReLoad_Click(sender, e); // Tải lại dữ liệu sau khi xóa
                ClearInput(); // Xóa dữ liệu trong các ô nhập
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearInput(); // Xóa dữ liệu trong các ô nhập
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            Reports.ChiNhanh.ChiNhanhReport frm = new Reports.ChiNhanh.ChiNhanhReport();
            frm.ShowDialog();
        }
    }
}
