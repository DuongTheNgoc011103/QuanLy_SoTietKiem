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

namespace QuanLy_SoTietKiem.Reports.LoaiSoTietKiem
{
    public partial class LoaiSoTietKiemReport: Form
    {
        public LoaiSoTietKiemReport()
        {
            InitializeComponent();
        }

        private void LoaiSoTietKiemReport_Load(object sender, EventArgs e)
        {
            List<LoaiSoTietKiemDTO> ds = LoaiSoTietKiemBLL.GetAllLoaiSoTietKiem();

            ReportDataSource rds = new ReportDataSource("DataSET_LoaiSoTietKiem", ds);
            this.reportViewer.LocalReport.ReportEmbeddedResource = "QuanLy_SoTietKiem.Reports.LoaiSoTietKiem.LoaiSoTietKiemReport.rdlc";
            this.reportViewer.LocalReport.DataSources.Clear();
            this.reportViewer.LocalReport.DataSources.Add(rds);
            this.reportViewer.LocalReport.Refresh();
            this.reportViewer.RefreshReport();
            this.reportViewer.RefreshReport();
        }
    }
}
