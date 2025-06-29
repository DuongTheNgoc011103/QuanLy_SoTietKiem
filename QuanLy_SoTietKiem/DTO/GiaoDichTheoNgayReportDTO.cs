using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy_SoTietKiem.DTO
{
    public class GiaoDichTheoNgayReportDTO
    {
        public int MaGD { get; set; }
        public string LoaiGiaoDich { get; set; }
        public decimal SoTien { get; set; }
        public DateTime NgayGD { get; set; }
        public float? LaiSuatApDung { get; set; } // Tỷ lệ lãi suất
        public string GhiChu { get; set; }
        public string HoTenKH { get; set; } // Tên Khách hàng (từ JOIN KhachHang)
        public string CMND_CCCD { get; set; } // CMND/CCCD Khách hàng (từ JOIN KhachHang)
        public string TenNhanVien { get; set; } // Tên Nhân viên thực hiện (từ JOIN NhanVien)
        public int MaSo { get; set; } // Mã sổ tiết kiệm liên quan
        public string TenLoaiSo { get; set; } // Tên loại sổ tiết kiệm (từ JOIN LoaiSoTietKiem)
       

        public GiaoDichTheoNgayReportDTO(int maGD, string loaiGiaoDich, decimal soTien, DateTime ngayGD, float? laiSuatApDung, string ghiChu, string hoTenKH, string cMND_CCCD, string tenNhanVien, int maSo, string tenLoaiSo)
        {
            MaGD = maGD;
            LoaiGiaoDich = loaiGiaoDich;
            SoTien = soTien;
            NgayGD = ngayGD;
            LaiSuatApDung = laiSuatApDung;
            GhiChu = ghiChu;
            HoTenKH = hoTenKH;
            CMND_CCCD = cMND_CCCD;
            TenNhanVien = tenNhanVien;
            MaSo = maSo;
            TenLoaiSo = tenLoaiSo;
        }


        // Constructor with parameters
        public GiaoDichTheoNgayReportDTO() { }
    }

}
