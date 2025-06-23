using QuanLy_SoTietKiem.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    public class NhanVienDAL
    {
        // Lấy danh sách nhân viên theo mã chi nhánh
        public static List<NhanVienDTO> GetNhanVienByMaCN(string maCN)
        {
            List<NhanVienDTO> ds = new List<NhanVienDTO>();
            string query = "SELECT MaNV, HoTen, ChucVu FROM NhanVien WHERE MaCN = @MaCN";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaCN", maCN)
            };

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

            foreach(DataRow row in dt.Rows)
            {
                NhanVienDTO nv = new NhanVienDTO
                (
                    Convert.ToInt32(row["MaNV"]),
                    row["HoTen"].ToString(),
                    row["ChucVu"].ToString()
                );
                ds.Add(nv);
            }

            return ds;
        }


        //get all nhan vien
        public static List<NhanVienDTO> GetAllNhanVien()
        {
            List<NhanVienDTO> ds = new List<NhanVienDTO>();
            string query = "SELECT * FROM NhanVien";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                NhanVienDTO nv = new NhanVienDTO
                (
                    Convert.ToInt32(row["MaNV"]),
                    row["HoTen"].ToString(),
                    row["NgaySinh"] as DateTime?,
                    row["CMND_CCCD"].ToString(),
                    row["DiaChi"].ToString(),
                    row["DienThoai"].ToString(),
                    row["Email"].ToString(),
                    row["ChucVu"].ToString(),
                    row["PhongBan"].ToString(),
                    Convert.ToInt32(row["MaCN"])
                );
                ds.Add(nv);
            }
            return ds;
        }

        // Lấy danh sách nhân viên chưa có tài khoản
        public static List<NhanVienDTO> GetNhanVienChuaCoTaiKhoan()
        {
            List<NhanVienDTO> ds = new List<NhanVienDTO>();
            string query = "SELECT MaNV, HoTen, Email FROM NhanVien WHERE MaNV NOT IN (SELECT MaNV FROM TaiKhoanNguoiDung)";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                NhanVienDTO nv = new NhanVienDTO(
                    Convert.ToInt32(row["MaNV"]),
                    row["HoTen"].ToString(),
                    null, // chức vụ không cần thiết trong danh sách này
                    row["Email"].ToString()
                );
                ds.Add(nv);
            }
            return ds;
        }


        //search nhan vien by name
        public static List<NhanVienDTO> SearchNhanVienByName(string name)
        {
            List<NhanVienDTO> ds = new List<NhanVienDTO>();
            string query = "SELECT * FROM NhanVien WHERE HoTen LIKE @Name";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Name", "%" + name + "%")
            };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
            foreach (DataRow row in dt.Rows)
            {
                NhanVienDTO nv = new NhanVienDTO
                (
                    Convert.ToInt32(row["MaNV"]),
                    row["HoTen"].ToString(),
                    row["NgaySinh"] as DateTime?,
                    row["CMND_CCCD"].ToString(),
                    row["DiaChi"].ToString(),
                    row["DienThoai"].ToString(),
                    row["Email"].ToString(),
                    row["ChucVu"].ToString(),
                    row["PhongBan"].ToString(),
                    Convert.ToInt32(row["MaCN"])
                );
                ds.Add(nv);
            }
            return ds;
        }

        // Thêm nhân viên mới
        public static bool AddNhanVien(NhanVienDTO nv)
        {
            string query = "INSERT INTO NhanVien (HoTen, NgaySinh, CMND_CCCD, DiaChi, DienThoai, Email, ChucVu, PhongBan, MaCN) " +
                           "VALUES (@HoTen, @NgaySinh, @CMND_CCCD, @DiaChi, @DienThoai, @Email, @ChucVu, @PhongBan, @MaCN)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@HoTen", nv.HoTen),
                new SqlParameter("@NgaySinh", nv.NgaySinh ?? (object)DBNull.Value),
                new SqlParameter("@CMND_CCCD", nv.CMND_CCCD),
                new SqlParameter("@DiaChi", nv.DiaChi),
                new SqlParameter("@DienThoai", nv.DienThoai),
                new SqlParameter("@Email", nv.Email),
                new SqlParameter("@ChucVu", nv.ChucVu),
                new SqlParameter("@PhongBan", nv.PhongBan),
                new SqlParameter("@MaCN", nv.MaCN)
            };
            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        /// <summary>
        /// ////
        /// </summary>
        /// <param name="dienThoai"></param>
        /// <returns></returns>
        /// 

        // Kiểm tra số điện thoại đã tồn tại chưa
        public static bool GetNhanVienBySDT(string dienThoai)
        {
            string query = "SELECT COUNT(*) FROM NhanVien WHERE DienThoai = @DienThoai";
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
            string query = "SELECT COUNT(*) FROM NhanVien WHERE DienThoai = @DienThoai AND DienThoai <> @DienThoaiCu";
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
        public static bool GetNhanVienByCMND_CCCD(string CMND_CCCD)
        {
            string query = "SELECT COUNT(*) FROM NhanVien WHERE CMND_CCCD = @CMND_CCCD";
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
            string query = "SELECT COUNT(*) FROM NhanVien WHERE CMND_CCCD = @CMND_CCCD AND CMND_CCCD <> @CMND_CCCDCu";
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
        public static bool GetNhanVienByEmail(string email)
        {
            string query = "SELECT COUNT(*) FROM NhanVien WHERE Email = @Email";
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
            string query = "SELECT COUNT(*) FROM NhanVien WHERE Email = @Email AND Email <> @EmailCu";
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


        // Cập nhật thông tin nhân viên
        public static bool Update(NhanVienDTO nv)
        {
            string query = "UPDATE NhanVien SET HoTen = @HoTen, NgaySinh = @NgaySinh, CMND_CCCD = @CMND_CCCD, DiaChi = @DiaChi, DienThoai = @DienThoai, Email = @Email, ChucVu = @ChucVu, PhongBan = @PhongBan, MaCN = @MaCN WHERE MaNV = @MaNV";
            SqlParameter[] parameters =
            {
                new SqlParameter("@HoTen", nv.HoTen),
                new SqlParameter("@NgaySinh", nv.NgaySinh ?? (object)DBNull.Value),
                new SqlParameter("@CMND_CCCD", nv.CMND_CCCD),
                new SqlParameter("@DiaChi", nv.DiaChi ?? null),
                new SqlParameter("@DienThoai", nv.DienThoai ?? null),
                new SqlParameter("@Email", nv.Email),
                new SqlParameter("@ChucVu", nv.ChucVu ?? null),
                new SqlParameter("@PhongBan", nv.PhongBan ?? null),
                new SqlParameter("@MaCN", nv.MaCN),
                new SqlParameter("@MaNV", nv.MaNV)
            };
            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        // Xóa nhân viên theo mã nhân viên
        //public static bool DeleteNhanVien(int maNV)
        //{
        //    string query = "DELETE FROM NhanVien WHERE MaNV = @MaNV";
        //    SqlParameter[] parameters =
        //    {
        //        new SqlParameter("@MaNV", maNV)
        //    };
        //    return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        //}

        public static bool DeleteNhanVien(int maNV)
        {
            // BƯỚC 1: KIỂM TRA TÀI KHOẢN NGƯỜI DÙNG CỦA NHÂN VIÊN
            if (TaiKhoanBLL.KiemTraTaiKhoanCuaNhanVien(maNV)) // Hàm này sẽ được tạo
            {
                MessageBox.Show("Không thể xóa nhân viên này vì nhân viên vẫn có tài khoản người dùng đang hoạt động.", "Lỗi xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // BƯỚC 2: KIỂM TRA GIAO DỊCH TIẾT KIỆM CỦA NHÂN VIÊN
            if (GiaoDichTietKiemBLL.KiemTraGiaoDichCuaNhanVien(maNV)) // Hàm này sẽ được tạo
            {
                MessageBox.Show("Không thể xóa nhân viên này vì nhân viên đã thực hiện các giao dịch trong hệ thống. Vui lòng đảm bảo không có giao dịch nào liên quan đến nhân viên này.", "Lỗi xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Nếu đã vượt qua tất cả các kiểm tra, tiến hành xóa nhân viên
            string query = "DELETE FROM NhanVien WHERE MaNV = @MaNV";
            SqlParameter[] parameters =
            {
                new SqlParameter("@MaNV", maNV)
            };
            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        // Lấy thông tin nhân viên cho report
        public static List<NhanVienDTO> GetNhanVienForReport()
        {
            List<NhanVienDTO> ds = new List<NhanVienDTO>();

            // JOIN để lấy TenChiNhanh từ bảng ChiNhanh
            string query = @"SELECT nv.HoTen, nv.ChucVu, nv.Email, nv.PhongBan, nv.DiaChi, nv.DienThoai,
                            cn.TenChiNhanh, nv.CMND_CCCD, nv.NgaySinh
                            FROM NhanVien nv
                            LEFT JOIN ChiNhanh cn ON nv.MaCN = cn.MaCN";

            DataTable dt = DatabaseHelper.ExecuteQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                NhanVienDTO nv = new NhanVienDTO(
                    row["HoTen"].ToString(),
                    row["ChucVu"].ToString(),
                    row["Email"].ToString(),
                    row["PhongBan"].ToString(),
                    row["DiaChi"].ToString(),
                    row["DienThoai"].ToString(),
                    row["TenChiNhanh"].ToString(),
                    row["CMND_CCCD"].ToString(),
                    row["NgaySinh"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["NgaySinh"])
                );
                ds.Add(nv);
            }

            return ds;
        }

        // Get HoTen Nhan Vien by MaNV
        public static string GetHoTenByMaNV(int maNV)
        {
            string query = "SELECT HoTen FROM NhanVien WHERE MaNV = @MaNV";
            SqlParameter[] parameters =
            {
                new SqlParameter("@MaNV", maNV)
            };
            object result = DatabaseHelper.ExecuteScalar(query, parameters);
            return result != null ? result.ToString() : string.Empty;
        }
    }

}
