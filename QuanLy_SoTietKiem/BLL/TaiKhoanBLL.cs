using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QuanLy_SoTietKiem.DAL;
using QuanLy_SoTietKiem.Utils;
using QuanLy_SoTietKiem.DTO;

namespace QuanLy_SoTietKiem.BLL
{
    public class TaiKhoanBLL
    {
        // Đang nhập hệ thống
        public static bool DangNhap(string tenDN, string matKhauGoc)
        {
            string matKhauMaHoa = MaHoa.MaHoaMD5(matKhauGoc);
            return TaiKhoanDAL.KiemTraDangNhap(tenDN, matKhauMaHoa);
        }

        // Lấy thông tin tài khoản sau khi đăng nhập thành công
        public static TaiKhoanDTO DangNhapLayThongTin(string tenDN, string matKhauGoc)
        {
            string matKhauMaHoa = MaHoa.MaHoaMD5(matKhauGoc);
            return TaiKhoanDAL.LayThongTinDangNhap(tenDN, matKhauMaHoa);
        }

        // Cập nhật mật khẩu cho tài khoản
        public static bool CapNhatMatKhau(string tenDN, string matKhauMoiMaHoa)
        {
            return TaiKhoanDAL.CapNhatMatKhauTheoTenDangNhap(tenDN, matKhauMoiMaHoa);
        }

        // Lấy danh sách tài khoản người dùng
        public static List<TaiKhoanDTO> LayDanhSachTaiKhoan()
        {
            return TaiKhoanDAL.GetAllTaiKhoan();
        }

        // Search tài khoản theo tên đăng nhập
        public static List<TaiKhoanDTO> TimKiemTaiKhoan(string tenDangNhap)
        {
            return TaiKhoanDAL.SearchTaiKhoanByTenDangNhap(tenDangNhap);
        }

        // Kiểm tra tên đăng nhập đã tồn tại chưa
        public static bool KiemTraTenDangNhapTonTai(string tenDangNhap)
        {
            return TaiKhoanDAL.GetTaiKhoanByTenDN(tenDangNhap);
        }

        // Kiểm tra tên đăng nhập đã tồn tại chưa (trong trường hợp sửa thông tin)
        public static bool TenDN_Edit(string tenDangNhap, string oldTenDangNhap)
        {
            return TaiKhoanDAL.TenDN_Edit(tenDangNhap, oldTenDangNhap);
        }

        // Thêm tài khoản mới
        public static bool ThemTaiKhoan(TaiKhoanDTO taiKhoan)
        {
            // Mã hóa mật khẩu trước khi lưu vào cơ sở dữ liệu
            taiKhoan.MatKhau = MaHoa.MaHoaMD5(taiKhoan.MatKhau);
            return TaiKhoanDAL.ThemTaiKhoan(taiKhoan);
        }

        // Cập nhật tài khoản
        public static bool SuaTaiKhoan(TaiKhoanDTO taiKhoan)
        {
            // Mã hóa mật khẩu trước khi lưu vào cơ sở dữ liệu
            taiKhoan.MatKhau = MaHoa.MaHoaMD5(taiKhoan.MatKhau);
            return TaiKhoanDAL.SuaTaiKhoan(taiKhoan);
        }

        // Xóa tài khoản
        public static bool XoaTaiKhoan(int maTaiKhoan)
        {
            return TaiKhoanDAL.XoaTaiKhoan(maTaiKhoan);
        }

        // Lấy thông tin tài khoản theo mã nhân viên
        public void GetMaNVByMaTaiKhoan(int maTaiKhoan)
        {
            TaiKhoanDAL.GetMaNVByMaTaiKhoan(maTaiKhoan);
        }

        // Kiểm tra xem tài khoản có thuộc về nhân viên nào không
        public static bool KiemTraTaiKhoanCuaNhanVien(int maNV)
        {
            return TaiKhoanDAL.CountTaiKhoanByMaNV(maNV) > 0;
        }
    }
}
