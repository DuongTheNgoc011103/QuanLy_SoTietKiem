using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QuanLy_SoTietKiem.DTO;
using QuanLy_SoTietKiem.DAL;
using QuanLy_SoTietKiem.Utils;
using System.Data.SqlClient;
using System.Data;

namespace QuanLy_SoTietKiem.BLL
{
    public class GiaoDichTietKiemBLL
    {
        // Lấy danh sách giao dịch gửi tiền tiết kiệm
        public static List<GiaoDichTietKiemDTO> GetGiaoDichGuiTien()
        {
            return GiaoDichTietKiemDAL.GetGiaoDichGuiTien();
        }

        // Thêm giao dịch tiết kiệm
        public static bool ThemGiaoDich(GiaoDichTietKiemDTO giaoDich)
        {
            return GiaoDichTietKiemDAL.ThemGiaoDich(giaoDich);
        }

        // Lấy mã giao dịch lớn nhất
        public static int GetMaxMaGD()
        {
             return GiaoDichTietKiemDAL.GetMaxMaGD() ?? 0;
        }

        // SearchGiaoDichByType
        public static List<GiaoDichTietKiemDTO> SearchGiaoDichByType(string loaiGiaoDich)
        {
            return GiaoDichTietKiemDAL.SearchGiaoDichByType(loaiGiaoDich);
        }

        // Kiểm tra giao dịch của nhân viên
        public static bool KiemTraGiaoDichCuaNhanVien(int maNV)
        {
            return GiaoDichTietKiemDAL.CountGiaoDichByMaNV(maNV) > 0;
        }

        // Kiểm tra giao dịch của sổ tiết kiệm
        public static bool KiemTraGiaoDichCuaSoTietKiem(int maSo)
        {
            return GiaoDichTietKiemDAL.CountGiaoDichByMaSo(maSo) > 0;
        }

        // Lấy danh sách giao dịch tiết kiệm theo mã sổ
        public static GiaoDichTietKiemDTO GetGiaoDichByMaGD(int maSo)
        {
            return GiaoDichTietKiemDAL.GetGiaoDichByMaGD(maSo);
        }

        // Lấy danh sách giao dịch tiết kiệm theo mã giao dịch
        public static RutTienReportDTO GetRutTienReportDataBasic(int maGD)
        {
            return GiaoDichTietKiemDAL.GetRutTienReportDataBasic(maGD);
        }
    }
}
