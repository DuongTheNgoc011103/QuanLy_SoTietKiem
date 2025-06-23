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
    public class LoaiSoTietKiemBLL
    {
        // Lấy danh sách loại sổ tiết kiệm
        public static List<LoaiSoTietKiemDTO> GetAllLoaiSoTietKiem()
        {
            return LoaiSoTietKiemDAL.GetAllLoaiSoTietKiem();
        }

        // Tìm kiếm loại sổ tiết kiệm theo tên
        public static List<LoaiSoTietKiemDTO> SearchLoaiSoTietKiemByName(string tenLoai)
        {
            return LoaiSoTietKiemDAL.SearchLoaiSoTietKiemByName(tenLoai);
        }

        // Thêm loại sổ tiết kiệm
        public static bool AddLoaiSoTietKiem(LoaiSoTietKiemDTO loaiSoTietKiem)
        {
            return LoaiSoTietKiemDAL.AddLoaiSoTietKiem(loaiSoTietKiem);
        }

        // Cập nhật loại sổ tiết kiệm
        public static bool UpdateLoaiSoTietKiem(LoaiSoTietKiemDTO loaiSoTietKiem)
        {
            return LoaiSoTietKiemDAL.UpdateLoaiSoTietKiem(loaiSoTietKiem);
        }

        // Xóa loại sổ tiết kiệm
        //public static bool DeleteLoaiSoTietKiem(int maLoai)
        //{
        //    return LoaiSoTietKiemDAL.DeleteLoaiSoTietKiem(maLoai);
        //}
        public static bool DeleteLoaiSoTietKiem(int maLoai)
        {
            // BƯỚC 1: KIỂM TRA SỔ TIẾT KIỆM ĐANG SỬ DỤNG LOẠI NÀY
            if (SoTietKiemBLL.KiemTraSoTietKiemByMaLoai(maLoai)) // Hàm này sẽ được tạo
            {
                MessageBox.Show("Không thể xóa loại sổ tiết kiệm này vì có sổ tiết kiệm đang sử dụng nó.", "Lỗi xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false; // Chặn xóa
            }

            // Nếu không có sổ nào sử dụng, tiến hành xóa loại sổ
            return LoaiSoTietKiemDAL.DeleteLoaiSoTietKiem(maLoai);
        }

        // Get Tem Loai So Tiet Kiem by MaLoai
        public static string GetTenLoaiByMaLoai(int maLoai)
        {
            return LoaiSoTietKiemDAL.GetTenLoaiByMaLoai(maLoai);
        }

        // GetAllLoaiSoTietKiem theo MaLoai
        public static LoaiSoTietKiemDTO GetLoaiSoTietKiemById(int maLoai)
        {
            return LoaiSoTietKiemDAL.GetLoaiSoTietKiemById(maLoai);
        }

        // GetLaiSuatByMaLoai
        public static float GetLaiSuatByMaLoai(int maLoai)
        {
            return LoaiSoTietKiemDAL.GetLaiSuatByMaLoai(maLoai);
        }
    }
}
