using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy_SoTietKiem.DTO
{
    public class TaiKhoanDTO
    {
        public int MaTaiKhoan { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string QuyenHan { get; set; }
        public bool TrangThai { get; set; }
        public int? MaNV { get; set; }
        public string HoTen { get; set; }

        public TaiKhoanDTO(int maTaiKhoan, string tenDangNhap, string matKhau, string quyenHan, bool trangThai, int? maNV)
        {
            MaTaiKhoan = maTaiKhoan;
            TenDangNhap = tenDangNhap;
            MatKhau = matKhau;
            QuyenHan = quyenHan;
            TrangThai = trangThai;
            MaNV = maNV;
        }

        public TaiKhoanDTO(int maTaiKhoan, string tenDangNhap, string matKhau, string quyenHan, bool trangThai, int? maNV, string hoTen)
        {
            MaTaiKhoan = maTaiKhoan;
            TenDangNhap = tenDangNhap;
            MatKhau = matKhau;
            QuyenHan = quyenHan;
            TrangThai = trangThai;
            MaNV = maNV;
            HoTen = hoTen;
        }

        public TaiKhoanDTO() { }

    }
}
