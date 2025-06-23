using Microsoft.Reporting.WinForms;
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

namespace QuanLy_SoTietKiem.Reports.GiaoDich_RutTien
{
    public partial class HoaDon_RutTien: Form
    {
        private int maGiaoDich;
        private decimal tongTienNhan;
        private decimal laiThucTe;

        public HoaDon_RutTien(int maGiaoDich, decimal tongTienNhan, decimal laiThucTe)
        {
            InitializeComponent();
            this.maGiaoDich = maGiaoDich;
            this.tongTienNhan = tongTienNhan;
            this.laiThucTe = laiThucTe;
        }

        private void HoaDon_RutTien_Load(object sender, EventArgs e)
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

            DateTime ngayRutTien = chiTiet.NgayGD.HasValue ? chiTiet.NgayGD.Value : DateTime.Now;
            SoTietKiemDTO soKetQua = SoTietKiemBLL.TinhLaiDuKien(chiTiet.MaSo, ngayRutTien, chiTiet.SoTien);

            laiThucTe = Convert.ToDecimal(soKetQua.LaiDuKien);
            string laiThucTeString = laiThucTe.ToString("N0", new CultureInfo("vi-VN"));

            tongTienNhan = Convert.ToDecimal(soKetQua.TongSoTienCuoiKy);
            string tongTienNhanString = tongTienNhan.ToString("N0", new CultureInfo("vi-VN"));

            ReportDataSource rds = new ReportDataSource("DataSET_GiaoDich_RutTien", ds);
            this.reportViewer.LocalReport.ReportEmbeddedResource = "QuanLy_SoTietKiem.Reports.GiaoDich_RutTien.HoaDon_RutTien_Report.rdlc";

            ReportParameter[] reportParams = new ReportParameter[]
            {
                new ReportParameter("LaiThucTe", laiThucTeString),
                new ReportParameter("SoTienBangChu", soTienBangChu),
                new ReportParameter("TongSoTienNhan", tongTienNhanString)
            };

            this.reportViewer.LocalReport.SetParameters(reportParams);

            this.reportViewer.LocalReport.DataSources.Clear();
            this.reportViewer.LocalReport.DataSources.Add(rds);
            this.reportViewer.LocalReport.Refresh();
            this.reportViewer.RefreshReport();
        }
    }
}
