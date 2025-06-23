using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QuanLy_SoTietKiem.DTO;
using System.Data.SqlClient;
using System.Data;

namespace QuanLy_SoTietKiem.DAL
{
    public class LichSuThaoTacDTODAL
    {
        // Lay danh sach lich su thao tac
        public static List<LichSuThaoTacDTO> LayDanhSachLichSuThaoTac(string thuTuThoiGian = "DESC")
        {
            List<LichSuThaoTacDTO> lstLichSu = new List<LichSuThaoTacDTO>();

            // Đảm bảo chỉ nhận ASC hoặc DESC để tránh SQL injection
            thuTuThoiGian = thuTuThoiGian.ToUpper() == "ASC" ? "ASC" : "DESC";

            string query = $"SELECT * FROM LichSuThaoTac ORDER BY ThoiGian {thuTuThoiGian}";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            foreach (DataRow row in dt.Rows)
            {
                LichSuThaoTacDTO lichSu = new LichSuThaoTacDTO
                {
                    MaLS = Convert.ToInt32(row["MaLS"]),
                    MaTaiKhoan = Convert.ToInt32(row["MaTaiKhoan"]),
                    ThoiGian = Convert.ToDateTime(row["ThoiGian"]),
                    ThaoTac = row["ThaoTac"].ToString(),
                    DoiTuong = row["DoiTuong"].ToString()
                };
                lstLichSu.Add(lichSu);
            }
            return lstLichSu;
        }


        // Them lich su thao tac
        public static bool ThemLichSuThaoTac(LichSuThaoTacDTO lichSu)
        {
            string query = "INSERT INTO LichSuThaoTac (MaTaiKhoan, ThaoTac, ThoiGian, DoiTuong) VALUES (@MaTaiKhoan, @ThaoTac, @ThoiGian, @DoiTuong)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaTaiKhoan", lichSu.MaTaiKhoan),
                new SqlParameter("@ThaoTac", lichSu.ThaoTac),
                new SqlParameter("@ThoiGian", lichSu.ThoiGian),
                new SqlParameter("@DoiTuong", lichSu.DoiTuong)
            };
            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        // Lọc danh sách theo thời gian
        public static List<LichSuThaoTacDTO> LocLichSuTheoThoiGian(DateTime fromDate, DateTime toDate)
        {
            List<LichSuThaoTacDTO> lstLichSu = new List<LichSuThaoTacDTO>();
            string query = "SELECT * FROM LichSuThaoTac WHERE ThoiGian BETWEEN @FromDate AND @ToDate ORDER BY ThoiGian DESC";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@FromDate", fromDate),
                new SqlParameter("@ToDate", toDate)
            };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
            foreach (DataRow row in dt.Rows)
            {
                LichSuThaoTacDTO lichSu = new LichSuThaoTacDTO
                {
                    MaLS = Convert.ToInt32(row["MaLS"]),
                    MaTaiKhoan = Convert.ToInt32(row["MaTaiKhoan"]),
                    ThoiGian = Convert.ToDateTime(row["ThoiGian"]),
                    ThaoTac = row["ThaoTac"].ToString(),
                    DoiTuong = row["DoiTuong"].ToString()
                };
                lstLichSu.Add(lichSu);
            }
            return lstLichSu;
        }

        // Lọc danh sách theo thao tác
        public static List<LichSuThaoTacDTO> LocLichSuTheoThaoTac(string thaoTac)
        {
            List<LichSuThaoTacDTO> lstLichSu = new List<LichSuThaoTacDTO>();
            string query = "SELECT * FROM LichSuThaoTac WHERE ThaoTac LIKE @ThaoTac ORDER BY ThoiGian DESC";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ThaoTac", "%" + thaoTac + "%")
            };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
            foreach (DataRow row in dt.Rows)
            {
                LichSuThaoTacDTO lichSu = new LichSuThaoTacDTO
                {
                    MaLS = Convert.ToInt32(row["MaLS"]),
                    MaTaiKhoan = Convert.ToInt32(row["MaTaiKhoan"]),
                    ThoiGian = Convert.ToDateTime(row["ThoiGian"]),
                    ThaoTac = row["ThaoTac"].ToString(),
                    DoiTuong = row["DoiTuong"].ToString()
                };
                lstLichSu.Add(lichSu);
            }
            return lstLichSu;
        }
    }
}
