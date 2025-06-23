using QuanLy_SoTietKiem.BLL;
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
    public partial class PhanLoai_KhachHang: Form
    {
        private int maKH = 0; // Mã khách hàng hiện tại, mặc định là 0 (chưa chọn khách hàng nào)
        private int maSo = 0; // Mã sổ tiết kiệm hiện tại, mặc định là 0 (chưa chọn sổ tiết kiệm nào)

        // Load dữ liệu phân trang cho Khách Hàng
        int currentPage = 1;
        int pageSize = 15;
        int totalRows = 0;
        int totalPages = 0;

        List<KhachHangDTO> fullData = new List<KhachHangDTO>();

        public void LoadFullData()
        {
            fullData = BLL.KhachHangBLL.GetKhachHangBySoTietKiem();
            totalRows = fullData.Count;
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
        }

        private void DisplayPage(int page)
        {
            int start = (page - 1) * pageSize;
            var pageData = fullData.Skip(start).Take(pageSize).ToList();

            dgvDSKhachHang.AutoGenerateColumns = false;
            dgvDSKhachHang.DataSource = pageData;

            txtPage_KH.Text = currentPage.ToString();
            txtTotalPages_KH.Text = totalPages.ToString();
            txtToTal_KH.Text = totalRows.ToString();
        }

        // Load dữ liệu phân trang cho Số Tiết Kiệm
        int currentPage_STK = 1;
        int pageSize_STK = 20;
        int totalRows_STK = 0;
        int totalPages_STK = 0;

        List<SoTietKiemDTO> fullData_STK = new List<SoTietKiemDTO>();

        public void LoadFullData_STK()
        {
            fullData_STK = SoTietKiemBLL.GetSoTietKiemByMaKH(maKH);
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
        public PhanLoai_KhachHang(TaiKhoanDTO taiKhoan)
        {
            InitializeComponent();
            this.taiKhoan = taiKhoan;
        }

        private void PhanLoai_KhachHang_Load(object sender, EventArgs e)
        {
            LoadFullData();
            currentPage = 1;
            DisplayPage(currentPage);

            LoadComboBoxTrangThai();
        }

        private void btnPrev_KH_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                DisplayPage(currentPage);
            }
        }

        private void btnNext_KH_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                DisplayPage(currentPage);
            }
        }

        // Clear input fields
        private void ClearInputFields()
        {
            txtCMND_CCCD.Clear();
            txtHoTen.Clear();
            txtDiaChi.Clear();
            cbxTrangThai.SelectedIndex = -1;

            cbxTrangThai_STK.SelectedIndex = -1;
            dtpNgayMo.Value = DateTime.Now;
            dtpNgayDaoHan.Value = DateTime.Now;
            num_SoTienGoc.Value = 0;
            num_KyHan.Value = 0;

            txtSearch.Clear();
            lblMoney.Text = "0 ₫"; // Reset label hiển thị tiền
        }

        private void btnReLoad_Click(object sender, EventArgs e)
        {
            // Tải lại dữ liệu Khách Hàng
            LoadFullData();
            currentPage = 1;
            DisplayPage(currentPage);

            ClearInputFields();

            // Tải lại dữ liệu Sổ Tiết Kiệm
            dgvDS_SoTietKiem.DataSource = null;


            lblLaiDuKien.Text = "0 ₫"; // Reset label hiển thị lãi dự kiến
            lblTongSoTienCuoiKy.Text = "0 ₫"; // Reset label hiển thị tổng số tiền cuối kỳ
            txtHienThi.Text = "0 ₫"; // Reset label hiển thị tiền bằng chữ
            dtpNgayKiemTra.Value = DateTime.Now; // Reset ngày kiểm tra lãi dự kiến
        }

        // Load ComboBox Trang Thai
        private void LoadComboBoxTrangThai()
        {
            cbxTrangThai.DataSource = new List<string> { "True", "False" };
            cbxTrangThai.SelectedIndex = -1; // Không chọn mặc định

            cbxTrangThai_STK.DataSource = new List<string> { "Đang hoạt động", "Đáo hạn", "Ngưng hoạt động" };
        }

        private void dgvDSKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvDSKhachHang.Rows.Count)
            {
                cbxTrangThai_STK.SelectedIndex = -1; // Reset trạng thái sổ tiết kiệm
                num_KyHan.Value = 0; // Reset kỳ hạn
                dtpNgayMo.Value = DateTime.Now; // Reset ngày mở sổ tiết kiệm
                dtpNgayDaoHan.Value = DateTime.Now; // Reset ngày đáo hạn
                lblMoney.Text = "0 ₫"; // Reset label hiển thị tiền
                num_SoTienGoc.Value = 0; // Reset số tiền gốc

                DataGridViewRow row = dgvDSKhachHang.Rows[e.RowIndex];
                maKH = Convert.ToInt32(row.Cells["MaKH"].Value);
                txtCMND_CCCD.Text = row.Cells["CMND_CCCD"]?.Value?.ToString();
                txtHoTen.Text = row.Cells["HoTen_KH"]?.Value?.ToString();
                txtDiaChi.Text = row.Cells["DiaChi"]?.Value?.ToString();
                cbxTrangThai.SelectedItem = row.Cells["TrangThai"]?.Value?.ToString();

                // Hiển thị danh sách Sổ Tiết Kiệm của khách hàng đã chọn
                LoadFullData_STK();
                currentPage_STK = 1;
                DisplayPage_STK(currentPage_STK);
            }
        }

        private void dgvDS_SoTietKiem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvDS_SoTietKiem.Rows.Count)
            {
                DataGridViewRow row = dgvDS_SoTietKiem.Rows[e.RowIndex];
                maSo = Convert.ToInt32(row.Cells["MaSo"].Value);

                dtpNgayMo.Value = Convert.ToDateTime(row.Cells["NgayMo"]?.Value);
                num_SoTienGoc.Value = Convert.ToDecimal(row.Cells["SoTienGoc"]?.Value);
                // Kiểm tra KyHan có phải DBNull không trước khi Convert.ToInt32
                if (row.Cells["KyHan"]?.Value != DBNull.Value)
                {
                    num_KyHan.Value = Convert.ToInt32(row.Cells["KyHan"]?.Value);
                }
                else
                {
                    num_KyHan.Value = 0; // Đặt về 0 hoặc giá trị mặc định cho không kỳ hạn
                }

                cbxTrangThai_STK.SelectedItem = row.Cells["TrangThai_STK"]?.Value?.ToString();

                // Tính toán ngày đáo hạn và hiển thị (chỉ để hiển thị, không lưu vào DB)
                DateTime ngayMo = dtpNgayMo.Value;
                int kyHan = Convert.ToInt32(num_KyHan.Value);
                if (kyHan > 0)
                {
                    dtpNgayDaoHan.Value = ngayMo.AddMonths(kyHan);
                }
                else
                {
                    // Nếu không kỳ hạn, có thể hiển thị một ngày nào đó hoặc để trống/text "Không kỳ hạn"
                    dtpNgayDaoHan.Value = DateTime.Now; // Hoặc một giá trị mặc định nào đó
                }

                decimal soTien = num_SoTienGoc.Value;
                string hienThi = string.Format(new CultureInfo("vi-VN"), "{0:#,##0} ₫", soTien);
                lblMoney.Text = hienThi;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm");
                return;
            }
            string keyword = txtSearch.Text.Trim();
            List<KhachHangDTO> searchResults = KhachHangBLL.SearchKhachHangByNameAndSoTietKiem(keyword);
            if (searchResults.Count == 0)
            {
                MessageBox.Show("Không tìm thấy khách hàng nào với từ khóa: " + keyword, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvDSKhachHang.DataSource = null; // Xóa dữ liệu trong DataGridView
            }
            else
            {
                dgvDSKhachHang.DataSource = searchResults;
                ClearInputFields(); // Xóa dữ liệu trong các ô nhập
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (cbxTrangThai_STK.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn sổ tiết kiệm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (cbxTrangThai_STK.Text == "Đang hoạt động")
            {
                if (DialogResult.Yes == MessageBox.Show("Xác nhận đóng sổ tiết kiệm này?", "Cảnh báo.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    // Lưu lịch sử
                    LichSuThaoTacDTO lichSu = new LichSuThaoTacDTO
                    {
                        MaTaiKhoan = taiKhoan.MaTaiKhoan,
                        ThoiGian = DateTime.Now,
                        ThaoTac = "Đóng sổ tiết kiệm",
                        DoiTuong = "Sổ tiết kiệm: " + maSo + " - Khách hàng: " + txtHoTen.Text.Trim()
                    };
                    LichSuThaoTacBLL.ThemLichSuThaoTac(lichSu);

                    SoTietKiemBLL.UpdateTrangThaiSoTietKiem(maSo, "Ngưng hoạt động");
                    MessageBox.Show("Đóng sổ tiết kiệm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Cập nhật lại danh sách sổ tiết kiệm
                    LoadFullData_STK();
                    currentPage_STK = 1;
                    DisplayPage_STK(currentPage_STK);
                }
            }
            else
            {
                MessageBox.Show("Sổ tiết kiệm đã đóng hoặc đã đáo hạn, không thể đóng lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvDSKhachHang_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridViewRow row = dgvDSKhachHang.Rows[e.RowIndex];

            if (row.Cells["TrangThai"].Value != null && row.Cells["TrangThai"].Value.ToString().Trim() == "False")
            {
                row.DefaultCellStyle.BackColor = Color.Gray; // hoặc Color.Red nếu bạn muốn đậm hơn
                row.DefaultCellStyle.ForeColor = Color.White; // chữ trắng cho nổi bật
            }
        }

        private void dgvDS_SoTietKiem_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridViewRow row = dgvDS_SoTietKiem.Rows[e.RowIndex];

            if (row.Cells["TrangThai_STK"].Value != null && row.Cells["TrangThai_STK"].Value.ToString().Trim() == "Ngưng hoạt động")
            {
                row.DefaultCellStyle.BackColor = Color.Gray; // hoặc Color.Red nếu bạn muốn đậm hơn
                row.DefaultCellStyle.ForeColor = Color.White; // chữ trắng cho nổi bật
            }
            else if (row.Cells["TrangThai_STK"].Value != null && row.Cells["TrangThai_STK"].Value.ToString().Trim() == "Đáo hạn")
            {
                row.DefaultCellStyle.ForeColor = Color.Red; // chữ trắng cho nổi bật
            }
        }

        private void btn_TraCuu_Click(object sender, EventArgs e)
        {
            if(maSo == 0)
            {
                MessageBox.Show("Vui lòng chọn sổ tiết kiệm để tra cứu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime ngayKiemTra = dtpNgayKiemTra.Value;

            SoTietKiemDTO soKetQua = SoTietKiemBLL.TinhLaiDuKien(maSo, ngayKiemTra, num_SoTienGoc.Value);

            if (soKetQua != null)
            {
                txtHienThi.Text = TienHelper.DocTienBangChu(soKetQua.TongSoTienCuoiKy);
                lblLaiDuKien.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0.00} ₫", soKetQua.LaiDuKien);
                lblTongSoTienCuoiKy.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0.00} ₫", soKetQua.TongSoTienCuoiKy);
            }
            else
            {
                // Xóa hoặc đặt về 0 nếu không tìm thấy sổ hoặc lỗi
                lblLaiDuKien.Text = "0 ₫";
                lblTongSoTienCuoiKy.Text = "0 ₫";
                txtHienThi.Text = "0 ₫";
            }
        }
    }
}
