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
    public partial class QuanLy_SoTietKiem: Form
    {
        private System.Windows.Forms.Timer daoHanTimer;
        private int soSoDaoHanCount = 0; // BIẾN MỚI: Để lưu số lượng sổ đáo hạn

        private int maNV;

        bool isSoTietKiemSelected = false;
        int selectedMaSo = 0; // Biến để lưu mã sổ tiết kiệm được chọn
        int selectedLoaiSTK = 0;

        /// <summary>
        /// Sổ tiết kiệm
        /// </summary>
        int currentPage = 1;
        int pageSize = 15;
        int totalRows = 0;
        int totalPages = 0;

        // Danh sách đầy đủ để phân trang
        List<SoTietKiemDTO> fullData = new List<SoTietKiemDTO>();

        public void LoadFullData()
        {
            fullData = BLL.SoTietKiemBLL.GetAllSoTietKiem();
            totalRows = fullData.Count;
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
        }

        private void DisplayPage(int page)
        {
            int start = (page - 1) * pageSize;
            var pageData = fullData.Skip(start).Take(pageSize).ToList();

            dgvDSSoTietKiem.AutoGenerateColumns = false;
            dgvDSSoTietKiem.DataSource = pageData;

            txtPage.Text = currentPage.ToString();
            txtTotalPages.Text = totalPages.ToString();
            txtTotal.Text = totalRows.ToString();
        }

        /// <summary>
        /// Khách Hàng
        /// </summary>
        int currentPage_KH = 1;
        int pageSize_KH = 10;
        int totalRows_KH = 0;
        int totalPages_KH = 0;

        // Danh sách đầy đủ để phân trang
        List<KhachHangDTO> fullData_KH = new List<KhachHangDTO>();

        public void LoadFullData_KH()
        {
            fullData_KH = BLL.KhachHangBLL.GetAllKhachHang();
            totalRows_KH = fullData_KH.Count;
            totalPages_KH = (int)Math.Ceiling((double)totalRows_KH / pageSize_KH);
        }

        private void DisplayPage_KH(int page)
        {
            int start = (page - 1) * pageSize_KH;
            var pageData = fullData_KH.Skip(start).Take(pageSize_KH).ToList();

            dgvDS_KhachHang.AutoGenerateColumns = false;
            dgvDS_KhachHang.DataSource = pageData;

            txtPage_KH.Text = currentPage_KH.ToString();
            txtTotalPages_KH.Text = totalPages_KH.ToString();
            txtTotal_KH.Text = totalRows_KH.ToString();
        }

        /// <summary>
        /// Loại Sổ Tiết Kiệm
        /// </summary>
        int currentPage_LoaiSTK = 1;
        int pageSize_LoaiSTK = 5;
        int totalRows_LoaiSTK = 0;
        int totalPages_LoaiSTK = 0;

        // Danh sách đầy đủ để phân trang
        List<LoaiSoTietKiemDTO> fullData_LoaiSTK = new List<LoaiSoTietKiemDTO>();

        public void LoadFullData_LoaiSTK()
        {
            fullData_LoaiSTK = BLL.LoaiSoTietKiemBLL.GetAllLoaiSoTietKiem();
            totalRows_LoaiSTK = fullData_LoaiSTK.Count;
            totalPages_LoaiSTK = (int)Math.Ceiling((double)totalRows_LoaiSTK / pageSize_LoaiSTK);
        }

        private void DisplayPage_LoaiSTK(int page)
        {
            int start = (page - 1) * pageSize_LoaiSTK;
            var pageData = fullData_LoaiSTK.Skip(start).Take(pageSize_LoaiSTK).ToList();

            dgv_DSLoaiSTK.AutoGenerateColumns = false;
            dgv_DSLoaiSTK.DataSource = pageData;

            txtPage_LoaiSTK.Text = currentPage_LoaiSTK.ToString();
            txtTotalPages_LoaiSTK.Text = totalPages_LoaiSTK.ToString();
            txtTotal_LoaiSTK.Text = totalRows_LoaiSTK.ToString();
        }


        // Thiết lập bộ đếm thời gian để kiểm tra đáo hạn
        private void SetupDaoHanTimer()
        {
            daoHanTimer = new System.Windows.Forms.Timer();
            daoHanTimer.Interval = 60 * 60 * 1000; // Kiểm tra mỗi 1 giờ (có thể điều chỉnh)
                                                   // daoHanTimer.Interval = 10 * 1000; // Để test, chạy mỗi 10 giây
            daoHanTimer.Tick += DaoHanTimer_Tick;
            daoHanTimer.Start();

            // Chạy kiểm tra ngay khi khởi động
            DaoHanTimer_Tick(null, null);
        }

        private void DaoHanTimer_Tick(object sender, EventArgs e)
        {
            // Kiểm tra và cập nhật trạng thái sổ đáo hạn
            List<int> updatedSoMos = SoTietKiemBLL.CapNhatSoDaoHan();

            if (updatedSoMos.Count > 0)
            {
                soSoDaoHanCount = updatedSoMos.Count; // Cập nhật số lượng sổ đáo hạn

                // Gửi email cho các sổ vừa đáo hạn
                foreach (int maSo in updatedSoMos)
                {
                    SoTietKiemDTO soChiTiet = SoTietKiemBLL.GetSoTietKiemChiTiet(maSo);
                    if (soChiTiet != null && !string.IsNullOrEmpty(KhachHangBLL.GetEmailByMaKH(soChiTiet.MaKH)))
                    {
                        // Gọi hàm gửi mail
                        SendMaturityReminderEmail(soChiTiet);
                    }
                }
            }
        }

        // Trong TrangChu.cs (tiếp nối phần DaoHanTimer_Tick)

        private void SendMaturityReminderEmail(SoTietKiemDTO soTietKiem)
        {
            string subject = $"THÔNG BÁO: Sổ tiết kiệm của quý khách đã ĐÁO HẠN - Mã sổ: {soTietKiem.MaSo}";

            string tenLoai = LoaiSoTietKiemBLL.GetTenLoaiByMaLoai(soTietKiem.MaLoai);
            string email = KhachHangBLL.GetEmailByMaKH(soTietKiem.MaKH);

            // Tạo nội dung email HTML
            StringBuilder bodyBuilder = new StringBuilder();
            bodyBuilder.AppendLine($"<p>Kính gửi Khách hàng: <strong>{soTietKiem.HoTen}</strong>,</p>");
            bodyBuilder.AppendLine("<p>Chúng tôi xin thông báo sổ tiết kiệm của quý khách với các thông tin sau đã chính thức đáo hạn:</p>");
            bodyBuilder.AppendLine("<ul>");
            bodyBuilder.AppendLine($"<li><strong>Mã sổ:</strong> {soTietKiem.MaSo}</li>");
            bodyBuilder.AppendLine($"<li><strong>Loại sổ:</strong> {tenLoai}</li>");
            bodyBuilder.AppendLine($"<li><strong>Ngày mở:</strong> {soTietKiem.NgayMo:dd/MM/yyyy}</li>");
            bodyBuilder.AppendLine($"<li><strong>Kỳ hạn:</strong> {soTietKiem.KyHan} tháng</li>");
            bodyBuilder.AppendLine($"<li><strong>Ngày đáo hạn:</strong> {soTietKiem.NgayDaoHan:dd/MM/yyyy}</li>");
            bodyBuilder.AppendLine($"<li><strong>Số tiền gốc:</strong> {string.Format(new CultureInfo("vi-VN"), "{0:#,##0.00} ₫", soTietKiem.SoTienGoc)}</li>");
            bodyBuilder.AppendLine("</ul>");
            bodyBuilder.AppendLine("<p>Để được tư vấn về các lựa chọn tiếp theo (tái tục, tất toán, gửi mới), quý khách vui lòng liên hệ với chúng tôi tại chi nhánh gần nhất hoặc qua điện thoại.</p>");
            bodyBuilder.AppendLine("<p>Trân trọng cảm ơn!</p>");
            bodyBuilder.AppendLine("<p><strong>Hệ thống Quản lý Sổ Tiết Kiệm</strong></p>");

            if (EmailHelper.SendEmail(email, subject, bodyBuilder.ToString()))
            {
                Console.WriteLine($"Đã gửi email nhắc nhở đáo hạn cho khách hàng {soTietKiem.HoTen} ({email}) về sổ {soTietKiem.MaSo}.");
            }
            else
            {
                Console.WriteLine($"Không thể gửi email nhắc nhở đáo hạn cho khách hàng {soTietKiem.HoTen} ({email}) về sổ {soTietKiem.MaSo}.");
            }
        }

        public QuanLy_SoTietKiem(int maNV)
        {
            InitializeComponent();
            this.maNV = maNV;

            SetupDaoHanTimer(); // Gọi khi khởi tạo form
        }

        private void QuanLy_SoTietKiem_Load(object sender, EventArgs e)
        {
            //SỔ TIẾT KIỆM
            // Tải dữ liệu đầy đủ
            LoadFullData();
            // Hiển thị trang đầu tiên
            DisplayPage(currentPage);

            //Khach Hàng
            // Tải dữ liệu đầy đủ
            LoadFullData_KH();
            // Hiển thị trang đầu tiên
            DisplayPage_KH(currentPage_KH);

            // Loại Sổ Tiết Kiệm
            // Tải dữ liệu đầy đủ
            LoadFullData_LoaiSTK();
            // Hiển thị trang đầu tiên
            DisplayPage_LoaiSTK(currentPage_LoaiSTK);

            CBX_TrangThai();

            btn_Notification.Text = $"Thông báo ({soSoDaoHanCount})"; // Cập nhật số lượng sổ đáo hạn
        }

        // combobox trạng thái
        private void CBX_TrangThai()
        {
            // Tải danh sách trạng thái
            cbxTrangThai.DataSource = new List<string> { "Đang hoạt động", "Ngưng hoạt động", "Đáo hạn" };
            cbxTrangThai.SelectedIndex = -1; // Không chọn mặc định
        }

        // Clear input fields
        private void ClearInputFields()
        {
            txtMaSo.Clear();
            txtKhachHang.Clear();
            txtLoaiSTK.Clear();
            num_SoTienGoc.Value = 0;
            num_KyHan.Value = 0;
            dtpNgayMo.Value = DateTime.Now;
            cbxTrangThai.SelectedIndex = -1; // Không chọn mặc định

            txtSearch.Clear();

            isSoTietKiemSelected = false;
            dgvDS_KhachHang.Enabled = true;
            dgv_DSLoaiSTK.Enabled = true;
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

        private void btnReLoad_Click(object sender, EventArgs e)
        {
            //So Tiết Kiệm
            // Tải lại dữ liệu đầy đủ
            LoadFullData();
            // Hiển thị trang đầu tiên
            currentPage = 1;
            DisplayPage(currentPage);

            // Khach Hang
            // Tải lại dữ liệu đầy đủ
            LoadFullData_KH();
            // Hiển thị trang đầu tiên
            currentPage_KH = 1;
            DisplayPage_KH(currentPage_KH);

            // Loai So Tiet Kiem
            // Tải lại dữ liệu đầy đủ
            LoadFullData_LoaiSTK();
            // Hiển thị trang đầu tiên
            currentPage_LoaiSTK = 1;
            DisplayPage_LoaiSTK(currentPage_LoaiSTK);

            // Xóa dữ liệu trong các ô nhập
            ClearInputFields();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm");
                return;
            }
            string keyword = txtSearch.Text.Trim();
            List<SoTietKiemDTO> searchResults = SoTietKiemBLL.SearchHoTenKhachHang(keyword);
            if (searchResults.Count == 0)
            {
                MessageBox.Show("Không tìm thấy khách hàng nào với từ khóa: " + keyword, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvDSSoTietKiem.DataSource = null; // Xóa dữ liệu trong DataGridView
            }
            else
            {
                dgvDSSoTietKiem.DataSource = searchResults;
                ClearInputFields(); // Xóa dữ liệu trong các ô nhập
            }
        }

        private void dgvDSSoTietKiem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvDSSoTietKiem.Rows.Count)
            {
                isSoTietKiemSelected = true; // Đánh dấu đã chọn sổ tiết kiệm
                dgvDS_KhachHang.Enabled = false;
                dgv_DSLoaiSTK.Enabled = false;

                DataGridViewRow row = dgvDSSoTietKiem.Rows[e.RowIndex];

                // Hiển thị thông tin sổ tiết kiệm được chọn vào các ô nhập
                txtMaSo.Text = row.Cells["MaSoTK"]?.Value?.ToString();
                txtKhachHang.Text = row.Cells["HoTen"]?.Value?.ToString();
                txtLoaiSTK.Text = row.Cells["TenLoai"]?.Value?.ToString();

                if (row.Cells["NgayMo"]?.Value != null && DateTime.TryParse(row.Cells["NgayMo"].Value.ToString(), out DateTime ngayMo))
                {
                    dtpNgayMo.Value = ngayMo;
                }
                else
                {
                    dtpNgayMo.Value = DateTime.Now;
                }

                num_KyHan.Text = row.Cells["KyHan"]?.Value?.ToString();
                num_SoTienGoc.Text = row.Cells["SoTienGoc_STK"]?.Value?.ToString();

                cbxTrangThai.SelectedItem = row.Cells["TrangThai"]?.Value?.ToString();
            }
        }

        private void btnPrev_KH_Click(object sender, EventArgs e)
        {
            if (currentPage_KH > 1)
            {
                currentPage_KH--;
                DisplayPage_KH(currentPage_KH);
            }
        }

        private void btnNext_KH_Click(object sender, EventArgs e)
        {
            if (currentPage_KH < totalPages_KH)
            {
                currentPage_KH++;
                DisplayPage_KH(currentPage_KH);
            }
        }

        private void btnPrev_LoaiSTK_Click(object sender, EventArgs e)
        {
            if (currentPage_LoaiSTK > 1)
            {
                currentPage_LoaiSTK--;
                DisplayPage_LoaiSTK(currentPage_LoaiSTK);
            }
        }

        private void btnNext_LoaiSTK_Click(object sender, EventArgs e)
        {
            if (currentPage_LoaiSTK < totalPages_LoaiSTK)
            {
                currentPage_LoaiSTK++;
                DisplayPage_LoaiSTK(currentPage_LoaiSTK);
            }
        }

        private void dgvDS_KhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!dgvDS_KhachHang.Enabled) return; // Ngăn click khi bị vô hiệu hóa

            if (e.RowIndex >= 0 && e.RowIndex < dgvDS_KhachHang.Rows.Count)
            {
                DataGridViewRow row = dgvDS_KhachHang.Rows[e.RowIndex];
                // Hiển thị thông tin khách hàng được chọn vào các ô nhập
                selectedMaSo = Convert.ToInt32(row.Cells["MaKH_STK"]?.Value);
                txtKhachHang.Text = row.Cells["HoTen_KH"]?.Value?.ToString();
            }
        }

        private void dgv_DSLoaiSTK_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!dgv_DSLoaiSTK.Enabled) return; // Ngăn click khi bị vô hiệu hóa

            if (e.RowIndex >= 0 && e.RowIndex < dgv_DSLoaiSTK.Rows.Count)
            {
                DataGridViewRow row = dgv_DSLoaiSTK.Rows[e.RowIndex];
                // Hiển thị thông tin loại sổ tiết kiệm được chọn vào các ô nhập
                selectedLoaiSTK = Convert.ToInt32(row.Cells["MaLoai_STK"].Value);
                txtLoaiSTK.Text = row.Cells["TenLoai_STK"]?.Value?.ToString();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearInputFields();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (isSoTietKiemSelected == true)
            {
                MessageBox.Show("Vui lòng hủy chọn sổ tiết kiệm hiện tại trước khi thêm mới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtKhachHang.Text) || string.IsNullOrEmpty(txtLoaiSTK.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng và loại sổ tiết kiệm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra ngày mở
            if (dtpNgayMo.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show("Ngày mở sổ tiết kiệm không được lớn hơn ngày hiện tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra số tiền gốc
            if (num_SoTienGoc.Value <= 0)
            {
                MessageBox.Show("Số tiền gốc phải lớn hơn 0", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                num_SoTienGoc.Focus();
                return;
            }

            // Xét Kỳ hạn
            LoaiSoTietKiemDTO selectedLoaiSTKInfo = LoaiSoTietKiemBLL.GetLoaiSoTietKiemById(selectedLoaiSTK);

            // Trường hợp 1: Sổ tiết kiệm không kỳ hạn
            // Bạn cần quy định cách nhận biết "không kỳ hạn" trong bảng LoaiSoTietKiem (ví dụ: TenLoai chứa "không kỳ hạn" hoặc một cột KyHanYeuCau là NULL/0)
            if (selectedLoaiSTKInfo.TenLoai.ToLower().Contains("không kỳ hạn"))
            {
                if (num_KyHan.Value != 0) // Nếu người dùng nhập kỳ hạn cho sổ không kỳ hạn
                {
                    MessageBox.Show("Loại sổ tiết kiệm này là không kỳ hạn. Vui lòng đặt kỳ hạn bằng 0.", "Lỗi Kỳ Hạn", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    num_KyHan.Value = 0; // Đặt lại về 0
                    num_KyHan.Focus();
                    return;
                }
                // Đối với sổ không kỳ hạn, KyHan trong SoTietKiem nên là 0 hoặc NULL.
                // Ở đây, chúng ta sẽ đặt nó là 0 nếu num_KyHan.Value được sử dụng.
            }
            else // Trường hợp 2: Sổ tiết kiệm có kỳ hạn
            {
                // Có thể có nhiều logic phụ ở đây:
                // a) LoaiSoTietKiem có một Kỳ hạn *cố định* (ví dụ: "Tiết kiệm kỳ hạn 3 tháng" nghĩa là KyHan = 3)
                // b) LoaiSoTietKiem có các giá trị Kỳ hạn *cho phép* (ví dụ: "Tiết kiệm có kỳ hạn" cho phép 1, 3, 6, 9, 12 tháng)
                // Để đơn giản và thực tế, giả sử LoaiSoTietKiem có một KyHan cụ thể được định nghĩa cho nó.

                // Giả sử LoaiSoTietKiemDTO có thuộc tính 'KyHan' lưu kỳ hạn chuẩn cho loại đó.
                // Nếu KyHan <= 0 trong LoaiSoTietKiemDTO, có thể hiểu là kỳ hạn linh hoạt nhưng vẫn là có kỳ hạn (người dùng phải nhập số dương).
                if (selectedLoaiSTKInfo.KyHanYeuCau > 0 && num_KyHan.Value != selectedLoaiSTKInfo.KyHanYeuCau)
                {
                    MessageBox.Show($"Loại sổ '{selectedLoaiSTKInfo.TenLoai}' yêu cầu kỳ hạn là {selectedLoaiSTKInfo.KyHanYeuCau} tháng. Vui lòng nhập đúng kỳ hạn.", "Lỗi Kỳ Hạn", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    num_KyHan.Value = selectedLoaiSTKInfo.KyHanYeuCau; // Tự động sửa
                    num_KyHan.Focus();
                    return;
                }
                else if (selectedLoaiSTKInfo.KyHanYeuCau <= 0 && num_KyHan.Value <= 0) // Nếu LoaiSoTietKiem cho phép kỳ hạn linh hoạt nhưng vẫn là có kỳ hạn
                {
                    // Nhánh này có thể dành cho các loại "Tiền gửi có kỳ hạn linh hoạt" mà người dùng chọn từ danh sách (ví dụ: 1, 3, 6, 9, 12)
                    // Trong trường hợp này, bạn có thể cần điền trước các lựa chọn num_KyHan dựa trên LoaiSoTietKiem.
                    // Đối với ví dụ này, hãy đảm bảo nó không phải là 0 hoặc số âm cho một loại tiền gửi có kỳ hạn
                    if (num_KyHan.Value <= 0)
                    {
                        MessageBox.Show("Kỳ hạn không hợp lệ cho loại sổ này. Vui lòng nhập kỳ hạn dương.", "Lỗi Kỳ Hạn", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        num_KyHan.Focus();
                        return;
                    }
                }
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thêm sổ tiết kiệm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SoTietKiemDTO newSoTietKiem = new SoTietKiemDTO
                {
                    MaKH = selectedMaSo, // Mã khách hàng đã chọn
                    MaLoai = selectedLoaiSTK, // Mã loại sổ tiết kiệm đã chọn
                    NgayMo = DateTime.Now, // Ngày mở sổ tiết kiệm là ngày hiện tại

                    KyHan = Convert.ToInt32(num_KyHan.Value),
                    SoTienGoc = Convert.ToDecimal(num_SoTienGoc.Value),

                    TrangThai = cbxTrangThai.SelectedItem?.ToString() ?? "Đang hoạt động" // Mặc định là "Đang hoạt động" nếu không chọn
                };
                if (SoTietKiemBLL.AddSoTietKiem(newSoTietKiem))
                {                    
                    int newMaSo = SoTietKiemBLL.GetMaxMaSo(); // Lấy mã sổ tiết kiệm mới nhất

                    GiaoDichTietKiemDTO newGiaoDich = new GiaoDichTietKiemDTO
                    {
                        MaSo = newMaSo, // Mã sổ tiết kiệm mới
                        MaNV = maNV, // Mã nhân viên hiện tại
                        NgayGD = DateTime.Now, // Ngày giao dịch là ngày hiện tại
                        GhiChu = "Mở sổ tiết kiệm", // Ghi chú giao dịch
                        LaiSuatApDung = selectedLoaiSTKInfo.LaiSuatCoBan, // Lãi suất áp dụng từ loại sổ tiết kiệm
                        SoTien = newSoTietKiem.SoTienGoc, // Số tiền giao dịch là số tiền gốc
                        LoaiGiaoDich = "Mở sổ tiết kiệm" // Loại giao dịch là "Mở sổ tiết kiệm"
                    };
                    // Thêm giao dịch đầu tiên cho sổ tiết kiệm mới
                    GiaoDichTietKiemDAL.ThemGiaoDich(newGiaoDich);

                    // Lưu lịch sử
                    LichSuThaoTacDTO lichSu = new LichSuThaoTacDTO
                    {
                        MaTaiKhoan = maNV,
                        ThoiGian = DateTime.Now,
                        ThaoTac = "Mở sổ tiết kiệm",
                        DoiTuong = "Sổ tiết kiệm: " + newMaSo
                    };
                    LichSuThaoTacBLL.ThemLichSuThaoTac(lichSu);

                    MessageBox.Show("Thêm sổ tiết kiệm và tạo giao dich đầu tiên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    btnReLoad_Click(sender, e); // Tải lại dữ liệu đầy đủ
                }
                else
                {
                    MessageBox.Show("Thêm sổ tiết kiệm thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!isSoTietKiemSelected)
            {
                MessageBox.Show("Vui lòng chọn sổ tiết kiệm cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra số tiền gốc
            if (num_SoTienGoc.Value <= 0)
            {
                MessageBox.Show("Số tiền gốc phải lớn hơn 0", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                num_SoTienGoc.Focus();
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn sửa sổ tiết kiệm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SoTietKiemDTO updatedSoTietKiem = new SoTietKiemDTO
                {
                    MaSo = Convert.ToInt32(txtMaSo.Text),
                    KyHan = Convert.ToInt32(num_KyHan.Value),
                    SoTienGoc = Convert.ToDecimal(num_SoTienGoc.Value),
                    TrangThai = cbxTrangThai.SelectedItem?.ToString()
                };

                if (SoTietKiemBLL.UpdateSoTietKiem(updatedSoTietKiem))
                {
                    // Lưu lịch sử
                    LichSuThaoTacDTO lichSu = new LichSuThaoTacDTO
                    {
                        MaTaiKhoan = maNV,
                        ThoiGian = DateTime.Now,
                        ThaoTac = "Cập nhật sổ tiết kiệm",
                        DoiTuong = "Sổ tiết kiệm: " + txtMaSo.Text
                    };
                    LichSuThaoTacBLL.ThemLichSuThaoTac(lichSu);

                    MessageBox.Show("Cập nhật sổ tiết kiệm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    btnReLoad_Click(sender, e); // Tải lại dữ liệu đầy đủ
                }
                else
                {
                    MessageBox.Show("Cập nhật sổ tiết kiệm thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!isSoTietKiemSelected)
            {
                MessageBox.Show("Vui lòng chọn sổ tiết kiệm cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa sổ tiết kiệm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                int maSo = Convert.ToInt32(txtMaSo.Text);
                if (SoTietKiemBLL.DeleteSoTietKiem(maSo))
                {
                    // Lưu lịch sử
                    LichSuThaoTacDTO lichSu = new LichSuThaoTacDTO
                    {
                        MaTaiKhoan = maNV,
                        ThoiGian = DateTime.Now,
                        ThaoTac = "Xóa sổ tiết kiệm",
                        DoiTuong = "Sổ tiết kiệm: " + txtMaSo.Text
                    };
                    LichSuThaoTacBLL.ThemLichSuThaoTac(lichSu);

                    MessageBox.Show("Xóa sổ tiết kiệm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btnReLoad_Click(sender, e); // Tải lại dữ liệu đầy đủ
                }
                else
                {
                    MessageBox.Show("Xóa sổ tiết kiệm thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            Reports.SoTietKiem.SoTietKiemReport frm = new Reports.SoTietKiem.SoTietKiemReport();
            frm.ShowDialog();
        }

        private void dgvDSSoTietKiem_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridViewRow row = dgvDSSoTietKiem.Rows[e.RowIndex];

            if (row.Cells["TrangThai"].Value != null && row.Cells["TrangThai"].Value.ToString().Trim() == "Ngưng hoạt động")
            {
                row.DefaultCellStyle.BackColor = Color.Gray; // hoặc Color.Red nếu bạn muốn đậm hơn
                row.DefaultCellStyle.ForeColor = Color.White; // chữ trắng cho nổi bật
            }
            else if (row.Cells["TrangThai"].Value != null && row.Cells["TrangThai"].Value.ToString().Trim() == "Đáo hạn")
            {
                row.DefaultCellStyle.ForeColor = Color.Red; // chữ đỏ cho nổi bật
            }
        }

        private void btn_Notification_Click(object sender, EventArgs e)
        {
            if (soSoDaoHanCount > 0)
            {
                MessageBox.Show($"Đã có {soSoDaoHanCount} sổ tiết kiệm đáo hạn và được cập nhật trạng thái.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Hiện tại không có thông báo nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Cập nhật lại số lượng sổ đáo hạn
            btn_Notification.Text = $"Thông báo (0)"; // Cập nhật số lượng sổ đáo hạn
        }
    }
}
