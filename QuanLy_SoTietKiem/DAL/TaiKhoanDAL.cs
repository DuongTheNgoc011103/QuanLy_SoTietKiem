using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using QuanLy_SoTietKiem.DTO;
using QuanLy_SoTietKiem.DAL;
using System.Data;

namespace QuanLy_SoTietKiem.DAL
{
    public class TaiKhoanDAL
    {
        // Kiểm tra đăng nhập hệ thống
        public static bool KiemTraDangNhap(string tenDN, string matKhau)
        {

            string sql = "SELECT COUNT(*) FROM TaiKhoanNguoiDung WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau";

            SqlParameter[] parameters =
            {
                new SqlParameter("@TenDangNhap", tenDN),
                new SqlParameter("@MatKhau", matKhau)
            };

            DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);

            if (dt.Rows.Count > 0)
            {
                int count = Convert.ToInt32(dt.Rows[0][0]);
                return count > 0;
            }

            return false;
        }

        // Lấy thông tin đăng nhập của người dùng
        public static TaiKhoanDTO LayThongTinDangNhap(string tenDN, string matKhau)
        {
            string sql = @"
                SELECT tk.MaTaiKhoan, tk.QuyenHan, nv.HoTen,
                        tk.TenDangNhap, tk.MaNV, tk.TrangThai
                        FROM TaiKhoanNguoiDung tk
                        JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                        WHERE tk.TenDangNhap = @TenDangNhap AND tk.MatKhau = @MatKhau";

            SqlParameter[] parameters =
            {
                new SqlParameter("@TenDangNhap", tenDN),
                new SqlParameter("@MatKhau", matKhau)
            };

            DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);
            if (dt.Rows.Count > 0)
            {
                return new TaiKhoanDTO
                {
                    MaTaiKhoan = Convert.ToInt32(dt.Rows[0]["MaTaiKhoan"]),
                    QuyenHan = dt.Rows[0]["QuyenHan"].ToString(),
                    HoTen = dt.Rows[0]["HoTen"].ToString(),
                    TenDangNhap = tenDN,
                    TrangThai = dt.Rows[0]["TrangThai"] != DBNull.Value && Convert.ToBoolean(dt.Rows[0]["TrangThai"]),
                    MaNV = dt.Rows[0]["MaNV"] is DBNull ? (int?)null : Convert.ToInt32(dt.Rows[0]["MaNV"]),
                };
            }
            return null;
        }

        // Cập nhật mật khẩu cho tài khoản người dùng
        public static bool CapNhatMatKhau(string email, string matKhauMoiMaHoa)
        {
            string sql = @"
                UPDATE TaiKhoanNguoiDung 
                SET MatKhau = @MatKhauMoi 
                WHERE MaNV IN ( SELECT MaNV FROM NhanVien WHERE Email = @Email )";

            SqlParameter[] parameters =
            {
                new SqlParameter("@MatKhauMoi", matKhauMoiMaHoa),
                new SqlParameter("@Email", email)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(sql, parameters);
            return rowsAffected > 0;
        }

        // Cập nhật mật khẩu theo tên đăng nhập
        public static bool CapNhatMatKhauTheoTenDangNhap(string tenDN, string matKhauMoi)
        {
            string sql = @"UPDATE TaiKhoanNguoiDung SET MatKhau = @MatKhauMoi WHERE TenDangNhap = @TenDangNhap";

            SqlParameter[] parameters =
            {
                new SqlParameter("@MatKhauMoi", matKhauMoi),
                new SqlParameter("@TenDangNhap", tenDN)
            };

            int rows = DatabaseHelper.ExecuteNonQuery(sql, parameters);
            return rows > 0;
        }


        // Lấy danh sách tài khoản người dùng
        public static List<TaiKhoanDTO> GetAllTaiKhoan()
        {
            List<TaiKhoanDTO> taiKhoans = new List<TaiKhoanDTO>();

            string sql = @" SELECT tk.*, nv.HoTen AS HoTen
                            FROM TaiKhoanNguoiDung tk
                            LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV";

            DataTable dt = DatabaseHelper.ExecuteQuery(sql);

            if (dt == null || dt.Rows.Count == 0)
                return taiKhoans;

            foreach (DataRow row in dt.Rows)
            {
                TaiKhoanDTO taiKhoan = new TaiKhoanDTO
                {
                    MaTaiKhoan = Convert.ToInt32(row["MaTaiKhoan"]),
                    TenDangNhap = row["TenDangNhap"].ToString(),
                    MatKhau = row["MatKhau"].ToString(),
                    QuyenHan = row["QuyenHan"].ToString(),
                    TrangThai = Convert.ToBoolean(row["TrangThai"]),
                    MaNV = row["MaNV"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["MaNV"]),
                    HoTen = row["HoTen"] == DBNull.Value ? "" : row["HoTen"].ToString()
                };

                taiKhoans.Add(taiKhoan);
            }

            return taiKhoans;
        }


        // Search tài khoản theo tên đăng nhập
        public static List<TaiKhoanDTO> SearchTaiKhoanByTenDangNhap(string tenDangNhap)
        {
            List<TaiKhoanDTO> taiKhoans = new List<TaiKhoanDTO>();
            string sql = "SELECT * FROM TaiKhoanNguoiDung WHERE TenDangNhap LIKE @TenDangNhap";
            SqlParameter[] parameters =
            {
                new SqlParameter("@TenDangNhap", "%" + tenDangNhap + "%")
            };
            DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);
            if (dt == null || dt.Rows.Count == 0)
                return taiKhoans;
            foreach (DataRow row in dt.Rows)
            {
                TaiKhoanDTO taiKhoan = new TaiKhoanDTO
                {
                    MaTaiKhoan = Convert.ToInt32(row["MaTaiKhoan"]),
                    TenDangNhap = row["TenDangNhap"].ToString(),
                    MatKhau = row["MatKhau"].ToString(),
                    QuyenHan = row["QuyenHan"].ToString(),
                    TrangThai = Convert.ToBoolean(row["TrangThai"]),
                    MaNV = row["MaNV"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["MaNV"])
                };
                taiKhoans.Add(taiKhoan);
            }
            return taiKhoans;
        }

        // Kiểm tra tên đăng nhập đã tồn tại chưa
        public static bool GetTaiKhoanByTenDN(string tenDN)
        {
            string query = "SELECT COUNT(*) FROM TaiKhoanNguoiDung WHERE TenDangNhap = @TenDangNhap";
            SqlParameter[] parameters =
            {
                new SqlParameter("@TenDangNhap", tenDN)
            };

            object result = DatabaseHelper.ExecuteScalar(query, parameters);
            int count = (result != null) ? Convert.ToInt32(result) : 0;

            return count > 0; // Nếu count > 0 thì đã tồn tại
        }

        // Kiểm tra tên đăng nhập đã tồn tại chưa (trong trường hợp sửa thông tin)
        public static bool TenDN_Edit(string tenDN, string oldTenDN)
        {
            string query = "SELECT COUNT(*) FROM TaiKhoanNguoiDung WHERE TenDangNhap = @TenDangNhap AND TenDangNhap <> @TenDangNhapCu";
            SqlParameter[] parameters =
            {
                new SqlParameter("@TenDangNhap", tenDN),
                new SqlParameter("@TenDangNhapCu", oldTenDN)
            };

            object result = DatabaseHelper.ExecuteScalar(query, parameters);
            int count = (result != null) ? Convert.ToInt32(result) : 0;

            return count > 0; // Nếu count > 0 tức là có mã khác trùng
        }

        // Thêm tài khoản mới
        public static bool ThemTaiKhoan(TaiKhoanDTO taiKhoan)
        {
            string sql = @"
                INSERT INTO TaiKhoanNguoiDung (TenDangNhap, MatKhau, QuyenHan, TrangThai, MaNV)
                VALUES (@TenDangNhap, @MatKhau, @QuyenHan, @TrangThai, @MaNV)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@TenDangNhap", taiKhoan.TenDangNhap),
                new SqlParameter("@MatKhau", taiKhoan.MatKhau),
                new SqlParameter("@QuyenHan", taiKhoan.QuyenHan),
                new SqlParameter("@TrangThai", taiKhoan.TrangThai),
                new SqlParameter("@MaNV", (object)taiKhoan.MaNV ?? DBNull.Value)
            };
            int rowsAffected = DatabaseHelper.ExecuteNonQuery(sql, parameters);
            return rowsAffected > 0;
        }

        // Sửa thông tin tài khoản
        public static bool SuaTaiKhoan(TaiKhoanDTO taiKhoan)
        {
            string sql = @"
                UPDATE TaiKhoanNguoiDung 
                SET TenDangNhap = @TenDangNhap, MatKhau = @MatKhau, QuyenHan = @QuyenHan, TrangThai = @TrangThai
                WHERE MaTaiKhoan = @MaTaiKhoan";
            SqlParameter[] parameters =
            {
                new SqlParameter("@MaTaiKhoan", taiKhoan.MaTaiKhoan),
                new SqlParameter("@TenDangNhap", taiKhoan.TenDangNhap),
                new SqlParameter("@MatKhau", taiKhoan.MatKhau),
                new SqlParameter("@QuyenHan", taiKhoan.QuyenHan),
                new SqlParameter("@TrangThai", taiKhoan.TrangThai),
            };
            int rowsAffected = DatabaseHelper.ExecuteNonQuery(sql, parameters);
            return rowsAffected > 0;
        }

        // Xóa tài khoản
        public static bool XoaTaiKhoan(int maTaiKhoan)
        {
            string sql = "DELETE FROM TaiKhoanNguoiDung WHERE MaTaiKhoan = @MaTaiKhoan";
            SqlParameter[] parameters =
            {
                new SqlParameter("@MaTaiKhoan", maTaiKhoan)
            };
            int rowsAffected = DatabaseHelper.ExecuteNonQuery(sql, parameters);
            return rowsAffected > 0;
        }

        // Get MaNV by MaTaiKhoan
        public static int? GetMaNVByMaTaiKhoan(int maTaiKhoan)
        {
            string sql = "SELECT MaNV FROM TaiKhoanNguoiDung WHERE MaTaiKhoan = @MaTaiKhoan";
            SqlParameter[] parameters =
            {
                new SqlParameter("@MaTaiKhoan", maTaiKhoan)
            };
            object result = DatabaseHelper.ExecuteScalar(sql, parameters);
            return result != null ? (int?)Convert.ToInt32(result) : null;
        }

        // Đếm số tài khoản theo mã nhân viên
        public static int CountTaiKhoanByMaNV(int maNV)
        {
            string query = "SELECT COUNT(*) FROM TaiKhoanNguoiDung WHERE MaNV = @MaNV";
            SqlParameter[] parameters = { new SqlParameter("@MaNV", maNV) };
            object result = DatabaseHelper.ExecuteScalar(query, parameters);
            return (result == null || result is DBNull) ? 0 : Convert.ToInt32(result);
        }
    }
}
