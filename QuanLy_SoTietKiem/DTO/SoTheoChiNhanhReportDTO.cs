using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy_SoTietKiem.DTO
{
    public class SoTheoChiNhanhReportDTO
    {
        public int MaCN { get; set; }
        public string TenChiNhanh { get; set; }
        public string NguoiQuanLy { get; set; } // Người quản lý chi nhánh
        public int TongSoSo { get; set; } // Tổng số sổ đang hoạt động của chi nhánh
        public decimal TongSoTienGoc { get; set; } // Tổng tiền gốc của các sổ đang hoạt động của chi nhánh

        public SoTheoChiNhanhReportDTO(int maCN, string tenChiNhanh, string nguoiQuanLy, int tongSoSo, decimal tongSoTienGoc)
        {
            MaCN = maCN;
            TenChiNhanh = tenChiNhanh;
            NguoiQuanLy = nguoiQuanLy;
            TongSoSo = tongSoSo;
            TongSoTienGoc = tongSoTienGoc;
        }

        public SoTheoChiNhanhReportDTO() { }
    }
}
