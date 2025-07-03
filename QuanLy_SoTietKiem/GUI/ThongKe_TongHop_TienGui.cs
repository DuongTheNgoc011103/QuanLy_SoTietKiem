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
using System.Globalization; // Để định dạng tiền tệ


namespace QuanLy_SoTietKiem.GUI
{
    public partial class ThongKe_TongHop_TienGui: Form
    {
        public ThongKe_TongHop_TienGui()
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
                // Gọi BLL để lấy dữ liệu báo cáo
                List<TongHopTienGuiReportDTO> reportData = GiaoDichTietKiemBLL.GetTongHopTienGui(tuNgay, denNgay);

                // Tính tổng tiền gửi và tổng số lượng giao dịch cho các tham số báo cáo
                decimal tongTienGuiTatCa = 0;
                int tongSoGiaoDichTatCa = 0;

                if (reportData != null && reportData.Count > 0)
                {
                    foreach (var item in reportData)
                    {
                        tongTienGuiTatCa += item.TongTien;
                        tongSoGiaoDichTatCa += item.SoLuongGiaoDich;
                    }
                }

                // Chuẩn bị ReportViewer
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.ReportEmbeddedResource = "QuanLy_SoTietKiem.Reports.ThongKe_BaoCao.TongHopTienGuiReport.rdlc"; // Đường dẫn đến file RDLC của bạn

                // Thiết lập các tham số cho báo cáo
                ReportParameter[] parameters = new ReportParameter[]
                {
                    new ReportParameter("ParamTuNgay", tuNgay.ToString("dd/MM/yyyy")),
                    new ReportParameter("ParamDenNgay", denNgay.ToString("dd/MM/yyyy")),
                    new ReportParameter("ParamTongTienGuiTatCa", tongTienGuiTatCa.ToString("N0", new CultureInfo("vi-VN")) + " ₫"),
                    new ReportParameter("ParamTongSoGiaoDichTatCa", tongSoGiaoDichTatCa.ToString("N0"))
                };
                reportViewer1.LocalReport.SetParameters(parameters);

                // Tạo ReportDataSource và gán dữ liệu
                // "DataSet_TongHopTienGui" phải khớp với tên DataSet trong RDLC
                ReportDataSource rds = new ReportDataSource("DataSet_TongHopTienGui", reportData);
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
