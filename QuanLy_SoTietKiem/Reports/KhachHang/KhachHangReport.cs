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

namespace QuanLy_SoTietKiem.Reports.KhachHang
{
    public partial class KhachHangReport: Form
    {
        public KhachHangReport()
        {
            InitializeComponent();
        }

        private void KhachHangReport_Load(object sender, EventArgs e)
        {
            List<KhachHangDTO> ds = KhachHangBLL.GetAllKhachHang();

            ReportDataSource rds = new ReportDataSource("DataSET_KhachHang", ds);
            this.reportViewer.LocalReport.ReportEmbeddedResource = "QuanLy_SoTietKiem.Reports.KhachHang.KhachHangReport.rdlc";
            this.reportViewer.LocalReport.DataSources.Clear();
            this.reportViewer.LocalReport.DataSources.Add(rds);
            this.reportViewer.LocalReport.Refresh();
            this.reportViewer.RefreshReport();
            this.reportViewer.RefreshReport();
        }
    }
}
