using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy_SoTietKiem.DTO
{
    public class GiaoDichTietKiemDTO
    {
        public int MaGD { get; set; }
        public int MaSo { get; set; }
        public string LoaiGiaoDich { get; set; }
        public decimal SoTien { get; set; }
        public DateTime? NgayGD { get; set; }
        public float? LaiSuatApDung { get; set; }
        public string GhiChu { get; set; }
        public int? MaNV { get; set; }
        public string HoTen { get; set; } // Thêm thuộc tính TenNV để lưu tên nhân viên

        // Report

        public string TenLoai { get; set; } // Tên loại sổ tiết kiệm
        public string HoTen_KH { get; set; } // Họ tên khách hàng
        public string CMND_CCCD { get; set; }
        public string DiaChi_KH { get; set; }
        public string HoTen_NV { get; set; } // Họ tên nhân viên


        public GiaoDichTietKiemDTO(int maGD, int maSo, string loaiGiaoDich, decimal soTien,
                                   DateTime? ngayGD, float? laiSuatApDung, string ghiChu, int? maNV, 
                                   string hoTen, string tenLoai, string hoTen_KH, string CMND_CCCD, 
                                   string diaChi_KH, string hoTen_NV )
        {
            MaGD = maGD;
            MaSo = maSo;
            LoaiGiaoDich = loaiGiaoDich;
            SoTien = soTien;
            NgayGD = ngayGD;
            LaiSuatApDung = laiSuatApDung;
            GhiChu = ghiChu;
            MaNV = maNV;
            HoTen = hoTen;
            TenLoai = tenLoai;
            HoTen_KH = hoTen_KH;
            this.CMND_CCCD = CMND_CCCD;
            DiaChi_KH = diaChi_KH;
            HoTen_NV = hoTen_NV;
        }

        public GiaoDichTietKiemDTO() { }
    }
}
