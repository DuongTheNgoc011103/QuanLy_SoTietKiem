using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy_SoTietKiem.DTO
{
    public class ChiNhanhDTO
    {
        public int MaCN { get; set; }
        public string TenChiNhanh { get; set; }
        public string DiaChi { get; set; }
        public string NguoiQuanLy { get; set; }
        
        public ChiNhanhDTO(int maChiNhanh, string tenChiNhanh, string diaChi, string nguoiQuanLy)
        {
            MaCN = maChiNhanh;
            TenChiNhanh = tenChiNhanh;
            DiaChi = diaChi;
            NguoiQuanLy = nguoiQuanLy;
        }

        public ChiNhanhDTO() { }
    }
}
