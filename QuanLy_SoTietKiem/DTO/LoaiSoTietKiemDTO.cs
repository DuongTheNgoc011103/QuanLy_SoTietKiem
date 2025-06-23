using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy_SoTietKiem.DTO
{
    public class LoaiSoTietKiemDTO
    {
        public int MaLoai { get; set; }
        public string TenLoai { get; set; }
        public float LaiSuatCoBan { get; set; }
        public string MoTa { get; set; }
        public float PhiRutTruocHan { get; set; }
        public int KyHanYeuCau { get; set; }

        public LoaiSoTietKiemDTO(int maLoai, string tenLoai, float laiSuatCoBan, string moTa, float phiRutTruocHan, int kyHanYeuCau)
        {
            MaLoai = maLoai;
            TenLoai = tenLoai;
            LaiSuatCoBan = laiSuatCoBan;
            MoTa = moTa;
            PhiRutTruocHan = phiRutTruocHan;
            KyHanYeuCau = kyHanYeuCau;
        }

        public LoaiSoTietKiemDTO(int maLoai, string tenLoai)
        {
            MaLoai = maLoai;
            TenLoai = tenLoai;
        }

        public LoaiSoTietKiemDTO() { }
    }
}
