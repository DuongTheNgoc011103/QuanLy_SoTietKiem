using Microsoft.Reporting.WinForms;
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
using System.Globalization; // Để định dạng tiền tệ

namespace QuanLy_SoTietKiem.GUI
{
    public partial class ThongKe_So_TheoChiNhanh: Form
    {
        public ThongKe_So_TheoChiNhanh()
        {
            InitializeComponent();
        }

        private void ThongKe_So_TheoChiNhanh_Load(object sender, EventArgs e)
        {
            LoadReport(); // Gọi hàm để tải báo cáo khi form được load
        }

        private void LoadReport()
        {
            try
            {
                // Gọi BLL để lấy dữ liệu báo cáo
                List<SoTheoChiNhanhReportDTO> reportData = SoTietKiemBLL.GetSoTheoChiNhanh();

                // Chuẩn bị ReportViewer
                reportViewer1.LocalReport.DataSources.Clear(); // Xóa các nguồn dữ liệu cũ
                reportViewer1.LocalReport.ReportEmbeddedResource = "QuanLy_SoTietKiem.Reports.ThongKe_BaoCao.SoTheoChiNhanhReport.rdlc"; // Đường dẫn đến file RDLC của bạn

                ReportDataSource rds = new ReportDataSource("DataSet_SoTheoChiNhanh", reportData);
                reportViewer1.LocalReport.DataSources.Add(rds);

                reportViewer1.RefreshReport(); // Làm mới báo cáo
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
