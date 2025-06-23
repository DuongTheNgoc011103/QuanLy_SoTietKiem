using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy_SoTietKiem.DTO
{
    public class KhachHangDTO
    {
        public int MaKH { get; set; }
        public string HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string CMND_CCCD { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public DateTime? NgayCap { get; set; }
        public bool TrangThai { get; set; }

        public KhachHangDTO(int maKH, string hoTen, DateTime? ngaySinh, string cmnd_cccd,
                           string diaChi, string dienThoai, string email,
                           DateTime? ngayCap, bool trangThai)
        {
            MaKH = maKH;
            HoTen = hoTen;
            NgaySinh = ngaySinh;
            CMND_CCCD = cmnd_cccd;
            DiaChi = diaChi;
            DienThoai = dienThoai;
            Email = email;
            NgayCap = ngayCap;
            TrangThai = trangThai;
        }

        public KhachHangDTO() { }
    }
}
