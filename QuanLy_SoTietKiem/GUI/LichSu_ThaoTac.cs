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
    public partial class LichSu_ThaoTac: Form
    {
        private int maNV;

        int currentPage = 1;
        int pageSize = 20;
        int totalRows = 0;
        int totalPages = 0;

        List<LichSuThaoTacDTO> fullData = new List<LichSuThaoTacDTO>();

        private void LoadFullData(string thuTuThoiGian = "DESC")
        {
            fullData = LichSuThaoTacBLL.LayDanhSachLichSuThaoTac(thuTuThoiGian);
            totalRows = fullData.Count;
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
        }


        private void DisplayPage(int page)
        {
            int start = (page - 1) * pageSize;
            var pageData = fullData.Skip(start).Take(pageSize).ToList();

            dgvDS_NhatKy_ThaoTac.AutoGenerateColumns = false; // Tắt tự động sinh cột
            dgvDS_NhatKy_ThaoTac.DataSource = pageData;

            txtPage.Text = currentPage.ToString();
            txtTotalPages.Text = totalPages.ToString();
            txtToTal.Text = totalRows.ToString();
        }

        private TaiKhoanDTO taiKhoan;
        public LichSu_ThaoTac(TaiKhoanDTO taiKhoan)
        {
            InitializeComponent();
            this.taiKhoan = taiKhoan;
        }

        private void LoadInformation()
        {
            txtMaNV.Text = "Mã Tài Khoản: " + taiKhoan.MaTaiKhoan.ToString();
            txtHoTen.Text = taiKhoan.HoTen;
            txtQuyenHan.Text = "Quyền Hạn: " + taiKhoan.QuyenHan;
        }

        private void LichSu_ThaoTac_Load(object sender, EventArgs e)
        {
            LoadFullData();
            DisplayPage(currentPage);

            LoadInformation();  // Hiển thị thông tin tài khoản

            rad_ThoiGianGiamDan.Checked = true; // Mặc định sắp xếp theo thời gian giảm dần
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

        private void CLearFields()
        {
            dtp_startDate.Value = DateTime.Now;
            dtp_endDate.Value = DateTime.Now;
            rad_DongSo.Checked = false;
            rad_MoSo.Checked = false;
            rad_RutTien.Checked = false;
            rad_GuiTien.Checked = false;
            rad_ThoiGianTangDan.Checked = false;
        }

        private void btnReLoad_Click(object sender, EventArgs e)
        {
            LoadFullData();                  // Tải lại toàn bộ dữ liệu để phân trang
            currentPage = 1;                 // Quay về trang đầu tiên
            DisplayPage(currentPage);       // Hiển thị lại dữ liệu theo trang

            CLearFields();                  // Xóa các trường lọc

            rad_ThoiGianGiamDan.Checked = true; // Mặc định sắp xếp theo thời gian giảm dần
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if(dtp_startDate.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày hiện tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtp_startDate.Focus();
                return;
            }

            if (dtp_endDate.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày kết thúc không được lớn hơn ngày hiện tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtp_endDate.Focus();
                return;
            }

            if (dtp_endDate.Value < dtp_startDate.Value)
            {
                MessageBox.Show("Ngày kết thúc không được nhỏ hơn ngày bắt đầu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtp_endDate.Focus();
                return;
            }

            DateTime fromDate = dtp_startDate.Value;
            DateTime toDate = dtp_endDate.Value;

            List<LichSuThaoTacDTO> filteredData = LichSuThaoTacBLL.LocLichSuTheoThoiGian(fromDate, toDate);
            MessageBox.Show($"Đã lọc {filteredData.Count} bản ghi từ {fromDate.ToShortDateString()} đến {toDate.ToShortDateString()}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (filteredData.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu trong khoảng thời gian đã chọn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnReLoad_Click(sender, e); // Quay về dữ liệu gốc
                return;
            }
            fullData = filteredData;
            totalRows = fullData.Count;
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
            currentPage = 1; // Quay về trang đầu tiên sau khi lọc
            DisplayPage(currentPage); // Hiển thị dữ liệu đã lọc

        }

        private void rad_DongSo_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_DongSo.Checked)
            {
                if (dgvDS_NhatKy_ThaoTac.RowCount == 0)
                {
                    MessageBox.Show("Không có dữ liệu để lọc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnReLoad_Click(sender, e); // Quay về dữ liệu gốc
                    return;
                }
                else
                {
                    fullData = LichSuThaoTacBLL.LocLichSuTheoThaoTac("Đóng sổ");
                    totalRows = fullData.Count;
                    totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
                    currentPage = 1; // Quay về trang đầu tiên sau khi lọc
                    DisplayPage(currentPage); // Hiển thị dữ liệu đã lọc
                }
            }
        }

        private void rad_MoSo_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_MoSo.Checked)
            {
                if (dgvDS_NhatKy_ThaoTac.RowCount == 0)
                {
                    MessageBox.Show("Không có dữ liệu để lọc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnReLoad_Click(sender, e); // Quay về dữ liệu gốc
                    return;
                }
                else
                {
                    fullData = LichSuThaoTacBLL.LocLichSuTheoThaoTac("Mở sổ");
                    totalRows = fullData.Count;
                    totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
                    currentPage = 1; // Quay về trang đầu tiên sau khi lọc
                    DisplayPage(currentPage); // Hiển thị dữ liệu đã lọc
                }
            }
        }

        private void rad_GuiTien_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_GuiTien.Checked)
            {
                if (dgvDS_NhatKy_ThaoTac.RowCount == 0)
                {
                    MessageBox.Show("Không có dữ liệu để lọc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnReLoad_Click(sender, e); // Quay về dữ liệu gốc
                    return;
                }
                else
                {
                    fullData = LichSuThaoTacBLL.LocLichSuTheoThaoTac("Gửi tiền");
                    totalRows = fullData.Count;
                    totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
                    currentPage = 1; // Quay về trang đầu tiên sau khi lọc
                    DisplayPage(currentPage); // Hiển thị dữ liệu đã lọc
                }
            }
        }

        private void rad_RutTien_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_RutTien.Checked)
            {
                if (dgvDS_NhatKy_ThaoTac.RowCount == 0)
                {
                    MessageBox.Show("Không có dữ liệu để lọc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnReLoad_Click(sender, e); // Quay về dữ liệu gốc
                    return;
                }
                else
                {
                    fullData = LichSuThaoTacBLL.LocLichSuTheoThaoTac("Rút");
                    totalRows = fullData.Count;
                    totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
                    currentPage = 1; // Quay về trang đầu tiên sau khi lọc
                    DisplayPage(currentPage); // Hiển thị dữ liệu đã lọc
                }
            }
        }

        private void rad_ThoiGianTangDan_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_ThoiGianTangDan.Checked)
            {
                LoadFullData("ASC");
                currentPage = 1;
                DisplayPage(currentPage);
            }
        }

        private void rad_ThoiGianGiamDan_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_ThoiGianGiamDan.Checked)
            {
                LoadFullData("DESC");
                currentPage = 1;
                DisplayPage(currentPage);
            }
        }

        private void dgvDS_NhatKy_ThaoTac_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvDS_NhatKy_ThaoTac.Rows.Count)
            {
                DataGridViewRow row = dgvDS_NhatKy_ThaoTac.Rows[e.RowIndex];

                if (row.Cells["ThoiGian"]?.Value != null && DateTime.TryParse(row.Cells["ThoiGian"].Value.ToString(), out DateTime ngayThaoTac))
                {
                    dtpNgayThaoTac.Value = ngayThaoTac;
                }
                else
                {
                    dtpNgayThaoTac.Value = DateTime.Now;
                }

                txtThaoTac.Text = row.Cells["ThaoTac"]?.Value?.ToString();
                txtDoiTuong.Text = row.Cells["DoiTuong"]?.Value?.ToString();
            }
        }
    }
}
