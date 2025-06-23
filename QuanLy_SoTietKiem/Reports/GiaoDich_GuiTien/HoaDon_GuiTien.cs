using Microsoft.Reporting.WinForms;
using QuanLy_SoTietKiem.BLL;
using QuanLy_SoTietKiem.DTO;
using QuanLy_SoTietKiem.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLy_SoTietKiem.Reports.GiaoDich_GuiTien
{
    public partial class HoaDon_GuiTien: Form
    {
        private int maGiaoDich;
        public HoaDon_GuiTien(int maGiaoDich)
        {
            InitializeComponent();
            this.maGiaoDich = maGiaoDich;
        }

        private void HoaDon_GuiTien_Load(object sender, EventArgs e)
        {
            // Lấy chi tiết giao dịch theo MaGD
            GiaoDichTietKiemDTO chiTiet = GiaoDichTietKiemBLL.GetGiaoDichByMaGD(maGiaoDich);
            if (chiTiet == null)
            {
                MessageBox.Show("Không tìm thấy giao dịch.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<GiaoDichTietKiemDTO> ds = new List<GiaoDichTietKiemDTO> { chiTiet };

            string soTienBangChu = TienHelper.DocTienBangChu(chiTiet.SoTien);

            ReportDataSource rds = new ReportDataSource("DataSET_GiaoDich_GuiTien", ds);
            this.reportViewer.LocalReport.ReportEmbeddedResource = "QuanLy_SoTietKiem.Reports.GiaoDich_GuiTien.HoaDon_GuiTien_Report.rdlc";
            ReportParameter param = new ReportParameter("SoTienBangChu", soTienBangChu);
            this.reportViewer.LocalReport.SetParameters(param);
            this.reportViewer.LocalReport.DataSources.Clear();
            this.reportViewer.LocalReport.DataSources.Add(rds);
            this.reportViewer.LocalReport.Refresh();
            this.reportViewer.RefreshReport();
        }
    }
}
