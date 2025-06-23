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

namespace QuanLy_SoTietKiem.Reports.SoTietKiem
{
    public partial class SoTietKiemReport: Form
    {
        public SoTietKiemReport()
        {
            InitializeComponent();
        }

        private void SoTietKiemReport_Load(object sender, EventArgs e)
        {
            List<SoTietKiemDTO> ds = SoTietKiemBLL.GetAllSoTietKiem();

            ReportDataSource rds = new ReportDataSource("DataSET_SoTietKiem", ds);
            this.reportViewer.LocalReport.ReportEmbeddedResource = "QuanLy_SoTietKiem.Reports.SoTietKiem.SoTietKiemReport.rdlc";
            this.reportViewer.LocalReport.DataSources.Clear();
            this.reportViewer.LocalReport.DataSources.Add(rds);
            this.reportViewer.LocalReport.Refresh();
            this.reportViewer.RefreshReport();
            this.reportViewer.RefreshReport();
        }
    }
}
