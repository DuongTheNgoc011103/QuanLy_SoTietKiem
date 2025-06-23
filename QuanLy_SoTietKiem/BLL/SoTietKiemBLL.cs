using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QuanLy_SoTietKiem.DTO;
using QuanLy_SoTietKiem.DAL;
using System.Windows.Forms;

namespace QuanLy_SoTietKiem.BLL
{
    public class SoTietKiemBLL
    {
        // Lấy danh sách sổ tiết kiệm theo mã loại sổ tiết kiệm
        public static List<SoTietKiemDTO> GetSoTietKiemByMaLoai(int maLoai)
        {
            return SoTietKiemDAL.GetSoTietKiemByMaLoai(maLoai);
        }

        // Lấy tất cả sổ tiết kiệm
        public static List<SoTietKiemDTO> GetAllSoTietKiem()
        {
            return SoTietKiemDAL.GetAllSoTietKiem();
        }

        // Search Ho Ten Khach Hang
        public static List<SoTietKiemDTO> SearchHoTenKhachHang(string hoTen)
        {
            return SoTietKiemDAL.SearchSoTietKiemByHoTen(hoTen);
        }

        // Thêm sổ tiết kiệm
        public static bool AddSoTietKiem(SoTietKiemDTO soTietKiem)
        {
            return SoTietKiemDAL.AddSoTietKiem(soTietKiem);
        }

        // Cập nhật sổ tiết kiệm
        public static bool UpdateSoTietKiem(SoTietKiemDTO soTietKiem)
        {
            return SoTietKiemDAL.UpdateSoTietKiem(soTietKiem);
        }

        // Xóa sổ tiết kiệm
        //public static bool DeleteSoTietKiem(int maSo)
        //{
        //    return SoTietKiemDAL.DeleteSoTietKiem(maSo);
        //}

        public static bool DeleteSoTietKiem(int maSo)
        {
            // BƯỚC 1: KIỂM TRA GIAO DỊCH CỦA SỔ TIẾT KIỆM
            if (GiaoDichTietKiemBLL.KiemTraGiaoDichCuaSoTietKiem(maSo)) // Hàm này sẽ được tạo
            {
                MessageBox.Show("Không thể xóa sổ tiết kiệm này vì đã có các giao dịch phát sinh. Vui lòng tất toán và đóng sổ nếu cần.", "Lỗi xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false; // Chặn xóa
            }

            // Nếu không có giao dịch, tiến hành xóa sổ
            return SoTietKiemDAL.DeleteSoTietKiem(maSo);
        }

        // Lấy MaSo lớn nhất trong bảng SoTietKiem
        public static int GetMaxMaSo()
        {
            return SoTietKiemDAL.GetMaxMaSo() ?? 0;
        }

        // Lấy danh sách sổ tiết kiệm theo mã khách hàng
        public static List<SoTietKiemDTO> GetSoTietKiemByMaKH(int maKH)
        {
            return SoTietKiemDAL.GetSoTietKiemByMaKH(maKH);
        }

        // GetSoTietKiemByCCCDKH
        public static List<SoTietKiemDTO> GetSoTietKiemByCCCDKH(string cccd)
        {
            return SoTietKiemDAL.GetSoTietKiemByCCCDKH(cccd);
        }

        // Fix for CS0127: Removed the 'return' keyword since the method returns void.
        public static void CapNhatTrangThaiSoTietKiem(int maKH, string trangThai)
        {
            SoTietKiemDAL.CapNhatTrangThaiSoTietKiem(maKH, trangThai);
        }

        // Update trạng thái sổ tiết kiệm
        public static void UpdateTrangThaiSoTietKiem(int maSo, string trangThai)
        {
            SoTietKiemDAL.UpdateTrangThaiSoTietKiem(maSo, trangThai);
        }

        //CapNhatSoTienGoc khi gửi
        public static void CapNhatSoTienGoc(int maSo, decimal soTienGoc)
        {
            SoTietKiemDAL.CapNhatSoTienGoc(maSo, soTienGoc);
        }

        // CapNhatSoTienGoc khi rút
        public static void CapNhatSoTienGocKhiRut(int maSo, decimal soTienGoc)
        {
            SoTietKiemDAL.CapNhatSoTienGocKhiRut(maSo, soTienGoc);
        }

        // CapNhatSoDaoHan
        public static List<int> CapNhatSoDaoHan()
        {
            return SoTietKiemDAL.CapNhatSoDaoHan();
        }

        // GetSoTietKiemChiTiet
        public static SoTietKiemDTO GetSoTietKiemChiTiet(int maSo)
        {
            return SoTietKiemDAL.GetSoTietKiemChiTiet(maSo);
        }

        // CapNhatNgayMo
        public static void CapNhatNgayMo(int maSo)
        {
            SoTietKiemDAL.CapNhatNgayMo(maSo);
        }

        // GetSoTietKiemInfoForCalculation
        public static SoTietKiemDTO GetSoTietKiemInfoForCalculation(int maSo)
        {
            return SoTietKiemDAL.GetSoTietKiemInfoForCalculation(maSo);
        }

        // TinhLaiDuKien
        public static SoTietKiemDTO TinhLaiDuKien(int maSo, DateTime ngayTinhToan, decimal soTien)
        {
            return SoTietKiemDAL.TinhLaiDuKien(maSo, ngayTinhToan, soTien);
        }

        // CountSoTietKiemByMaKH
        public static bool KiemTraSoTietKiemCuaKhachHang(int maKH)
        {
            return SoTietKiemDAL.CountSoTietKiemByMaKH(maKH) > 0;
        }

        // CountSoTietKiemByMaLoai
        public static bool KiemTraSoTietKiemByMaLoai(int maLoai)
        {
            return SoTietKiemDAL.CountSoTietKiemByMaLoai(maLoai) > 0;
        }
    }
}
