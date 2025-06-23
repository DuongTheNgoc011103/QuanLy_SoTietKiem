using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QuanLy_SoTietKiem.DTO;
using System.Data.SqlClient;
using System.Data;
using QuanLy_SoTietKiem.BLL;
using System.Windows.Forms;

namespace QuanLy_SoTietKiem.DAL
{
    public class KhachHangDAL
    {
        // Lấy danh sách khách hàng
        public static List<KhachHangDTO> GetALlKhachHang()
        {
            List<KhachHangDTO> ds = new List<KhachHangDTO>();
            string query = "SELECT * FROM KhachHang";

            DataTable dt = DatabaseHelper.ExecuteQuery(query);
            foreach (DataRow dr in dt.Rows)
            {
                KhachHangDTO khachHang = new KhachHangDTO
                {
                    MaKH = Convert.ToInt32(dr["MaKH"]),
                    HoTen = dr["HoTen"].ToString(),
                    NgaySinh = dr["NgaySinh"] as DateTime?,
                    CMND_CCCD = dr["CMND_CCCD"].ToString(),
                    DiaChi = dr["DiaChi"].ToString(),
                    DienThoai = dr["DienThoai"].ToString(),
                    Email = dr["Email"].ToString(),
                    NgayCap = dr["NgayCap"] as DateTime?,
                    TrangThai = Convert.ToBoolean(dr["TrangThai"])
                };
                ds.Add(khachHang);
            }

            return ds;
        }

        // Search khách hàng theo tên
        public static List<KhachHangDTO> SearchKhachHangByName(string name)
        {
            List<KhachHangDTO> ds = new List<KhachHangDTO>();
            string query = "SELECT * FROM KhachHang WHERE HoTen LIKE @name";
            SqlParameter[] parameters = { new SqlParameter("@name", "%" + name + "%") };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
            foreach (DataRow dr in dt.Rows)
            {
                KhachHangDTO khachHang = new KhachHangDTO
                {
                    MaKH = Convert.ToInt32(dr["MaKH"]),
                    HoTen = dr["HoTen"].ToString(),
                    NgaySinh = dr["NgaySinh"] as DateTime?,
                    CMND_CCCD = dr["CMND_CCCD"].ToString(),
                    DiaChi = dr["DiaChi"].ToString(),
                    DienThoai = dr["DienThoai"].ToString(),
                    Email = dr["Email"].ToString(),
                    NgayCap = dr["NgayCap"] as DateTime?,
                    TrangThai = Convert.ToBoolean(dr["TrangThai"])
                };
                ds.Add(khachHang);
            }
            return ds;
        }

        /// <summary>
        /// ////
        /// </summary>
        /// <param name="dienThoai"></param>
        /// <returns></returns>
        /// 

        // Kiểm tra số điện thoại đã tồn tại chưa
        public static bool GetKhachHangBySDT(string dienThoai)
        {
            string query = "SELECT COUNT(*) FROM KhachHang WHERE DienThoai = @DienThoai";
            SqlParameter[] parameters =
            {
                new SqlParameter("@DienThoai", dienThoai)
            };

            object result = DatabaseHelper.ExecuteScalar(query, parameters);
            int count = (result != null) ? Convert.ToInt32(result) : 0;

            return count > 0; // Nếu count > 0 thì số điện thoại đã tồn tại
        }

        // Kiểm tra số điện thoại đã tồn tại chưa (trong trường hợp sửa thông tin)
        public static bool DienThoai_Edit(string dienThoai, string oldDienThoai)
        {
            string query = "SELECT COUNT(*) FROM KhachHang WHERE DienThoai = @DienThoai AND DienThoai <> @DienThoaiCu";
            SqlParameter[] parameters =
            {
                new SqlParameter("@DienThoai", dienThoai),
                new SqlParameter("@DienThoaiCu", oldDienThoai)
            };

            object result = DatabaseHelper.ExecuteScalar(query, parameters);
            int count = (result != null) ? Convert.ToInt32(result) : 0;

            return count > 0; // Nếu count > 0 tức là có mã khác trùng
        }

        // Kiểm tra CMND_CCCD đã tồn tại chưa
        public static bool GetKhachHangByCMND_CCCD(string CMND_CCCD)
        {
            string query = "SELECT COUNT(*) FROM KhachHang WHERE CMND_CCCD = @CMND_CCCD";
            SqlParameter[] parameters =
            {
                new SqlParameter("@CMND_CCCD", CMND_CCCD)
            };

            object result = DatabaseHelper.ExecuteScalar(query, parameters);
            int count = (result != null) ? Convert.ToInt32(result) : 0;

            return count > 0; // Nếu count > 0 thì số điện thoại đã tồn tại
        }

        // Kiểm tra số điện thoại đã tồn tại chưa (trong trường hợp sửa thông tin)
        public static bool CMND_CCCD_Edit(string CMND_CCCD, string oldCMND_CCCD)
        {
            string query = "SELECT COUNT(*) FROM KhachHang WHERE CMND_CCCD = @CMND_CCCD AND CMND_CCCD <> @CMND_CCCDCu";
            SqlParameter[] parameters =
            {
                new SqlParameter("@CMND_CCCD", CMND_CCCD),
                new SqlParameter("@CMND_CCCDCu", oldCMND_CCCD)
            };

            object result = DatabaseHelper.ExecuteScalar(query, parameters);
            int count = (result != null) ? Convert.ToInt32(result) : 0;

            return count > 0; // Nếu count > 0 tức là có mã khác trùng
        }

        // Kiểm tra Email đã tồn tại chưa
        public static bool GetKhachHangByEmail(string email)
        {
            string query = "SELECT COUNT(*) FROM KhachHang WHERE Email = @Email";
            SqlParameter[] parameters =
            {
                new SqlParameter("@Email", email)
            };

            object result = DatabaseHelper.ExecuteScalar(query, parameters);
            int count = (result != null) ? Convert.ToInt32(result) : 0;

            return count > 0; // Nếu count > 0 thì số điện thoại đã tồn tại
        }

        // Kiểm tra số điện thoại đã tồn tại chưa (trong trường hợp sửa thông tin)
        public static bool Email_Edit(string email, string oldEmail)
        {
            string query = "SELECT COUNT(*) FROM KhachHang WHERE Email = @Email AND Email <> @EmailCu";
            SqlParameter[] parameters =
            {
                new SqlParameter("@Email", email),
                new SqlParameter("@EmailCu", oldEmail)
            };

            object result = DatabaseHelper.ExecuteScalar(query, parameters);
            int count = (result != null) ? Convert.ToInt32(result) : 0;

            return count > 0; // Nếu count > 0 tức là có mã khác trùng
        }

        /// <summary>
        /// //////////////////////////////////////
        /// </summary>
        /// <param name="nv"></param>
        /// <returns></returns>
        /// 

        // Thêm khách hàng
        public static bool AddKhachHang(KhachHangDTO kh)
        {
            string query = "INSERT INTO KhachHang (HoTen, NgaySinh, CMND_CCCD, DiaChi, DienThoai, Email, NgayCap, TrangThai) " +
                           "VALUES (@HoTen, @NgaySinh, @CMND_CCCD, @DiaChi, @DienThoai, @Email, @NgayCap, @TrangThai)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@HoTen", kh.HoTen),
                new SqlParameter("@NgaySinh", (object)kh.NgaySinh ?? DBNull.Value),
                new SqlParameter("@CMND_CCCD", kh.CMND_CCCD),
                new SqlParameter("@DiaChi", kh.DiaChi),
                new SqlParameter("@DienThoai", kh.DienThoai),
                new SqlParameter("@Email", kh.Email),
                new SqlParameter("@NgayCap", (object)kh.NgayCap ?? DBNull.Value),
                new SqlParameter("@TrangThai", kh.TrangThai)
            };
            int result = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return result > 0; // Trả về true nếu thêm thành công
        }

        // Cập nhật thông tin khách hàng
        public static bool UpdateKhachHang(KhachHangDTO kh)
        {
            string query = "UPDATE KhachHang SET HoTen = @HoTen, NgaySinh = @NgaySinh, CMND_CCCD = @CMND_CCCD, " +
                           "DiaChi = @DiaChi, DienThoai = @DienThoai, Email = @Email, NgayCap = @NgayCap, TrangThai = @TrangThai " +
                           "WHERE MaKH = @MaKH";
            SqlParameter[] parameters =
            {
                new SqlParameter("@MaKH", kh.MaKH),
                new SqlParameter("@HoTen", kh.HoTen),
                new SqlParameter("@NgaySinh", kh.NgaySinh ?? (object)DBNull.Value),
                new SqlParameter("@CMND_CCCD", kh.CMND_CCCD),
                new SqlParameter("@DiaChi", kh.DiaChi ?? null),
                new SqlParameter("@DienThoai", kh.DienThoai ?? null),
                new SqlParameter("@Email", kh.Email),
                new SqlParameter("@NgayCap", (object)kh.NgayCap ?? DBNull.Value),
                new SqlParameter("@TrangThai", kh.TrangThai)
            };
            int result = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return result > 0; // Trả về true nếu cập nhật thành công
        }

        // Xóa khách hàng
        //public static bool DeleteKhachHang(int maKH)
        //{
        //    string query = "DELETE FROM KhachHang WHERE MaKH = @MaKH";
        //    SqlParameter[] parameters =
        //    {
        //        new SqlParameter("@MaKH", maKH)
        //    };
        //    int result = DatabaseHelper.ExecuteNonQuery(query, parameters);
        //    return result > 0; // Trả về true nếu xóa thành công
        //}

        public static bool DeleteKhachHang(int maKH)
        {
            // BƯỚC 1: KIỂM TRA SỔ TIẾT KIỆM CỦA KHÁCH HÀNG
            // Cần một phương thức trong SoTietKiemDAL hoặc SoTietKiemBLL để kiểm tra
            // xem có sổ tiết kiệm nào thuộc về khách hàng này không.

            if (SoTietKiemBLL.KiemTraSoTietKiemCuaKhachHang(maKH)) // Hàm này sẽ được tạo
            {
                MessageBox.Show("Không thể xóa khách hàng này vì khách hàng vẫn còn sổ tiết kiệm đang hoạt động hoặc đã đóng. Vui lòng đóng/tất toán tất cả sổ tiết kiệm của khách hàng trước khi xóa.", "Lỗi xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false; // Chặn xóa
            }

            // BƯỚC 2: XÓA CÁC GIAO DỊCH LIÊN QUAN ĐẾN KHÁCH HÀNG TRỰC TIẾP HOẶC GIÁN TIẾP
            // Trong trường hợp này, bảng SoTietKiem tham chiếu KhachHang.
            // Bảng GiaoDichTietKiem tham chiếu SoTietKiem.
            // Bạn không muốn xóa sổ tiết kiệm, nên cách tốt nhất là chỉ cho phép xóa KH khi không còn sổ nào.

            // Nếu đã vượt qua kiểm tra sổ tiết kiệm, thì tiến hành xóa khách hàng
            string query = "DELETE FROM KhachHang WHERE MaKH = @MaKH";
            SqlParameter[] parameters =
            {
                new SqlParameter("@MaKH", maKH)
            };
            int result = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return result > 0;
        }

        // Get HoTen Khach Hang by MaKH
        public static string GetHoTenKhachHangByMaKH(int maKH)
        {
            string query = "SELECT HoTen FROM KhachHang WHERE MaKH = @MaKH";
            SqlParameter[] parameters =
            {
                new SqlParameter("@MaKH", maKH)
            };
            object result = DatabaseHelper.ExecuteScalar(query, parameters);
            return result != null ? result.ToString() : string.Empty; // Trả về tên khách hàng hoặc chuỗi rỗng nếu không tìm thấy
        }

        // Get Khách Hàng có Số Tiết Kiệm
        public static List<KhachHangDTO> GetKhachHangBySoTietKiem()
        {
            List<KhachHangDTO> ds = new List<KhachHangDTO>();
            string query = "SELECT DISTINCT * FROM KhachHang WHERE MaKH IN ( SELECT MaKH FROM SoTietKiem )";

            DataTable dt = DatabaseHelper.ExecuteQuery(query);
            foreach (DataRow dr in dt.Rows)
            {
                KhachHangDTO khachHang = new KhachHangDTO
                {
                    MaKH = Convert.ToInt32(dr["MaKH"]),
                    HoTen = dr["HoTen"].ToString(),
                    NgaySinh = dr["NgaySinh"] as DateTime?,
                    CMND_CCCD = dr["CMND_CCCD"].ToString(),
                    DiaChi = dr["DiaChi"].ToString(),
                    DienThoai = dr["DienThoai"].ToString(),
                    Email = dr["Email"].ToString(),
                    NgayCap = dr["NgayCap"] as DateTime?,
                    TrangThai = Convert.ToBoolean(dr["TrangThai"])
                };
                ds.Add(khachHang);
            }
            return ds;
        }

        // Seacrh Khách Hàng có Số Tiết Kiệm theo tên
        public static List<KhachHangDTO> SearchKhachHangByNameAndSoTietKiem(string name)
        {
            List<KhachHangDTO> ds = new List<KhachHangDTO>();

            string query = @"
                SELECT DISTINCT * 
                FROM KhachHang 
                WHERE MaKH IN (SELECT MaKH FROM SoTietKiem)
                AND HoTen LIKE @name";

            SqlParameter[] parameters = {
                new SqlParameter("@name", "%" + name + "%")
            };

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

            foreach (DataRow dr in dt.Rows)
            {
                KhachHangDTO khachHang = new KhachHangDTO
                {
                    MaKH = Convert.ToInt32(dr["MaKH"]),
                    HoTen = dr["HoTen"].ToString(),
                    NgaySinh = dr["NgaySinh"] as DateTime?,
                    CMND_CCCD = dr["CMND_CCCD"].ToString(),
                    DiaChi = dr["DiaChi"].ToString(),
                    DienThoai = dr["DienThoai"].ToString(),
                    Email = dr["Email"].ToString(),
                    NgayCap = dr["NgayCap"] as DateTime?,
                    TrangThai = Convert.ToBoolean(dr["TrangThai"])
                };
                ds.Add(khachHang);
            }

            return ds;
        }

        // Get Email by MaKH
        public static string GetEmailByMaKH(int maKH)
        {
            string query = "SELECT Email FROM KhachHang WHERE MaKH = @MaKH";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKH", maKH)
            };
            DataTable dataTable = DatabaseHelper.ExecuteQuery(query, parameters);
            if (dataTable.Rows.Count > 0)
            {
                return dataTable.Rows[0]["Email"]?.ToString();
            }
            return null;
        }
    }
}
