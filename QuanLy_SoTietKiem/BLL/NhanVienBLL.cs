using QuanLy_SoTietKiem.DAL;
using QuanLy_SoTietKiem.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy_SoTietKiem.BLL
{
    public class NhanVienBLL
    {
        // Lấy danh sách nhân viên theo mã chi nhánh
        public static List<NhanVienDTO> GetNhanVienByMaCN(string maCN)
        {
            return NhanVienDAL.GetNhanVienByMaCN(maCN);
        }

        // Lấy HoTen theo mã nhân viên
        public static string GetHoTenByMaNV(int maNV)
        {
            return NhanVienDAL.GetHoTenByMaNV(maNV);
        }

        // Lấy tất cả nhân viên
        public static List<NhanVienDTO> GetAllNhanVien()
        {
            return NhanVienDAL.GetAllNhanVien();
        }

        // Lấy danh sách nhân viên chưa có tài khoản
        public static List<NhanVienDTO> GetNhanVienChuaCoTaiKhoan()
        {
            return NhanVienDAL.GetNhanVienChuaCoTaiKhoan();
        }

        // Thêm nhân viên
        public static bool AddNhanVien(NhanVienDTO nv)
        {
            return NhanVienDAL.AddNhanVien(nv);
        }

        // kiểm tra sdt đã tồn tại hay chưa
        public static bool GetNhanVienBySDT(string dienThoai)
        {
            return NhanVienDAL.GetNhanVienBySDT(dienThoai);
        }

        // Kiểm tra số điện thoại đã tồn tại chưa (trong trường hợp sửa thông tin)
        public static bool DienThoai_Edit(string dienThoai, string oldDienThoai)
        {
            return NhanVienDAL.DienThoai_Edit(dienThoai, oldDienThoai);
        }

        // kiểm tra CMND_CCCD đã tồn tại hay chưa
        public static bool GetNhanVienByCMND_CCCD(string CMND_CCCD)
        {
            return NhanVienDAL.GetNhanVienByCMND_CCCD(CMND_CCCD);
        }

        // Kiểm tra CMND_CCCD đã tồn tại chưa (trong trường hợp sửa thông tin)
        public static bool CMND_CCCD_Edit(string CMND_CCCD, string oldCMND_CCCD)
        {
            return NhanVienDAL.CMND_CCCD_Edit(CMND_CCCD, oldCMND_CCCD);
        }

        // kiểm tra email đã tồn tại hay chưa
        public static bool GetNhanVienByEmail(string email)
        {
            return NhanVienDAL.GetNhanVienByEmail(email);
        }

        // Kiểm tra CMND_CCCD đã tồn tại chưa (trong trường hợp sửa thông tin)
        public static bool Email_Edit(string email, string oldEmail)
        {
            return NhanVienDAL.Email_Edit(email, oldEmail);
        }

        // Sửa thông tin nhân viên
        public static bool EditNhanVien(NhanVienDTO nv)
        {
            return NhanVienDAL.Update(nv);
        }

        //search nhan vien by name
        public static List<NhanVienDTO> SearchNhanVien(string keyword)
        {
            return NhanVienDAL.SearchNhanVienByName(keyword);
        }

        // Xóa nhân viên
        public static bool DeleteNhanVien(int maNV)
        {
            return NhanVienDAL.DeleteNhanVien(maNV);
        }

        // Lấy thông tin nhân viên cho report
        public static List<NhanVienDTO> GetNhanVienForReport()
        {
            return NhanVienDAL.GetNhanVienForReport();
        }
    }
}
