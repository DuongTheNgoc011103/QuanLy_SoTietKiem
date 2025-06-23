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
    public partial class QuanLy_DSKhachHang: Form
    {
        private string oldDienThoai = "";
        private string oldCMND_CCCD = "";
        private string oldEmail = "";
        private bool oldTrangThai;

        int currentPage = 1;
        int pageSize = 15;
        int totalRows = 0;
        int totalPages = 0;

        List<DTO.KhachHangDTO> fullData = new List<DTO.KhachHangDTO>();

        public void LoadFullData()
        {
            fullData = BLL.KhachHangBLL.GetAllKhachHang();
            totalRows = fullData.Count;
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
        }

        private void DisplayPage(int page)
        {
            int start = (page - 1) * pageSize;
            var pageData = fullData.Skip(start).Take(pageSize).ToList();
            
            dgvDSKhachHang.AutoGenerateColumns = false;
            dgvDSKhachHang.DataSource = pageData;

            txtPage.Text = currentPage.ToString();
            txtTotalPages.Text = totalPages.ToString();
            txtTotal.Text = totalRows.ToString();
        }

        private TaiKhoanDTO taiKhoan;
        public QuanLy_DSKhachHang(TaiKhoanDTO taiKhoan)
        {
            InitializeComponent();
            this.taiKhoan = taiKhoan;
        }

        private void QuanLy_DSKhachHang_Load(object sender, EventArgs e)
        {
            LoadFullData();
            DisplayPage(currentPage);
            LoadCbxTrangThai(); // Tải danh sách trạng thái vào ComboBox
        }

        // Load CBX Trang Thai
        private void LoadCbxTrangThai()
        {
            // Tải danh sách trạng thái
            cbxTrangThai.DataSource = new List<string> { "True", "False" };
            cbxTrangThai.SelectedIndex = -1; // Không chọn mặc định
        }

        private void dgvDSKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvDSKhachHang.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvDSKhachHang.Rows[e.RowIndex];

                txtMaKH.Text = selectedRow.Cells["MaKH"]?.Value?.ToString() ?? string.Empty;
                txtHoTen.Text = selectedRow.Cells["HoTen"]?.Value?.ToString() ?? string.Empty;

                if (selectedRow.Cells["NgaySinh"]?.Value != null && DateTime.TryParse(selectedRow.Cells["NgaySinh"].Value.ToString(), out DateTime ngaySinh))
                {
                    dtpNgaySinh.Value = ngaySinh;
                }
                else
                {
                    dtpNgaySinh.Value = DateTime.Now;
                }

                txtCMND_CCCD.Text = selectedRow.Cells["CMND_CCCD"]?.Value?.ToString() ?? string.Empty;
                oldCMND_CCCD = txtCMND_CCCD.Text; // Lưu giá trị cũ để kiểm tra khi sửa

                if (selectedRow.Cells["NgayCap"]?.Value != null && DateTime.TryParse(selectedRow.Cells["NgayCap"].Value.ToString(), out DateTime ngayCap))
                {
                    dtpNgayCap.Value = ngayCap;
                }
                else
                {
                    dtpNgaySinh.Value = DateTime.Now;
                }

                txtDienThoai.Text = selectedRow.Cells["DienThoai"]?.Value?.ToString() ?? string.Empty;
                oldDienThoai = txtDienThoai.Text; // Lưu giá trị cũ để kiểm tra khi sửa

                txtDiaChi.Text = selectedRow.Cells["DiaChi"]?.Value?.ToString() ?? string.Empty;

                txtEmail.Text = selectedRow.Cells["Email"]?.Value?.ToString() ?? string.Empty;
                oldEmail = txtEmail.Text; // Lưu giá trị cũ để kiểm tra khi sửa

                cbxTrangThai.SelectedItem = selectedRow.Cells["TrangThai"]?.Value?.ToString() ?? string.Empty;
                oldTrangThai = Convert.ToBoolean(cbxTrangThai.SelectedItem); // Lưu trạng thái cũ để kiểm tra khi sửa
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

        // Xóa dữ liệu trong các ô nhập
        private void ClearInput()
        {
            txtMaKH.Clear();
            txtHoTen.Clear();
            dtpNgaySinh.Value = DateTime.Now;
            txtCMND_CCCD.Clear();
            dtpNgayCap.Value = DateTime.Now;
            txtDienThoai.Clear();
            txtDiaChi.Clear();
            txtEmail.Clear();
            cbxTrangThai.SelectedIndex = -1; // Đặt lại trạng thái

            txtSearch.Clear(); // Xóa ô tìm kiếm
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearInput(); // Xóa dữ liệu trong các ô nhập
        }

        private void btnReLoad_Click(object sender, EventArgs e)
        {
            LoadFullData();                  // Tải lại toàn bộ dữ liệu để phân trang
            currentPage = 1;                 // Quay về trang đầu tiên
            DisplayPage(currentPage);       // Hiển thị lại dữ liệu theo trang
            ClearInput();                    // Xóa ô nhập
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm");
                return;
            }
            string keyword = txtSearch.Text.Trim();
            List<KhachHangDTO> searchResults = KhachHangBLL.SearchKhachHangByName(keyword);
            if (searchResults.Count == 0)
            {
                MessageBox.Show("Không tìm thấy khách hàng nào với từ khóa: " + keyword, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvDSKhachHang.DataSource = null; // Xóa dữ liệu trong DataGridView
            }
            else
            {
                dgvDSKhachHang.DataSource = searchResults;
                ClearInput(); // Xóa dữ liệu trong các ô nhập
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(txtHoTen.Text) || string.IsNullOrEmpty(txtCMND_CCCD.Text) || cbxTrangThai.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (KhachHangBLL.GetKhachHangByCMND_CCCD(txtCMND_CCCD.Text.Trim()))
            {
                MessageBox.Show("CMND_CCCD đã tồn tại trong hệ thống");
                txtCMND_CCCD.Focus();
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                // Kiểm tra định dạng email
                if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
                {
                    MessageBox.Show("Email không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return;
                }

                // Kiểm tra email đã tồn tại chưa
                if (KhachHangBLL.GetKhachHangByEmail(txtEmail.Text.Trim()))
                {
                    MessageBox.Show("Email đã tồn tại trong hệ thống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return;
                }
            }

            // Kiểm tra ngày sinh
            if (dtpNgaySinh.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày sinh không hợp lệ");
                dtpNgaySinh.Focus();
                return;
            }

            // Kiểm tra ngày cấp
            if (dtpNgayCap.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày cấp không hợp lệ");
                dtpNgayCap.Focus();
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtDienThoai.Text))
            {
                // Kiểm tra SDT
                if (!string.IsNullOrEmpty(txtDienThoai.Text) && (txtDienThoai.Text.Length < 10 || txtDienThoai.Text.Length > 11 || !txtDienThoai.Text.All(char.IsDigit)))
                {
                    MessageBox.Show("Số điện thoại không hợp lệ");
                    txtDienThoai.Focus();
                    return;
                }

                // Kiểm tra xem số điện thoại đã tồn tại chưa
                if (KhachHangBLL.GetKhachHangBySDT(txtDienThoai.Text))
                {
                    MessageBox.Show("Số điện thoại đã tồn tại trong hệ thống");
                    txtDienThoai.Focus();
                    return;
                }
            }

            // Kiểm tra xem người dùng có chắc chắn muốn thêm chi nhánh không
            if (DialogResult.OK == MessageBox.Show("Xác nhận thêm mới khách hàng " + txtHoTen.Text, "Thông báo.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                // Tạo đối tượng khách hàng mới
                KhachHangDTO newKhachHang = new KhachHangDTO
                {
                    HoTen = txtHoTen.Text,
                    NgaySinh = dtpNgaySinh.Value,
                    CMND_CCCD = txtCMND_CCCD.Text,
                    DiaChi = txtDiaChi.Text ?? null,
                    DienThoai = txtDienThoai.Text ?? null,
                    Email = txtEmail.Text ?? null,
                    NgayCap = dtpNgayCap.Value,
                    TrangThai = Convert.ToBoolean(cbxTrangThai.SelectedItem)
                };

                if (KhachHangBLL.AddKhachHang(newKhachHang))
                {
                    // Lưu lịch sử
                    LichSuThaoTacDTO lichSu = new LichSuThaoTacDTO
                    {
                        MaTaiKhoan = taiKhoan.MaTaiKhoan,
                        ThoiGian = DateTime.Now,
                        ThaoTac = "Thêm khách hàng mới",
                        DoiTuong = "Khách Hàng: " + txtHoTen.Text
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
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(txtHoTen.Text) || string.IsNullOrEmpty(txtCMND_CCCD.Text) || cbxTrangThai.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (KhachHangBLL.CMND_CCCD_Edit(txtCMND_CCCD.Text.Trim(), oldCMND_CCCD))
            {
                MessageBox.Show("CMND_CCCD đã tồn tại trong hệ thống");
                txtCMND_CCCD.Focus();
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                // Kiểm tra định dạng email
                if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
                {
                    MessageBox.Show("Email không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return;
                }

                // Kiểm tra email đã tồn tại chưa (trong trường hợp sửa thông tin)
                if (KhachHangBLL.Email_Edit(txtEmail.Text.Trim(), oldEmail))
                {
                    MessageBox.Show("Email đã tồn tại trong hệ thống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return;
                }
            }

            // Kiểm tra ngày sinh
            if (dtpNgaySinh.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày sinh không hợp lệ");
                dtpNgaySinh.Focus();
                return;
            }

            // Kiểm tra ngày cấp
            if (dtpNgayCap.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày cấp không hợp lệ");
                dtpNgayCap.Focus();
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtDienThoai.Text))
            {
                // Kiểm tra SDT
                if (!string.IsNullOrEmpty(txtDienThoai.Text) && (txtDienThoai.Text.Length < 10 || txtDienThoai.Text.Length > 11 || !txtDienThoai.Text.All(char.IsDigit)))
                {
                    MessageBox.Show("Số điện thoại không hợp lệ");
                    txtDienThoai.Focus();
                    return;
                }

                // Kiểm tra xem số điện thoại đã tồn tại chưa (trong trường hợp sửa thông tin)
                if (KhachHangBLL.DienThoai_Edit(txtDienThoai.Text.Trim(), oldDienThoai))
                {
                    MessageBox.Show("Số điện thoại đã tồn tại trong hệ thống");
                    txtDienThoai.Focus();
                    return;
                }
            }

            // Kiểm tra xem người dùng có chắc chắn muốn thêm chi nhánh không
            if (DialogResult.OK == MessageBox.Show("Xác nhận sửa thông tin khách hàng " + txtHoTen.Text, "Thông báo.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                // Tạo đối tượng khách hàng mới
                KhachHangDTO newKhachHang = new KhachHangDTO
                {
                    MaKH = Convert.ToInt32(txtMaKH.Text),
                    HoTen = txtHoTen.Text,
                    NgaySinh = dtpNgaySinh.Value,
                    CMND_CCCD = txtCMND_CCCD.Text,
                    DiaChi = txtDiaChi.Text ?? null,
                    DienThoai = txtDienThoai.Text ?? null,
                    Email = txtEmail.Text ?? null,
                    NgayCap = dtpNgayCap.Value,
                    TrangThai = Convert.ToBoolean(cbxTrangThai.SelectedItem)
                };

                if (KhachHangBLL.UpdateKhachHang(newKhachHang))
                {
                    // Lưu lịch sử
                    LichSuThaoTacDTO lichSu = new LichSuThaoTacDTO
                    {
                        MaTaiKhoan = taiKhoan.MaTaiKhoan,
                        ThoiGian = DateTime.Now,
                        ThaoTac = "Cập nhật thông tin khách hàng",
                        DoiTuong = "Khách Hàng: " + txtHoTen.Text
                    };
                    LichSuThaoTacBLL.ThemLichSuThaoTac(lichSu);

                    MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (!oldTrangThai)
                    {
                        // ✅ Cập nhật trạng thái các sổ tiết kiệm theo trạng thái khách hàng
                        string trangThaiSo = newKhachHang.TrangThai ? "Đang hoạt động" : "Ngưng hoạt động";
                        SoTietKiemBLL.CapNhatTrangThaiSoTietKiem(newKhachHang.MaKH, trangThaiSo);

                    }
                }
                else
                    MessageBox.Show("Cập nhật thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnReLoad_Click(sender, e); // Tải lại dữ liệu sau khi thêm mới
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKH.Text.Trim()))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa");
                return;
            }

            // Kiểm tra xem người dùng có chắc chắn muốn xóa khách hàng không
            if (DialogResult.OK == MessageBox.Show("Xác nhận xóa khách hàng " + txtHoTen.Text, "Thông báo.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                int maNV = Convert.ToInt32(txtMaKH.Text);
                if (KhachHangBLL.DeleteKhachHang(maNV))
                {
                    // Lưu lịch sử
                    LichSuThaoTacDTO lichSu = new LichSuThaoTacDTO
                    {
                        MaTaiKhoan = taiKhoan.MaTaiKhoan,
                        ThoiGian = DateTime.Now,
                        ThaoTac = "Xóa khách hàng",
                        DoiTuong = "Khách Hàng: " + txtHoTen.Text
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
            Reports.KhachHang.KhachHangReport frm = new Reports.KhachHang.KhachHangReport();
            frm.ShowDialog();
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
    }
}
