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
    public partial class GiaoDich_RutTien: Form
    {
        private TaiKhoanDTO taiKhoan;

        private bool isChoseMethod = false; // đúng nếu đã chọn phương thức rút tiền

        private string trangThai_STK = ""; // Biến để lưu trạng thái của sổ tiết kiệm (mặc định là rỗng)
        private int maLoai = 0;
        private int maSo_TK = 0; // Biến để lưu mã số tiết kiệm
        private decimal soTienRut = 0; // Biến để lưu số tiền rút
        private string ghiChu = ""; // Biến để lưu ghi chú rút tiền
        private bool isRutToanBo = false; // Biến để xác định có rút toàn bộ hay không

        int currentPage_STK = 1;
        int pageSize_STK = 5;
        int totalRows_STK = 0;
        int totalPages_STK = 0;
        List<SoTietKiemDTO> fullList_STK = new List<SoTietKiemDTO>();

        // Danh sách giao dịch gửi tiền
        int currentPage = 1;
        int pageSize = 10;
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

        public GiaoDich_RutTien(TaiKhoanDTO taiKhoan)
        {
            InitializeComponent();
            this.taiKhoan = taiKhoan;
        }

        private void GiaoDich_RutTien_Load(object sender, EventArgs e)
        {
            LoadFullData();
            currentPage = 1;
            DisplayPage(currentPage);

            txtSoTienRut.Enabled = false;
            txtGhiChu.Enabled = false;
        }

        private void btnRutMotPhan_Click(object sender, EventArgs e)
        {
            isChoseMethod = true;
            txtSoTienRut.Enabled = true;
            txtGhiChu.Enabled = true;
            txtGhiChu.Text = "Rút một phần tiền tiết kiệm"; // Lưu ghi chú nếu có

            btnRutToanBo.Enabled = false; // Vô hiệu hóa nút rút toàn bộ khi đã chọn rút một phần

            if (string.IsNullOrEmpty(txtMaSo.Text.Trim()))
            {
                MessageBox.Show("Vui lòng chọn sổ tiết kiệm trước khi rút tiền!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                isChoseMethod = false; // reset trạng thái nếu không chọn sổ
                txtSoTienRut.Enabled = false; // vô hiệu hóa nhập tiền rút
                txtGhiChu.Enabled = false; // vô hiệu hóa ghi chú
                btnRutToanBo.Enabled = true; // Bật lại nút rút toàn bộ
                return;
            }

            if(trangThai_STK == "Ngưng hoạt động")
            {
                MessageBox.Show("Sổ tiết kiệm này đã ngưng hoạt động, không thể rút tiền!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                isChoseMethod = false; // reset trạng thái nếu sổ không hợp lệ
                txtSoTienRut.Enabled = false; // vô hiệu hóa nhập tiền rút
                txtGhiChu.Enabled = false; // vô hiệu hóa ghi chú
                btnRutToanBo.Enabled = true; // Bật lại nút rút toàn bộ
                return;
            }
        }

        private void btnRutToanBo_Click(object sender, EventArgs e)
        {
            isRutToanBo = true; // Đánh dấu là rút toàn bộ

            isChoseMethod = true;
            txtSoTienRut.Enabled = false; // nếu rút toàn bộ không cần nhập
            txtGhiChu.Enabled = true;
            btnXemTruoc.Enabled = false; // Bật nút xem trước khi rút toàn bộ

            txtGhiChu.Text = "Rút toàn bộ tiền tiết kiệm"; // Lưu ghi chú nếu có

            btnRutMotPhan.Enabled = false; // Vô hiệu hóa nút rút một phần khi đã chọn rút toàn bộ

            decimal soTienRutValue = soTienRut;
            txtSoTienRut.Text = soTienRutValue.ToString(); // Hiển thị số tiền rút toàn bộ vào ô nhập tiền rút
            decimal soTienRutInput = Convert.ToDecimal(soTienRutValue);

            if (string.IsNullOrEmpty(txtMaSo.Text.Trim()))
            {
                MessageBox.Show("Vui lòng chọn sổ tiết kiệm trước khi rút tiền!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                isChoseMethod = false; // reset trạng thái nếu không chọn sổ
                txtSoTienRut.Enabled = false; // vô hiệu hóa nhập tiền rút
                txtGhiChu.Enabled = false; // vô hiệu hóa ghi chú
                btnRutMotPhan.Enabled = true; // Bật lại nút rút một phần
                return;
            }

            if(trangThai_STK == "Ngưng hoạt động")
            {
                MessageBox.Show("Sổ tiết kiệm này đã ngưng hoạt động, không thể rút tiền!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                isChoseMethod = false; // reset trạng thái nếu sổ không hợp lệ
                txtSoTienRut.Enabled = false; // vô hiệu hóa nhập tiền rút
                txtGhiChu.Enabled = false; // vô hiệu hóa ghi chú
                btnRutMotPhan.Enabled = true; // Bật lại nút rút một phần
                return;
            }

            // Tính lãi thực tế và hiện thị tổng tiền nhận được
            DateTime ngayRut = DateTime.Now;
            SoTietKiemDTO soKetQua = SoTietKiemBLL.TinhLaiDuKien(maSo_TK, ngayRut, soTienRutInput);
            if (soKetQua != null)
            {
                decimal tongTienNhan = soKetQua.SoTienGoc + soKetQua.LaiDuKien;
                txtTongTienNhan.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0.00} ₫", tongTienNhan);
                txtLaiThucTe.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0.00} ₫", soKetQua.LaiDuKien);
            }
            else
            {
                txtTongTienNhan.Text = "0 ₫";
                txtLaiThucTe.Text = "0 ₫";
            }
        }

        private void btnRutTien_Click(object sender, EventArgs e)
        {
            if (!isChoseMethod)
            {
                MessageBox.Show("Vui lòng chọn phương thức rút tiền (một phần hoặc toàn bộ) trước khi thao tác!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Dòng này cần parse txtSoTienRut, KHÔNG phải lblSoTienHienTai
            string soTienRutTextInput = txtSoTienRut.Text.Trim();
            if (string.IsNullOrEmpty(soTienRutTextInput))
            {
                MessageBox.Show("Vui lòng nhập số tiền rút!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoTienRut.Focus();
                return;
            }
            if (!decimal.TryParse(txtSoTienRut.Text, out decimal soTienRut))
            {
                MessageBox.Show("Số tiền rút không hợp lệ. Vui lòng nhập số tiền hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoTienRut.Focus();
                return;
            }

            // Lấy thông tin sổ hiện tại để kiểm tra số dư gốc.
            SoTietKiemDTO currentSoTietKiem = SoTietKiemBLL.GetSoTietKiemChiTiet(maSo_TK);

            if (soTienRut > currentSoTietKiem.SoTienGoc)
            {
                MessageBox.Show("Số tiền rút không được lớn hơn số tiền hiện tại trong sổ tiết kiệm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoTienRut.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtTongTienNhan.Text))
            {
                MessageBox.Show("Vui lòng xem trước số tiền nhận được trước khi rút tiền!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal soTienGiaoDich = soTienRut; // Assigning a value to the variable to fix CS0165

            if (DialogResult.OK == MessageBox.Show("Xác nhận rút tiền từ sổ tiết kiệm này!", "Thông báo.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                int newMaGD = BLL.GiaoDichTietKiemBLL.GetMaxMaGD() + 1; // Lấy mã giao dịch mới

                float laiSuatApDung = LoaiSoTietKiemBLL.GetLaiSuatByMaLoai(maLoai);

                // Parse lại các giá trị từ textbox hiển thị để đảm bảo format chuẩn cho báo cáo
                decimal laiThucTeHienThi = 0;
                decimal.TryParse(txtLaiThucTe.Text.Replace(" ₫", "").Trim(), NumberStyles.Currency, new CultureInfo("vi-VN"), out laiThucTeHienThi);

                decimal tongTienNhanHienThi = 0;
                decimal.TryParse(txtTongTienNhan.Text.Replace(" ₫", "").Trim(), NumberStyles.Currency, new CultureInfo("vi-VN"), out tongTienNhanHienThi);

                GiaoDichTietKiemDTO giaoDich = new GiaoDichTietKiemDTO
                {
                    MaSo = int.Parse(txtMaSo.Text),
                    LoaiGiaoDich = "Rút tiền từ sổ tiết kiệm",
                    SoTien = soTienGiaoDich,
                    NgayGD = dtpNgayGD.Value,
                    LaiSuatApDung = laiSuatApDung,
                    GhiChu = txtGhiChu.Text.Trim() + " với Lãi thực tế: " + laiThucTeHienThi + ". Tổng tiền nhận: " + tongTienNhanHienThi,
                    MaNV = taiKhoan.MaNV // Lấy mã nhân viên từ tài khoản đăng nhập
                };
                if (BLL.GiaoDichTietKiemBLL.ThemGiaoDich(giaoDich))
                {
                    if (isRutToanBo)
                    {
                        // Cập nhật trạng thái sổ tiết kiệm là "Đã đóng" nếu rút toàn bộ
                        SoTietKiemBLL.UpdateTrangThaiSoTietKiem(int.Parse(txtMaSo.Text.Trim()), "Ngưng hoạt động");
                    }

                    // Cập nhật số tiền gốc trong sổ tiết kiệm
                    SoTietKiemBLL.CapNhatSoTienGocKhiRut(int.Parse(txtMaSo.Text.Trim()), Convert.ToDecimal(txtSoTienRut.Text));

                    if (isRutToanBo)
                    {
                        ghiChu = "Rút toàn bộ tiền tiết kiệm"; // Cập nhật ghi chú nếu rút toàn bộ
                    }
                    else
                    {
                        ghiChu = "Rút một phần tiền tiết kiệm"; // Cập nhật ghi chú nếu rút một phần
                    }

                    // Lưu lịch sử
                    LichSuThaoTacDTO lichSu = new LichSuThaoTacDTO
                    {
                        MaTaiKhoan = taiKhoan.MaTaiKhoan,
                        ThoiGian = DateTime.Now,
                        ThaoTac = txtGhiChu.Text.Trim() + " với Lãi thực tế: " + laiThucTeHienThi + ". Tổng tiền nhận: " + tongTienNhanHienThi,
                        DoiTuong = "Sổ tiết kiệm: " + int.Parse(txtMaSo.Text.Trim())
                    };
                    LichSuThaoTacBLL.ThemLichSuThaoTac(lichSu);

                    MessageBox.Show("Thực hiện giao dịch rút tiền thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btnReLoad_Click(sender, e); // Tải lại dữ liệu
                    btnClear_Click(sender, e); // Xóa các trường nhập liệu

                    // Hiển thị báo cáo giao dịch rút tiền
                    Reports.GiaoDich_RutTien.HoaDon_RutTien frmReport = new Reports.GiaoDich_RutTien.HoaDon_RutTien(
                        newMaGD,
                        tongTienNhanHienThi, // Tổng tiền nhận
                        laiThucTeHienThi // Lãi thực tế
                    );
                    frmReport.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Thực hiện giao dịch rút tiền thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnReLoad_Click(object sender, EventArgs e)
        {
            isRutToanBo = false; // reset trạng thái rút toàn bộ
            isChoseMethod = false; // reset trạng thái chọn phương thức
                                   // Disable nhập liệu
            txtSoTienRut.Enabled = false;
            txtGhiChu.Enabled = false;

            txtSoTienRut.Clear();
            txtGhiChu.Clear();

            txtTongTienNhan.Clear();
            txtLaiThucTe.Clear();

            txtNhanVien.Clear();
            txtMaGD.Clear();
            dtpNgayGD.Value = DateTime.Now;
            lblSoTienGui.Text = "0 ₫";

            txtSearch.Clear();

            LoadFullData();
            currentPage = 1;
            DisplayPage(currentPage);

            // Reset trạng thái của các nút
            btnRutMotPhan.Enabled = true;
            btnRutToanBo.Enabled = true;
            btnXemTruoc.Enabled = true; // Bật lại nút xem trước
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

                txtMaSo.Text = row.Cells["MaSo_TK"]?.Value?.ToString();
                maSo_TK = Convert.ToInt32(txtMaSo.Text); // Lưu mã số tiết kiệm để sử dụng sau này

                txtHoTen.Text = row.Cells["HoTen"]?.Value?.ToString();
                maLoai = Convert.ToInt32(row.Cells["MaLoai_STK"]?.Value ?? 0);
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

                soTienRut = soTienGoc; // Lưu số tiền gốc để sử dụng sau này

                lbl_SoTienBangChu.Text = TienHelper.DocTienBangChu(soTienGoc);

                int KyHan = Convert.ToInt32(row.Cells["KyHan_STK"]?.Value ?? 0);
                if (KyHan > 0)
                {
                    dtp_NgayDaoHan.Value = dtpNgayMo.Value.AddMonths(KyHan);
                }
                else
                {
                    dtp_NgayDaoHan.Value = DateTime.Now; // Nếu không có kỳ hạn, đặt ngày đáo hạn là ngày hiện tại
                }

                DateTime ngayDaoHan = dtp_NgayDaoHan.Value;

                SoTietKiemDTO soKetQua = SoTietKiemBLL.TinhLaiDuKien(maSo_TK, ngayDaoHan, soTienGoc);

                if (soKetQua != null)
                {
                    txtLaiSuatDuKien.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0.00} ₫", soKetQua.LaiDuKien);
                }
                else
                {
                    // Xóa hoặc đặt về 0 nếu không tìm thấy sổ hoặc lỗi
                    txtLaiSuatDuKien.Text = "0 ₫";
                }
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

            dtp_NgayDaoHan.Value = DateTime.Now;
            txtLaiSuatDuKien.Clear();

            btnReLoad_Click(sender, e); // Tải lại dữ liệu giao dịch gửi tiền

            txtSearch.Clear();
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
            txtSoTienRut.Clear();
            txtGhiChu.Clear();

            txtLaiThucTe.Clear();
            txtTongTienNhan.Clear();
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

        private void btnXemTruoc_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSoTienRut.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập số tiền rút trước khi xem trước!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoTienRut.Focus();
                return;
            }

            string soTienRutValue = txtSoTienRut.Text.Trim();
            decimal soTienRutInput = Convert.ToDecimal(soTienRutValue);


            DateTime ngayDaoHan = dtp_NgayDaoHan.Value;

            SoTietKiemDTO soKetQua = SoTietKiemBLL.TinhLaiDuKien(maSo_TK, ngayDaoHan, soTienRutInput);

            if (soKetQua != null)
            {
                txtTongTienNhan.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0.00} ₫", soKetQua.TongSoTienCuoiKy);
                txtLaiThucTe.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0.00} ₫", soKetQua.LaiDuKien);
            }
            else
            {
                // Xóa hoặc đặt về 0 nếu không tìm thấy sổ hoặc lỗi
                txtTongTienNhan.Text = "0 ₫";
                txtLaiThucTe.Text = "0 ₫";
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
