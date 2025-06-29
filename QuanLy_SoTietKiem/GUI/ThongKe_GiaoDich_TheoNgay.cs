using Microsoft.Reporting.WinForms;
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

namespace QuanLy_SoTietKiem.GUI
{
    public partial class ThongKe_GiaoDich_TheoNgay: Form
    {
        public ThongKe_GiaoDich_TheoNgay()
        {
            InitializeComponent();

            // Thiết lập định dạng cho DateTimePicker nếu cần
            dtp_StartDate.Format = DateTimePickerFormat.Custom;
            dtp_StartDate.CustomFormat = "dd/MM/yyyy";

            // Thiết lập định dạng cho DateTimePicker nếu cần
            dtp_EndDate.Format = DateTimePickerFormat.Custom;
            dtp_EndDate.CustomFormat = "dd/MM/yyyy";

            // Đặt giá trị mặc định cho ngày (ví dụ: tháng hiện tại)
            dtp_EndDate.Value = DateTime.Today;
            dtp_StartDate.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        }

        private void ThongKe_GiaoDich_TheoNgay_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            DateTime tuNgay = dtp_StartDate.Value;
            DateTime denNgay = dtp_EndDate.Value;

            if (tuNgay.Date > denNgay.Date)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                List<GiaoDichTheoNgayReportDTO> reportData = GiaoDichTietKiemBLL.GetGiaoDichTheoNgay(tuNgay, denNgay);

                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.ReportEmbeddedResource = "QuanLy_SoTietKiem.Reports.ThongKe_BaoCao.GiaoDichTheoNgayReport.rdlc";

                // Tính tổng tiền gửi và rút với điều kiện đúng
                // LƯU Ý: Các chuỗi "Mở sổ tiết kiệm", "Gửi tiền vào sổ tiết kiệm", "Rút tiền từ sổ tiết kiệm"
                // phải khớp chính xác với dữ liệu trong cột LoaiGiaoDich của DB.
                decimal tongTienGui = reportData
                    .Where(gd => gd.LoaiGiaoDich == "Mở sổ tiết kiệm" || gd.LoaiGiaoDich == "Gửi tiền vào sổ tiết kiệm")
                    .Sum(gd => gd.SoTien);

                decimal tongTienRut = reportData
                    .Where(gd => gd.LoaiGiaoDich == "Rút tiền từ sổ tiết kiệm")
                    .Sum(gd => gd.SoTien);

                ReportParameter[] parameters = new ReportParameter[]
                {
            new ReportParameter("ParamTuNgay", tuNgay.ToString("dd/MM/yyyy")),
            new ReportParameter("ParamDenNgay", denNgay.ToString("dd/MM/yyyy")),
            // Gán các giá trị đã tính toán vào ReportParameter
            new ReportParameter("ParamTongTienGui", tongTienGui.ToString("N0", new CultureInfo("vi-VN")) + " ₫"), // Thêm ký hiệu tiền tệ
            new ReportParameter("ParamTongTienRut", tongTienRut.ToString("N0", new CultureInfo("vi-VN")) + " ₫") // Thêm ký hiệu tiền tệ
                };
                reportViewer1.LocalReport.SetParameters(parameters);

                ReportDataSource rds = new ReportDataSource("DataSet_GiaoDichTheoNgay", reportData);
                reportViewer1.LocalReport.DataSources.Add(rds);

                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
