using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy_SoTietKiem.DTO
{
    public class NhanVienDTO
    {
        public int MaNV { get; set; }
        public string HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string CMND_CCCD { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public string ChucVu { get; set; }
        public string PhongBan { get; set; }
        public int MaCN { get; set; }
        public string TenChiNhanh { get; set; }

        public NhanVienDTO(int maNV, string hoTen, DateTime? ngaySinh, string cmnd_cccd,
                           string diaChi, string dienThoai, string email,
                           string chucVu, string phongBan, int maCN)
        {
            MaNV = maNV;
            HoTen = hoTen;
            NgaySinh = ngaySinh;
            CMND_CCCD = cmnd_cccd;
            DiaChi = diaChi;
            DienThoai = dienThoai;
            Email = email;
            ChucVu = chucVu;
            PhongBan = phongBan;
            MaCN = maCN;
        }

        public NhanVienDTO(int maNV, string hoTen, string chucVu = null, string email = null)
        {
            MaNV = maNV;
            HoTen = hoTen;
            ChucVu = chucVu;
            Email = email;
        }

        public NhanVienDTO(string hoTen, string chucVu, string email, string phongBan, string diaChi, string dienThoai, string tenChiNhanh, string cmnd_cccd, DateTime? ngaySinh)
        {
            HoTen = hoTen;
            ChucVu = chucVu;
            Email = email;
            PhongBan = phongBan;
            DiaChi = diaChi;
            DienThoai = dienThoai;
            TenChiNhanh = tenChiNhanh;
            CMND_CCCD = cmnd_cccd;
            NgaySinh = ngaySinh;
        }

        public NhanVienDTO() { }
    }
}