using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QuanLy_SoTietKiem.DTO;
using QuanLy_SoTietKiem.DAL;

namespace QuanLy_SoTietKiem.BLL
{
    public class KhachHangBLL
    {
        // Lấy danh sách khách hàng
        public static List<KhachHangDTO> GetAllKhachHang()
        {
            return KhachHangDAL.GetALlKhachHang();
        }

        // Tìm kiếm khách hàng theo tên
        public static List<KhachHangDTO> SearchKhachHangByName(string name)
        {
            return KhachHangDAL.SearchKhachHangByName(name);
        }

        // kiểm tra sdt đã tồn tại hay chưa
        public static bool GetKhachHangBySDT(string dienThoai)
        {
            return KhachHangDAL.GetKhachHangBySDT(dienThoai);
        }

        // Kiểm tra số điện thoại đã tồn tại chưa (trong trường hợp sửa thông tin)
        public static bool DienThoai_Edit(string dienThoai, string oldDienThoai)
        {
            return KhachHangDAL.DienThoai_Edit(dienThoai, oldDienThoai);
        }

        // kiểm tra CMND_CCCD đã tồn tại hay chưa
        public static bool GetKhachHangByCMND_CCCD(string CMND_CCCD)
        {
            return KhachHangDAL.GetKhachHangByCMND_CCCD(CMND_CCCD);
        }

        // Kiểm tra CMND_CCCD đã tồn tại chưa (trong trường hợp sửa thông tin)
        public static bool CMND_CCCD_Edit(string CMND_CCCD, string oldCMND_CCCD)
        {
            return KhachHangDAL.CMND_CCCD_Edit(CMND_CCCD, oldCMND_CCCD);
        }

        // kiểm tra email đã tồn tại hay chưa
        public static bool GetKhachHangByEmail(string email)
        {
            return KhachHangDAL.GetKhachHangByEmail(email);
        }

        // Kiểm tra CMND_CCCD đã tồn tại chưa (trong trường hợp sửa thông tin)
        public static bool Email_Edit(string email, string oldEmail)
        {
            return KhachHangDAL.Email_Edit(email, oldEmail);
        }

        // Thêm khách hàng
        public static bool AddKhachHang(KhachHangDTO kh)
        {
            return KhachHangDAL.AddKhachHang(kh);
        }

        // Cập nhật thông tin khách hàng
        public static bool UpdateKhachHang(KhachHangDTO kh)
        {
            return KhachHangDAL.UpdateKhachHang(kh);
        }

        // Xóa khách hàng
        public static bool DeleteKhachHang(int maKH)
        {
            return KhachHangDAL.DeleteKhachHang(maKH);
        }

        // Get Khach Hang Co SoTietKiem
        public static List<KhachHangDTO> GetKhachHangBySoTietKiem()
        {
            return KhachHangDAL.GetKhachHangBySoTietKiem();
        }

        // SearchKhachHangByNameAndSoTietKiem
        public static List<KhachHangDTO> SearchKhachHangByNameAndSoTietKiem(string name)
        {
            return KhachHangDAL.SearchKhachHangByNameAndSoTietKiem(name);
        }

        // GetEmailByMaKH
        public static string GetEmailByMaKH(int maKH)
        {
            return KhachHangDAL.GetEmailByMaKH(maKH);
        }
    }
}
