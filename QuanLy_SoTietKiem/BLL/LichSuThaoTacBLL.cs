using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QuanLy_SoTietKiem.DTO;
using QuanLy_SoTietKiem.DAL;

namespace QuanLy_SoTietKiem.BLL
{
    public class LichSuThaoTacBLL
    {
        // Lấy danh sách lịch sử thao tác theo thứ tự thời gian (DESC hoặc ASC)
        public static List<LichSuThaoTacDTO> LayDanhSachLichSuThaoTac(string thuTuThoiGian = "DESC")
        {
            return LichSuThaoTacDTODAL.LayDanhSachLichSuThaoTac(thuTuThoiGian);
        }

        // Them lich su thao tac
        public static bool ThemLichSuThaoTac(LichSuThaoTacDTO lichSu)
        {
            return LichSuThaoTacDTODAL.ThemLichSuThaoTac(lichSu);
        }

        // Lọc danh sách theo thời gian
        public static List<LichSuThaoTacDTO> LocLichSuTheoThoiGian(DateTime fromDate, DateTime toDate)
        {
            return LichSuThaoTacDTODAL.LocLichSuTheoThoiGian(fromDate, toDate);
        }

        // Lấy lịch sử thao tác theo thao tác
        public static List<LichSuThaoTacDTO> LocLichSuTheoThaoTac(string thaoTac)
        {
            return LichSuThaoTacDTODAL.LocLichSuTheoThaoTac(thaoTac);
        }
    }
}
