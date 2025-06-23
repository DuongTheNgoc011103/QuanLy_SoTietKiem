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
    public class ChiNhanhDAL
    {
        //getAllChiNhanh
        public static List<ChiNhanhDTO> GetAllChiNhanh()
        {
            List<ChiNhanhDTO> chiNhanhList = new List<ChiNhanhDTO>();
            string sql = "SELECT * FROM ChiNhanh";
            DataTable dt = DatabaseHelper.ExecuteQuery(sql);

            foreach(DataRow row in dt.Rows)
            {
                ChiNhanhDTO cn = new ChiNhanhDTO(
                    Convert.ToInt32(row["MaCN"]),
                    row["TenChiNhanh"].ToString(),
                    row["DiaChi"].ToString(),
                    row["NguoiQuanLy"].ToString()
                );
                chiNhanhList.Add(cn);
            }

            return chiNhanhList;
        }

        //AddChiNhanh
        public static bool AddChiNhanh(ChiNhanhDTO cn)
        {
            string query = "INSERT INTO ChiNhanh (TenChiNhanh, DiaChi, NguoiQuanLy) VALUES (@TenChiNhanh, @DiaChi, @NguoiQuanLy)";
            SqlParameter[] parameters = {
                new SqlParameter("@TenChiNhanh", cn.TenChiNhanh),
                new SqlParameter("@DiaChi", cn.DiaChi),
                new SqlParameter("@NguoiQuanLy", cn.NguoiQuanLy)
            };
            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        //EditChiNhanh
        public static bool Update(ChiNhanhDTO cn)
        {
            string query = "UPDATE ChiNhanh SET TenChiNhanh = @TenChiNhanh, DiaChi = @DiaChi, NguoiQuanLy = @NguoiQuanLy WHERE MaCN = @MaCN";
            SqlParameter[] parameters = {
                new SqlParameter("@TenChiNhanh", cn.TenChiNhanh),
                new SqlParameter("@DiaChi", cn.DiaChi),
                new SqlParameter("@NguoiQuanLy", cn.NguoiQuanLy),
                new SqlParameter("@MaCN", cn.MaCN)
            };
            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        // DeleteChiNhanh
        public static bool DeleteChiNhanh(int maCN)
        {
            string query = "DELETE FROM ChiNhanh WHERE MaCN = @MaCN";
            SqlParameter[] parameters = {
                new SqlParameter("@MaCN", maCN)
            };
            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        //SearchChiNhanh
        public static List<ChiNhanhDTO> SearchChiNhanh(string keyword)
        {
            List<ChiNhanhDTO> chiNhanhList = new List<ChiNhanhDTO>();
            string sql = "SELECT * FROM ChiNhanh WHERE TenChiNhanh LIKE @keyword OR DiaChi LIKE @keyword";
            SqlParameter[] parameters = {
                new SqlParameter("@keyword", "%" + keyword + "%")
            };
            DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);
            foreach (DataRow row in dt.Rows)
            {
                ChiNhanhDTO cn = new ChiNhanhDTO(
                    Convert.ToInt32(row["MaCN"]),
                    row["TenChiNhanh"].ToString(),
                    row["DiaChi"].ToString(),
                    row["NguoiQuanLy"].ToString()
                );
                chiNhanhList.Add(cn);
            }
            return chiNhanhList;
        }
    }
}
