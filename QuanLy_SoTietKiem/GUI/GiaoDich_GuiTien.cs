using QuanLy_SoTietKiem.BLL;
using QuanLy_SoTietKiem.DTO;
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

using QuanLy_SoTietKiem.Utils;

namespace QuanLy_SoTietKiem.GUI
{
    public partial class GiaoDich_GuiTien: Form
    {
        private TaiKhoanDTO taiKhoan;
        private string trangThai_STK = ""; // Biến để lưu trạng thái của sổ tiết kiệm (mặc định là rỗng)
        private int maLoai = 0;

        int currentPage_STK = 1;
        int pageSize_STK = 10;
        int totalRows_STK = 0;
        int totalPages_STK = 0;
        List<SoTietKiemDTO> fullList_STK = new List<SoTietKiemDTO>();


        // Danh sách giao dịch gửi tiền
        int currentPage = 1;
        int pageSize = 15;
        int totalRows = 0;
        int totalPages = 0;
        List<GiaoDichTietKiemDTO> fullData = new List<GiaoDichTietKiemDTO>();

        public void LoadFullData()
        {
            fullData = BLL.GiaoDichTietKiemBLL.GetGiaoDichGuiTien();
            totalRows = fullData.Count;
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
        }

        private void DisplayPage(int page)
        {
            int start = (page - 1) * pageSize;
            var pageData = fullData.Skip(start).Take(pageSize).ToList();

            dgvDSGiaoDichGuiTienTietKiem.AutoGenerateColumns = false;
            dgvDSGiaoDichGuiTienTietKiem.DataSource = pageData;

            txtPage.Text = currentPage.ToString();
            txtTotalPages.Text = totalPages.ToString();
            txtTotal.Text = totalRows.ToString();
        }

        //
        public GiaoDich_GuiTien(TaiKhoanDTO taiKhoan)
        {
            InitializeComponent();
            this.taiKhoan = taiKhoan;
        }

        private void GiaoDich_GuiTien_Load(object sender, EventArgs e)
        {
            LoadFullData();
            currentPage = 1;
            DisplayPage(currentPage);
        }

        private void DisplayPageSTK(int page)
        {
            if (fullList_STK == null || fullList_STK.Count == 0) return;

            int start = (page - 1) * pageSize_STK;
            var pageData = fullList_STK.Skip(start).Take(pageSize_STK).ToList();

            dgvDS_SoTietKiem.AutoGenerateColumns = false;
            dgvDS_SoTietKiem.DataSource = pageData;

            txtPage_STK.Text = currentPage_STK.ToString();
            txtTotalPages_STK.Text = totalPages_STK.ToString();
            txtTotal_STK.Text = totalRows_STK.ToString();
        }


        private void btnSearchByCMND_CCCD_Click(object sender, EventArgs e)
        {
            string keyword = txtSearchByCMND_CCCD.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Vui lòng nhập CMND/CCCD để tìm kiếm.");
                txtSearchByCMND_CCCD.Focus();
                return;
            }

            fullList_STK = SoTietKiemBLL.GetSoTietKiemByCCCDKH(keyword);
            totalRows_STK = fullList_STK.Count;
            totalPages_STK = (int)Math.Ceiling((double)totalRows_STK / pageSize_STK);
            currentPage_STK = 1;

            if (totalRows_STK == 0)
            {
                MessageBox.Show("Không tìm thấy sổ tiết kiệm nào với CCCD: " + keyword, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvDS_SoTietKiem.DataSource = null;
                groupDS_STK.Text = "Danh sách sổ tiết kiệm";
                txtPage_STK.Text = "0";
                txtTotalPages_STK.Text = "0";
                txtTotal_STK.Text = "0";
            }
            else
            {
                groupDS_STK.Text = $"Danh sách sổ tiết kiệm của {keyword}";
                DisplayPageSTK(currentPage_STK);
            }
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


        private void dgvDS_SoTietKiem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvDS_SoTietKiem.Rows.Count)
            {
                DataGridViewRow row = dgvDS_SoTietKiem.Rows[e.RowIndex];
                
                txtMaSo.Text = row.Cells["MaSo"]?.Value?.ToString();
                txtHoTen.Text = row.Cells["HoTen"]?.Value?.ToString();
                maLoai = Convert.ToInt32(row.Cells["MaLoai"]?.Value ?? 0);
                txtTenLoaiSo.Text = row.Cells["TenLoai_STK"]?.Value?.ToString();

                if (row.Cells["NgayMo"]?.Value != null && DateTime.TryParse(row.Cells["NgayMo"].Value.ToString(), out DateTime ngayMo))
                {
                    dtpNgayMo.Value = ngayMo;
                }
                else
                {
                    dtpNgayMo.Value = DateTime.Now;
                }

                string trangThai = row.Cells["TrangThai"]?.Value?.ToString() ?? "";
                trangThai_STK = trangThai; // Lưu trạng thái của sổ tiết kiệm

                string soTienGocStr = row.Cells["SoTienGoc"]?.Value?.ToString() ?? "0";
                decimal soTienGoc = 0;
                decimal.TryParse(soTienGocStr, out soTienGoc);

                string hienThi = string.Format(new CultureInfo("vi-VN"), "{0:#,##0.00} ₫", soTienGoc);
                lblSoTienHienTai.Text = hienThi;

                lbl_SoTienBangChu.Text = TienHelper.DocTienBangChu(soTienGoc);
            }
        }

        private void btnPrev_STK_Click(object sender, EventArgs e)
        {
            if (currentPage_STK > 1)
            {
                currentPage_STK--;
                DisplayPageSTK(currentPage_STK);
            }
        }

        private void btnNext_STK_Click(object sender, EventArgs e)
        {
            if (currentPage_STK < totalPages_STK)
            {
                currentPage_STK++;
                DisplayPageSTK(currentPage_STK);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearchByCMND_CCCD.Clear();
            dgvDS_SoTietKiem.DataSource = null;
            groupDS_STK.Text = "Danh sách sổ tiết kiệm";
            txtPage_STK.Text = "0";
            txtTotalPages_STK.Text = "0";
            txtTotal_STK.Text = "0";
            txtMaSo.Clear();
            txtHoTen.Clear();
            txtTenLoaiSo.Clear();
            dtpNgayMo.Value = DateTime.Now;
            lblSoTienHienTai.Text = "0 ₫";
            lbl_SoTienBangChu.Text = "Không đồng";
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

        private void dgvDSGiaoDichGuiTienTietKiem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvDSGiaoDichGuiTienTietKiem.Rows.Count)
            {
                DataGridViewRow row = dgvDSGiaoDichGuiTienTietKiem.Rows[e.RowIndex];

                lblSoTienGui.Text = row.Cells["SoTien"]?.Value?.ToString();

                if (row.Cells["NgayGD"]?.Value != null && DateTime.TryParse(row.Cells["NgayGD"].Value.ToString(), out DateTime ngayGD))
                {
                    dtpNgayGD.Value = ngayGD;
                }
                else
                {
                    dtpNgayGD.Value = DateTime.Now;
                }

                txtNhanVien.Text = row.Cells["HoTen_NV"]?.Value?.ToString();
                txtMaGD.Text = row.Cells["MaGD"]?.Value?.ToString();
                txtMaSo_GD.Text = row.Cells["MaSo_GD"]?.Value?.ToString();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtSoTienGui.Clear();
            txtGhiChu.Clear();
        }

        private void btnGuiTien_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSoTienGui.Text))
            {
                MessageBox.Show("Vui lòng nhập số tiền gửi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoTienGui.Focus();
                return;
            }
            if (!decimal.TryParse(txtSoTienGui.Text, out decimal soTienGui) || soTienGui <= 0)
            {
                MessageBox.Show("Số tiền gửi không hợp lệ. Vui lòng nhập số tiền lớn hơn 0.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoTienGui.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtMaSo.Text))
            {
                MessageBox.Show("Vui lòng chọn sổ tiết kiệm để gửi tiền.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if(trangThai_STK == "Ngưng hoạt động")
            {
                MessageBox.Show("Sổ tiết kiệm này đã NGƯNG HOẠT ĐỘNG. Không thể thực hiện giao dịch gửi tiền.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(trangThai_STK == "Đang hoạt động")
            {
                if (DialogResult.OK == MessageBox.Show("Xác nhận gửi tiền vào sổ tiết kiệm này!", "Thông báo.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    float laiSuatApDung = LoaiSoTietKiemBLL.GetLaiSuatByMaLoai(maLoai);

                    GiaoDichTietKiemDTO giaoDich = new GiaoDichTietKiemDTO
                    {
                        MaSo = int.Parse(txtMaSo.Text),
                        LoaiGiaoDich = "Gửi tiền vào sổ tiết kiệm",
                        SoTien = soTienGui,
                        NgayGD = dtpNgayGD.Value,
                        LaiSuatApDung = laiSuatApDung,
                        GhiChu = txtGhiChu.Text.Trim() ?? "Gửi tiền",
                        MaNV = taiKhoan.MaNV // Lấy mã nhân viên từ tài khoản đăng nhập
                    };
                    if (BLL.GiaoDichTietKiemBLL.ThemGiaoDich(giaoDich))
                    {
                        int newMaGD = GiaoDichTietKiemBLL.GetMaxMaGD(); // Lấy mã giao dịch mới nhất

                        // Cập nhật số tiền gốc trong sổ tiết kiệm
                        SoTietKiemBLL.CapNhatSoTienGoc(int.Parse(txtMaSo.Text.Trim()), soTienGui);

                        // Lưu lịch sử
                        LichSuThaoTacDTO lichSu = new LichSuThaoTacDTO
                        {
                            MaTaiKhoan = taiKhoan.MaTaiKhoan,
                            ThoiGian = DateTime.Now,
                            ThaoTac = "Gửi tiền vào sổ tiết kiệm",
                            DoiTuong = "Sổ tiết kiệm: " + int.Parse(txtMaSo.Text.Trim())
                        };
                        LichSuThaoTacBLL.ThemLichSuThaoTac(lichSu);

                        MessageBox.Show("Thêm giao dịch gửi tiền thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        btnReLoad_Click(sender, e); // Tải lại dữ liệu
                        btnClear_Click(sender, e); // Xóa các trường nhập liệu

                        // Hiển thị báo cáo giao dịch gửi tiền
                        Reports.GiaoDich_GuiTien.HoaDon_GuiTien frm = new Reports.GiaoDich_GuiTien.HoaDon_GuiTien(newMaGD);
                        frm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Thêm giao dịch gửi tiền thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if(trangThai_STK == "Đáo hạn")
            {
                if (DialogResult.OK == MessageBox.Show("Sổ tiết kiệm này đã ĐÁO HẠN. Xác nhận tiếp tục gia hạn sổ tiết kiệm này!", "Thông báo.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    float laiSuatApDung = LoaiSoTietKiemBLL.GetLaiSuatByMaLoai(maLoai);

                    GiaoDichTietKiemDTO giaoDich = new GiaoDichTietKiemDTO
                    {
                        MaSo = int.Parse(txtMaSo.Text),
                        LoaiGiaoDich = "Gửi tiền vào sổ tiết kiệm",
                        SoTien = soTienGui,
                        NgayGD = dtpNgayGD.Value,
                        LaiSuatApDung = laiSuatApDung,
                        GhiChu = txtGhiChu.Text.Trim() ?? "Gửi tiền",
                        MaNV = taiKhoan.MaNV // Lấy mã nhân viên từ tài khoản đăng nhập
                    };
                    if (BLL.GiaoDichTietKiemBLL.ThemGiaoDich(giaoDich))
                    {
                        int newMaGD = GiaoDichTietKiemBLL.GetMaxMaGD(); // Lấy mã giao dịch mới nhất

                        // Cập nhật số tiền gốc trong sổ tiết kiệm
                        SoTietKiemBLL.CapNhatSoTienGoc(int.Parse(txtMaSo.Text.Trim()), soTienGui);

                        // Cập nhật Ngày mở sổ tiết kiệm
                        SoTietKiemBLL.CapNhatNgayMo(int.Parse(txtMaSo.Text.Trim()));

                        // Cập nhật trạng thái sổ tiết kiệm
                        SoTietKiemBLL.UpdateTrangThaiSoTietKiem(int.Parse(txtMaSo.Text.Trim()), "Đang hoạt động");

                        // Lưu lịch sử
                        LichSuThaoTacDTO lichSu = new LichSuThaoTacDTO
                        {
                            MaTaiKhoan = taiKhoan.MaTaiKhoan,
                            ThoiGian = DateTime.Now,
                            ThaoTac = "Gửi tiền vào sổ tiết kiệm",
                            DoiTuong = "Sổ tiết kiệm: " + int.Parse(txtMaSo.Text.Trim())
                        };
                        LichSuThaoTacBLL.ThemLichSuThaoTac(lichSu);

                        MessageBox.Show("Thêm giao dịch gửi tiền thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        btnReLoad_Click(sender, e); // Tải lại dữ liệu
                        btnClear_Click(sender, e); // Xóa các trường nhập liệu

                        // Hiển thị báo cáo giao dịch gửi tiền
                        Reports.GiaoDich_GuiTien.HoaDon_GuiTien frm = new Reports.GiaoDich_GuiTien.HoaDon_GuiTien(newMaGD);
                        frm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Thêm giao dịch gửi tiền thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnReLoad_Click(object sender, EventArgs e)
        {
            txtSoTienGui.Clear();
            txtGhiChu.Clear();

            txtNhanVien.Clear();
            txtMaGD.Clear();
            dtpNgayGD.Value = DateTime.Now;
            lblSoTienGui.Text = "0 ₫";

            txtSearch.Clear();

            LoadFullData();
            currentPage = 1;
            DisplayPage(currentPage);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm");
                return;
            }
            string keyword = txtSearch.Text.Trim();
            List<GiaoDichTietKiemDTO> searchResults = GiaoDichTietKiemBLL.SearchGiaoDichByType(keyword);
            if (searchResults.Count == 0)
            {
                MessageBox.Show("Không tìm thấy loại giao dịch nào với từ khóa: " + keyword, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvDSGiaoDichGuiTienTietKiem.DataSource = null; // Xóa dữ liệu trong DataGridView
            }
            else
            {
                dgvDSGiaoDichGuiTienTietKiem.DataSource = searchResults;
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            Reports.GiaoDichTietKiem.GiaoDichTietKiem frm = new Reports.GiaoDichTietKiem.GiaoDichTietKiem();
            frm.ShowDialog();
        }
    }
}
