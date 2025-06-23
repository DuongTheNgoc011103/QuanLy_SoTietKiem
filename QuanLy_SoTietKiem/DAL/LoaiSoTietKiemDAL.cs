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
    public class LoaiSoTietKiemDAL
    {
        // Lấy danh sách loại sổ tiết kiệm
        public static List<LoaiSoTietKiemDTO> GetAllLoaiSoTietKiem()
        {
            List<LoaiSoTietKiemDTO> loaiSoTietKiems = new List<LoaiSoTietKiemDTO>();
            string query = "SELECT * FROM LoaiSoTietKiem";

            DataTable dataTable = DatabaseHelper.ExecuteQuery(query);
            foreach (DataRow row in dataTable.Rows)
            {
                LoaiSoTietKiemDTO loaiSoTietKiem = new LoaiSoTietKiemDTO
                {
                    MaLoai = row["MaLoai"] != DBNull.Value ? Convert.ToInt32(row["MaLoai"]) : 0,
                    TenLoai = row["TenLoai"]?.ToString(),
                    LaiSuatCoBan = row["LaiSuatCoBan"] != DBNull.Value ? Convert.ToSingle(row["LaiSuatCoBan"]) : 0,
                    MoTa = row["MoTa"]?.ToString(),
                    PhiRutTruocHan = row["PhiRutTruocHan"] != DBNull.Value ? Convert.ToSingle(row["PhiRutTruocHan"]) : 0,
                    KyHanYeuCau = row["KyHanYeuCau"] != DBNull.Value ? Convert.ToInt32(row["KyHanYeuCau"]) : 0
                };
                loaiSoTietKiems.Add(loaiSoTietKiem);
            }

            return loaiSoTietKiems;
        }

        // Search loại sổ tiết kiệm theo tên
        public static List<LoaiSoTietKiemDTO> SearchLoaiSoTietKiemByName(string tenLoai)
        {
            List<LoaiSoTietKiemDTO> loaiSoTietKiems = new List<LoaiSoTietKiemDTO>();
            string query = "SELECT * FROM LoaiSoTietKiem WHERE TenLoai LIKE @TenLoai";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenLoai", "%" + tenLoai + "%")
            };
            DataTable dataTable = DatabaseHelper.ExecuteQuery(query, parameters);
            foreach (DataRow row in dataTable.Rows)
            {
                LoaiSoTietKiemDTO loaiSoTietKiem = new LoaiSoTietKiemDTO
                {
                    MaLoai = row["MaLoai"] != DBNull.Value ? Convert.ToInt32(row["MaLoai"]) : 0,
                    TenLoai = row["TenLoai"]?.ToString(),
                    LaiSuatCoBan = row["LaiSuatCoBan"] != DBNull.Value ? Convert.ToSingle(row["LaiSuatCoBan"]) : 0,
                    MoTa = row["MoTa"]?.ToString(),
                    PhiRutTruocHan = row["PhiRutTruocHan"] != DBNull.Value ? Convert.ToSingle(row["PhiRutTruocHan"]) : 0,
                    KyHanYeuCau = row["KyHanYeuCau"] != DBNull.Value ? Convert.ToInt32(row["KyHanYeuCau"]) : 0
                };
                loaiSoTietKiems.Add(loaiSoTietKiem);
            }
            return loaiSoTietKiems;
        }

        // Thêm loại sổ tiết kiệm mới
        public static bool AddLoaiSoTietKiem(LoaiSoTietKiemDTO loaiSoTietKiem)
        {
            string query = "INSERT INTO LoaiSoTietKiem (TenLoai, LaiSuatCoBan, MoTa, PhiRutTruocHan, KyHanYeuCau) " +
                           "VALUES (@TenLoai, @LaiSuatCoBan, @MoTa, @PhiRutTruocHan, @KyHanYeuCau)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenLoai", loaiSoTietKiem.TenLoai),
                new SqlParameter("@LaiSuatCoBan", loaiSoTietKiem.LaiSuatCoBan),
                new SqlParameter("@MoTa", loaiSoTietKiem.MoTa),
                new SqlParameter("@PhiRutTruocHan", loaiSoTietKiem.PhiRutTruocHan),
                new SqlParameter("@KyHanYeuCau", loaiSoTietKiem.KyHanYeuCau)
            };
            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        // Cập nhật thông tin loại sổ tiết kiệm
        public static bool UpdateLoaiSoTietKiem(LoaiSoTietKiemDTO loaiSoTietKiem)
        {
            string query = "UPDATE LoaiSoTietKiem SET " +
                           "TenLoai = @TenLoai, " +
                           "LaiSuatCoBan = @LaiSuatCoBan, " +
                           "MoTa = @MoTa, " +
                           "PhiRutTruocHan = @PhiRutTruocHan, " +
                           "KyHanYeuCau = @KyHanYeuCau " +
                           "WHERE MaLoai = @MaLoai";
            SqlParameter[] parameters =
            {
                new SqlParameter("@MaLoai", loaiSoTietKiem.MaLoai),
                new SqlParameter("@TenLoai", loaiSoTietKiem.TenLoai),
                new SqlParameter("@LaiSuatCoBan", loaiSoTietKiem.LaiSuatCoBan),
                new SqlParameter("@MoTa", loaiSoTietKiem.MoTa),
                new SqlParameter("@PhiRutTruocHan", loaiSoTietKiem.PhiRutTruocHan),
                new SqlParameter("@KyHanYeuCau", loaiSoTietKiem.KyHanYeuCau)
            };
            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        // Xóa loại sổ tiết kiệm
        public static bool DeleteLoaiSoTietKiem(int maLoai)
        {
            string query = "DELETE FROM LoaiSoTietKiem WHERE MaLoai = @MaLoai";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaLoai", maLoai)
            };
            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        // GetAllLoaiSoTietKiem theo MaLoai
        public static LoaiSoTietKiemDTO GetLoaiSoTietKiemById(int maLoai)
        {
            LoaiSoTietKiemDTO loaiSTK = null;
            string query = "SELECT MaLoai, TenLoai, LaiSuatCoBan, MoTa, PhiRutTruocHan, KyHanYeuCau FROM LoaiSoTietKiem WHERE MaLoai = @MaLoai";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaLoai", maLoai)
            };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                loaiSTK = new LoaiSoTietKiemDTO
                {
                    MaLoai = row["MaLoai"] != DBNull.Value ? Convert.ToInt32(row["MaLoai"]) : 0,
                    TenLoai = row["TenLoai"]?.ToString(),
                    LaiSuatCoBan = row["LaiSuatCoBan"] != DBNull.Value ? Convert.ToSingle(row["LaiSuatCoBan"]) : 0,
                    MoTa = row["MoTa"]?.ToString(),
                    PhiRutTruocHan = row["PhiRutTruocHan"] != DBNull.Value ? Convert.ToSingle(row["PhiRutTruocHan"]) : 0,
                    KyHanYeuCau = row["KyHanYeuCau"] != DBNull.Value ? Convert.ToInt32(row["KyHanYeuCau"]) : 0
                };
            }
            return loaiSTK;
        }

        // Get TenLoai by MaLoai
        public static string GetTenLoaiByMaLoai(int maLoai)
        {
            string query = "SELECT TenLoai FROM LoaiSoTietKiem WHERE MaLoai = @MaLoai";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaLoai", maLoai)
            };
            DataTable dataTable = DatabaseHelper.ExecuteQuery(query, parameters);
            if (dataTable.Rows.Count > 0)
            {
                return dataTable.Rows[0]["TenLoai"]?.ToString();
            }
            return null;
        }

        // GetLaiSuatByMaLoai
        public static float GetLaiSuatByMaLoai(int maLoai)
        {
            string query = "SELECT LaiSuatCoBan FROM LoaiSoTietKiem WHERE MaLoai = @MaLoai";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaLoai", maLoai)
            };
            DataTable dataTable = DatabaseHelper.ExecuteQuery(query, parameters);
            if (dataTable.Rows.Count > 0 && dataTable.Rows[0]["LaiSuatCoBan"] != DBNull.Value)
            {
                return Convert.ToSingle(dataTable.Rows[0]["LaiSuatCoBan"]);
            }
            return 0;
        }
    }
}
