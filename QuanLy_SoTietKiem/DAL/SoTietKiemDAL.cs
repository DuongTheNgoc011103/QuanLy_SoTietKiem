﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QuanLy_SoTietKiem.DTO;
using System.Data.SqlClient;
using System.Data;

namespace QuanLy_SoTietKiem.DAL
{
    public class SoTietKiemDAL
    {
        // Lấy danh sách sổ tiết kiệm theo mã loại sổ tiết kiệm
        public static List<SoTietKiemDTO> GetSoTietKiemByMaLoai(int maLoai)
        {
            List<SoTietKiemDTO> soTietKiems = new List<SoTietKiemDTO>();
            string query = @"SELECT stk.MaSo, stk.MaKH, kh.HoTen, stk.MaLoai, stk.SoTienGoc, stk.TrangThai
                            FROM SoTietKiem stk
                            JOIN KhachHang kh ON stk.MaKH = kh.MaKH
                            WHERE stk.MaLoai = @MaLoai";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaLoai", maLoai)
            };
            DataTable dataTable = DatabaseHelper.ExecuteQuery(query, parameters);
            foreach (DataRow row in dataTable.Rows)
            {
                SoTietKiemDTO soTietKiem = new SoTietKiemDTO
                {
                    MaSo = row["MaSo"] != DBNull.Value ? Convert.ToInt32(row["MaSo"]) : 0,
                    MaKH = row["MaKH"] != DBNull.Value ? Convert.ToInt32(row["MaKH"]) : 0,
                    HoTen = row["HoTen"]?.ToString(),
                    MaLoai = row["MaLoai"] != DBNull.Value ? Convert.ToInt32(row["MaLoai"]) : 0,
                    SoTienGoc = row["SoTienGoc"] != DBNull.Value ? Convert.ToDecimal(row["SoTienGoc"]) : 0,
                    TrangThai = row["TrangThai"]?.ToString()
                };
                soTietKiems.Add(soTietKiem);
            }
            return soTietKiems;
        }

        // GetSoTietKiemByMaKH
        public static List<SoTietKiemDTO> GetSoTietKiemByMaKH(int maKH)
        {
            List<SoTietKiemDTO> soTietKiems = new List<SoTietKiemDTO>();
            string query = @"SELECT stk.MaSo, stk.SoTienGoc, loai.LaiSuatCoBan, stk.NgayMo, stk.KyHan, stk.TrangThai
                            FROM SoTietKiem stk
                            JOIN KhachHang kh ON stk.MaKH = kh.MaKH
                            JOIN LoaiSoTietKiem loai ON stk.MaLoai = loai.MaLoai
                            WHERE stk.MaKH = @MaKH";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKH", maKH)
            };
            DataTable dataTable = DatabaseHelper.ExecuteQuery(query, parameters);
            foreach (DataRow row in dataTable.Rows)
            {
                SoTietKiemDTO soTietKiem = new SoTietKiemDTO
                {
                    MaSo = row["MaSo"] != DBNull.Value ? Convert.ToInt32(row["MaSo"]) : 0,
                    SoTienGoc = row["SoTienGoc"] != DBNull.Value ? Convert.ToDecimal(row["SoTienGoc"]) : 0,
                    LaiSuatCoBan = row["LaiSuatCoBan"] != DBNull.Value ? Convert.ToSingle(row["LaiSuatCoBan"]) : 0,
                    NgayMo = row["NgayMo"] != DBNull.Value ? Convert.ToDateTime(row["NgayMo"]) : DateTime.Now,
                    KyHan = row["KyHan"] != DBNull.Value ? (int?)Convert.ToInt32(row["KyHan"]) : null,
                    TrangThai = row["TrangThai"]?.ToString()
                };
                soTietKiems.Add(soTietKiem);
            }
            return soTietKiems;
        }

        // GetSoTietKiemByCCCDKH
        public static List<SoTietKiemDTO> GetSoTietKiemByCCCDKH(string CMND_CCCD)
        {
            List<SoTietKiemDTO> soTietKiems = new List<SoTietKiemDTO>();
            string query = @"SELECT stk.MaSo, stk.SoTienGoc, loai.LaiSuatCoBan, stk.NgayMo, stk.KyHan, stk.TrangThai, kh.HoTen, loai.MaLoai, loai.TenLoai
                            FROM SoTietKiem stk
                            JOIN KhachHang kh ON stk.MaKH = kh.MaKH
                            JOIN LoaiSoTietKiem loai ON stk.MaLoai = loai.MaLoai
                            WHERE kh.CMND_CCCD = @CMND_CCCD";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CMND_CCCD", CMND_CCCD)
            };
            DataTable dataTable = DatabaseHelper.ExecuteQuery(query, parameters);
            foreach (DataRow row in dataTable.Rows)
            {
                SoTietKiemDTO soTietKiem = new SoTietKiemDTO
                {
                    MaSo = row["MaSo"] != DBNull.Value ? Convert.ToInt32(row["MaSo"]) : 0,
                    HoTen = row["HoTen"]?.ToString(),
                    MaLoai = row["MaLoai"] != DBNull.Value ? Convert.ToInt32(row["MaLoai"]) : 0,
                    TenLoai = row["TenLoai"]?.ToString(),
                    SoTienGoc = row["SoTienGoc"] != DBNull.Value ? Convert.ToDecimal(row["SoTienGoc"]) : 0,
                    LaiSuatCoBan = row["LaiSuatCoBan"] != DBNull.Value ? Convert.ToSingle(row["LaiSuatCoBan"]) : 0,
                    NgayMo = row["NgayMo"] != DBNull.Value ? Convert.ToDateTime(row["NgayMo"]) : DateTime.Now,
                    KyHan = row["KyHan"] != DBNull.Value ? (int?)Convert.ToInt32(row["KyHan"]) : null,
                    TrangThai = row["TrangThai"]?.ToString()
                };
                soTietKiems.Add(soTietKiem);
            }
            return soTietKiems;
        }

        // Lấy danh sách tất cả sổ tiết kiệm
        public static List<SoTietKiemDTO> GetAllSoTietKiem()
        {
            List<SoTietKiemDTO> soTietKiems = new List<SoTietKiemDTO>();
            string query = @"SELECT stk.*, kh.HoTen, loai.TenLoai
                            FROM SoTietKiem stk
                            JOIN KhachHang kh ON stk.MaKH = kh.MaKH
                            JOIN LoaiSoTietKiem loai ON stk.MaLoai = loai.MaLoai";
            DataTable dataTable = DatabaseHelper.ExecuteQuery(query);
            foreach (DataRow row in dataTable.Rows)
            {
                SoTietKiemDTO soTietKiem = new SoTietKiemDTO
                {
                    MaSo = row["MaSo"] != DBNull.Value ? Convert.ToInt32(row["MaSo"]) : 0,
                    MaKH = row["MaKH"] != DBNull.Value ? Convert.ToInt32(row["MaKH"]) : 0,
                    HoTen = row["HoTen"]?.ToString(),
                    MaLoai = row["MaLoai"] != DBNull.Value ? Convert.ToInt32(row["MaLoai"]) : 0,
                    TenLoai = row["TenLoai"]?.ToString(),
                    SoTienGoc = row["SoTienGoc"] != DBNull.Value ? Convert.ToDecimal(row["SoTienGoc"]) : 0,
                    NgayMo = row["NgayMo"] != DBNull.Value ? Convert.ToDateTime(row["NgayMo"]) : DateTime.Now,
                    KyHan = row["KyHan"] != DBNull.Value ? (int?)Convert.ToInt32(row["KyHan"]) : null,
                    TrangThai = row["TrangThai"]?.ToString()
                };
                soTietKiems.Add(soTietKiem);
            }
            return soTietKiems;
        }

        // Search Ho Ten Khach Hang
        public static List<SoTietKiemDTO> SearchSoTietKiemByHoTen(string hoTen)
        {
            List<SoTietKiemDTO> soTietKiems = new List<SoTietKiemDTO>();
            string query = @"SELECT stk.*, kh.HoTen, loai.TenLoai
                            FROM SoTietKiem stk
                            JOIN KhachHang kh ON stk.MaKH = kh.MaKH
                            JOIN LoaiSoTietKiem loai ON stk.MaLoai = loai.MaLoai
                            WHERE kh.HoTen LIKE @HoTen";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@HoTen", "%" + hoTen + "%")
            };
            DataTable dataTable = DatabaseHelper.ExecuteQuery(query, parameters);
            foreach (DataRow row in dataTable.Rows)
            {
                SoTietKiemDTO soTietKiem = new SoTietKiemDTO
                {
                    MaSo = row["MaSo"] != DBNull.Value ? Convert.ToInt32(row["MaSo"]) : 0,
                    MaKH = row["MaKH"] != DBNull.Value ? Convert.ToInt32(row["MaKH"]) : 0,
                    HoTen = row["HoTen"]?.ToString(),
                    MaLoai = row["MaLoai"] != DBNull.Value ? Convert.ToInt32(row["MaLoai"]) : 0,
                    TenLoai = row["TenLoai"]?.ToString(),
                    SoTienGoc = row["SoTienGoc"] != DBNull.Value ? Convert.ToDecimal(row["SoTienGoc"]) : 0,
                    NgayMo = row["NgayMo"] != DBNull.Value ? Convert.ToDateTime(row["NgayMo"]) : DateTime.MinValue,
                    KyHan = row["KyHan"] != DBNull.Value ? (int?)Convert.ToInt32(row["KyHan"]) : null,
                    TrangThai = row["TrangThai"]?.ToString()
                };
                soTietKiems.Add(soTietKiem);
            }
            return soTietKiems;
        }

        // Thêm sổ tiết kiệm
        public static bool AddSoTietKiem(SoTietKiemDTO soTietKiem)
        {
            string query = @"INSERT INTO SoTietKiem (MaKH, MaLoai, NgayMo, KyHan, SoTienGoc, TrangThai)
                             VALUES (@MaKH, @MaLoai, @NgayMo, @KyHan, @SoTienGoc, @TrangThai)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKH", soTietKiem.MaKH),
                new SqlParameter("@MaLoai", soTietKiem.MaLoai),
                new SqlParameter("@NgayMo", soTietKiem.NgayMo),
                new SqlParameter("@KyHan", (object)soTietKiem.KyHan ?? DBNull.Value),
                new SqlParameter("@SoTienGoc", soTietKiem.SoTienGoc),
                new SqlParameter("@TrangThai", soTietKiem.TrangThai ?? (object)DBNull.Value)
            };
            int result = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return result > 0;
        }

        // Cập nhật sổ tiết kiệm
        public static bool UpdateSoTietKiem(SoTietKiemDTO soTietKiem)
        {
            string query = @"UPDATE SoTietKiem
                             SET KyHan = @KyHan, SoTienGoc = @SoTienGoc, TrangThai = @TrangThai
                             WHERE MaSo = @MaSo";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaSo", soTietKiem.MaSo),
                new SqlParameter("@KyHan", (object)soTietKiem.KyHan ?? DBNull.Value),
                new SqlParameter("@SoTienGoc", soTietKiem.SoTienGoc),
                new SqlParameter("@TrangThai", soTietKiem.TrangThai ?? (object)DBNull.Value)
            };
            int result = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return result > 0;
        }

        // Xóa sổ tiết kiệm theo mã sổ
        public static bool DeleteSoTietKiem(int maSo)
        {
            string query = @"DELETE FROM SoTietKiem WHERE MaSo = @MaSo";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaSo", maSo)
            };
            int result = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return result > 0;
        }

        // Lấy MaSo lớn nhất trong bảng SoTietKiem
        public static int? GetMaxMaSo()
        {
            string query = "SELECT MAX(MaSo) FROM SoTietKiem";
            object result = DatabaseHelper.ExecuteScalar(query);

            if (result != DBNull.Value && int.TryParse(result.ToString(), out int maSo))
            {
                return maSo;
            }
            return null;
        }

        // Cập nhật trạng thái sổ tiết kiệm theo mã khách hàng
        public static void CapNhatTrangThaiSoTietKiem(int maKH, string trangThai)
        {
            string query = "UPDATE SoTietKiem SET TrangThai = @TrangThai " +
                   "WHERE MaKH = @MaKH AND TrangThai = N'Ngưng hoạt động'";

            SqlParameter[] parameters = {
                new SqlParameter("@TrangThai", trangThai),
                new SqlParameter("@MaKH", maKH)
            };
            DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // Update trạng thái sổ tiết kiệm theo mã sổ
        public static void UpdateTrangThaiSoTietKiem(int maSo, string trangThai)
        {
            string query = "UPDATE SoTietKiem SET TrangThai = @TrangThai WHERE MaSo = @MaSo";
            SqlParameter[] parameters = {
                new SqlParameter("@TrangThai", trangThai),
                new SqlParameter("@MaSo", maSo)
            };
            DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // Cap nhat so tien goc khi gửi
        public static void CapNhatSoTienGoc(int maSo, decimal soTienGoc)
        {
            string query = "UPDATE SoTietKiem SET SoTienGoc = SoTienGoc + @SoTienGoc WHERE MaSo = @MaSo";
            SqlParameter[] parameters = {
                new SqlParameter("@SoTienGoc", soTienGoc),
                new SqlParameter("@MaSo", maSo)
            };
            DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // Cập nhật số tiền gốc khi rút
        public static void CapNhatSoTienGocKhiRut(int maSo, decimal soTienGoc)
        {
            string query = "UPDATE SoTietKiem SET SoTienGoc = SoTienGoc - @SoTienGoc WHERE MaSo = @MaSo";
            SqlParameter[] parameters = {
                new SqlParameter("@SoTienGoc", soTienGoc),
                new SqlParameter("@MaSo", maSo)
            };
            DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // Fix for CS0161: Ensure all code paths in CapNhatSoDaoHan return a value
        public static List<int> CapNhatSoDaoHan()
        {
            List<int> maSoDaoHan = new List<int>();

            // Lấy danh sách các sổ tiết kiệm đang hoạt động, có kỳ hạn, và ngày đáo hạn (tính toán) nhỏ hơn hoặc bằng ngày hiện tại
            string query = @"
            SELECT MaSo, MaKH, KyHan, SoTienGoc, NgayMo,
                    DATEADD(month, KyHan, NgayMo) AS NgayDaoHanCalculated
                    FROM SoTietKiem
                    WHERE TrangThai = N'Đang hoạt động'
                    AND KyHan IS NOT NULL
                    AND DATEADD(month, KyHan, NgayMo) <= GETDATE()";

            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            foreach (DataRow row in dt.Rows)
            {
                int maSo = Convert.ToInt32(row["MaSo"]);

                // Cập nhật trạng thái sổ thành "Đáo hạn"
                string updateQuery = "UPDATE SoTietKiem SET TrangThai = N'Đáo hạn' WHERE MaSo = @MaSo";
                Dictionary<string, object> dictParameters = new Dictionary<string, object>
                {
                    { "@MaSo", maSo }
                };
                SqlParameter[] sqlParameters = dictParameters.Select(p => new SqlParameter(p.Key, p.Value)).ToArray();

                // Gọi ExecuteNonQuery với mảng SqlParameter[]
                if (DatabaseHelper.ExecuteNonQuery(updateQuery, sqlParameters) > 0)
                {
                    maSoDaoHan.Add(maSo);
                }
            }

            return maSoDaoHan;
        }

        // Lấy chi tiết sổ tiết kiệm theo mã sổ
        public static SoTietKiemDTO GetSoTietKiemChiTiet(int maSo)
        {
            string query = @"
            SELECT STK.MaSo, STK.MaKH, KH.HoTen AS TenKhachHang, KH.Email AS EmailKhachHang, 
                   STK.MaLoai, LSTK.TenLoai, STK.NgayMo, STK.KyHan, STK.SoTienGoc, STK.TrangThai,
                   CASE WHEN STK.KyHan IS NOT NULL THEN DATEADD(month, STK.KyHan, STK.NgayMo) ELSE NULL END AS NgayDaoHanCalculated
            FROM SoTietKiem AS STK
            JOIN KhachHang AS KH ON STK.MaKH = KH.MaKH
            JOIN LoaiSoTietKiem AS LSTK ON STK.MaLoai = LSTK.MaLoai
            WHERE STK.MaSo = @MaSo";

            Dictionary<string, object> dictParameters = new Dictionary<string, object>
            {
                { "@MaSo", maSo }
            };
            SqlParameter[] sqlParameters = dictParameters.Select(p => new SqlParameter(p.Key, p.Value)).ToArray();
            DataTable dt = DatabaseHelper.ExecuteQuery(query, sqlParameters);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new SoTietKiemDTO
                {
                    MaSo = Convert.ToInt32(row["MaSo"]),
                    MaKH = Convert.ToInt32(row["MaKH"]),
                    MaLoai = Convert.ToInt32(row["MaLoai"]),
                    TenLoai = row["TenLoai"].ToString(),
                    NgayMo = Convert.ToDateTime(row["NgayMo"]),
                    KyHan = row["KyHan"] is DBNull ? (int?)null : Convert.ToInt32(row["KyHan"]),
                    SoTienGoc = Convert.ToDecimal(row["SoTienGoc"]),
                    TrangThai = row["TrangThai"].ToString(),
                    // Lấy NgayDaoHan đã được tính toán từ query
                    NgayDaoHan = row["NgayDaoHanCalculated"] is DBNull ? (DateTime?)null : Convert.ToDateTime(row["NgayDaoHanCalculated"])
                };
            }
            return null;
        }

        // cập nhật ngày mở thành ngày hiện tại
        public static void CapNhatNgayMo(int maSo)
        {
            string query = "UPDATE SoTietKiem SET NgayMo = GETDATE() WHERE MaSo = @MaSo";
            SqlParameter[] parameters = {
                new SqlParameter("@MaSo", maSo)
            };
            DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // GetSoTietKiemInfoForCalculation
        public static SoTietKiemDTO GetSoTietKiemInfoForCalculation(int maSo)
        {
            string query = @"
            SELECT STK.MaSo, STK.MaKH, KH.HoTen AS TenKhachHang, 
                   STK.MaLoai, LSTK.TenLoai, STK.NgayMo, STK.KyHan, 
                   STK.SoTienGoc, STK.TrangThai, LSTK.LaiSuatCoBan
            FROM SoTietKiem AS STK
            JOIN KhachHang AS KH ON STK.MaKH = KH.MaKH
            JOIN LoaiSoTietKiem AS LSTK ON STK.MaLoai = LSTK.MaLoai
            WHERE STK.MaSo = @MaSo";
            SqlParameter[] parameters = {
                new SqlParameter("@MaSo", maSo)
            };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new SoTietKiemDTO
                {
                    MaSo = Convert.ToInt32(row["MaSo"]),
                    MaKH = Convert.ToInt32(row["MaKH"]),
                    HoTen = row["TenKhachHang"].ToString(),
                    MaLoai = Convert.ToInt32(row["MaLoai"]),
                    TenLoai = row["TenLoai"].ToString(),
                    NgayMo = Convert.ToDateTime(row["NgayMo"]),
                    KyHan = row["KyHan"] is DBNull ? (int?)null : Convert.ToInt32(row["KyHan"]),
                    SoTienGoc = Convert.ToDecimal(row["SoTienGoc"]),
                    TrangThai = row["TrangThai"].ToString(),
                    LaiSuatCoBan = Convert.ToSingle(row["LaiSuatCoBan"])
                };
            }
            return null;
        }

        // Đếm số lượng sổ tiết kiệm theo mã khách hàng
        public static int CountSoTietKiemByMaKH(int maKH)
        {
            string query = "SELECT COUNT(*) FROM SoTietKiem WHERE MaKH = @MaKH";
            SqlParameter[] parameters = { new SqlParameter("@MaKH", maKH) };
            // Giả sử DatabaseHelper.ExecuteScalar trả về đối tượng và cần ép kiểu
            object result = DatabaseHelper.ExecuteScalar(query, parameters);
            return (result == null || result is DBNull) ? 0 : Convert.ToInt32(result);
        }

        // Đếm số lượng sổ tiết kiệm theo mã loại sổ tiết kiệm
        public static int CountSoTietKiemByMaLoai(int maLoai)
        {
            string query = "SELECT COUNT(*) FROM SoTietKiem WHERE MaLoai = @MaLoai";
            SqlParameter[] parameters = { new SqlParameter("@MaLoai", maLoai) };
            object result = DatabaseHelper.ExecuteScalar(query, parameters);
            return (result == null || result is DBNull) ? 0 : Convert.ToInt32(result);
        }

        // Tính lãi dự kiến cho sổ tiết kiệm
        public static SoTietKiemDTO TinhLaiDuKien(int maSo, DateTime ngayTinhToan, decimal soTien)
        {
            // Lấy thông tin sổ từ DAL
            SoTietKiemDTO soTietKiem = SoTietKiemDAL.GetSoTietKiemInfoForCalculation(maSo);

            if (soTietKiem == null)
            {
                return null; // Không tìm thấy sổ
            }

            // --- Logic tính toán lãi suất dự kiến ---
            decimal laiDuKien = 0;
            decimal soTienGoc = soTien;
            float laiSuatHangNam = soTietKiem.LaiSuatCoBan;

            // Tính số ngày gửi thực tế (hoặc số tháng)
            TimeSpan thoiGianGui = ngayTinhToan.Date - soTietKiem.NgayMo.Date;
            int soNgayGui = thoiGianGui.Days;

            if (soTietKiem.KyHan.HasValue && soTietKiem.KyHan.Value > 0)
            {
                // Sổ có kỳ hạn
                DateTime ngayDaoHanThucTe = soTietKiem.NgayMo.AddMonths(soTietKiem.KyHan.Value).Date;

                if (ngayTinhToan.Date < ngayDaoHanThucTe.Date)
                {
                    // Tính lãi khi kiểm tra trước đáo hạn:
                    // Thường tính theo lãi suất không kỳ hạn cho số ngày thực gửi.
                    // Hoặc lãi suất phạt/phí nếu có chính sách rút trước hạn.
                    // Ở đây dùng một lãi suất không kỳ hạn giả định.
                    float laiSuatKhongKyHan = 0.1f; // Ví dụ: 0.1% / năm
                    laiDuKien = soTienGoc * (decimal)(laiSuatKhongKyHan / 100 / 365) * soNgayGui;
                }
                else
                {
                    // Đã đáo hạn hoặc đúng ngày đáo hạn:
                    // Lãi suất tính theo kỳ hạn đã gửi, cho đủ kỳ hạn hoặc đến ngày tính toán nếu vượt quá.
                    // Giả định tính theo đủ kỳ hạn đầu tiên (nếu đã qua ngày đáo hạn).
                    // Nếu muốn tính lãi cho nhiều kỳ hạn tái tục tự động thì phức tạp hơn.
                    decimal soNgayTinhLai = (ngayDaoHanThucTe - soTietKiem.NgayMo).Days;
                    if (soNgayTinhLai < 0) soNgayTinhLai = 0; // Tránh ngày đáo hạn trong quá khứ so với ngày mở

                    laiDuKien = soTienGoc * (decimal)(laiSuatHangNam / 100 / 365) * soNgayTinhLai;

                    // Nếu bạn muốn tính lãi cho cả thời gian sau đáo hạn nhưng chưa tất toán (thường là lãi suất không kỳ hạn)
                    if (ngayTinhToan.Date > ngayDaoHanThucTe.Date)
                    {
                        int soNgayQuaHan = (ngayTinhToan.Date - ngayDaoHanThucTe.Date).Days;
                        float laiSuatSauDaoHan = 0.1f; // Lãi suất áp dụng sau khi đáo hạn (thường là lãi suất không kỳ hạn)
                        laiDuKien += soTienGoc * (decimal)(laiSuatSauDaoHan / 100 / 365) * soNgayQuaHan;
                    }
                }
            }
            else
            {
                // Sổ không kỳ hạn (demand deposit)
                // Lãi suất thường rất thấp, tính theo ngày
                float laiSuatKhongKyHan = 0.1f; // Ví dụ: 0.1% / năm
                laiDuKien = soTienGoc * (decimal)(laiSuatKhongKyHan / 100 / 365) * soNgayGui;
            }

            soTietKiem.LaiDuKien = Math.Round(laiDuKien, 2); // Làm tròn 2 chữ số thập phân
            soTietKiem.TongSoTienCuoiKy = soTienGoc + soTietKiem.LaiDuKien;

            return soTietKiem;
        }

        // Lấy báo cáo số tiết kiệm theo chi nhánh
        public static List<SoTheoChiNhanhReportDTO> GetSoTheoChiNhanh()
        {
            List<SoTheoChiNhanhReportDTO> result = new List<SoTheoChiNhanhReportDTO>();
            string query = @"
                SELECT
                    CN.MaCN,
                    CN.TenChiNhanh,
                    CN.NguoiQuanLy,
                    COUNT(DISTINCT STK.MaSo) AS TongSoSo, -- Đếm số sổ duy nhất
                    ISNULL(SUM(STK.SoTienGoc), 0) AS TongSoTienGoc -- Tổng tiền gốc của các sổ đó
                FROM ChiNhanh AS CN
                LEFT JOIN NhanVien AS NV ON CN.MaCN = NV.MaCN
                LEFT JOIN GiaoDichTietKiem AS GD ON NV.MaNV = GD.MaNV
                    AND GD.LoaiGiaoDich = N'Mở sổ tiết kiệm'
                    -- Đảm bảo chỉ lấy giao dịch mở sổ ban đầu (MaGD nhỏ nhất cho sổ đó)
                    AND GD.MaGD = (
                        SELECT MIN(GD_Sub.MaGD)
                        FROM GiaoDichTietKiem AS GD_Sub
                        WHERE GD_Sub.MaSo = GD.MaSo
                          AND GD_Sub.LoaiGiaoDich = N'Mở sổ tiết kiệm'
                    )
                LEFT JOIN SoTietKiem AS STK ON GD.MaSo = STK.MaSo AND STK.TrangThai = N'Đang hoạt động' -- Chỉ tính các sổ đang hoạt động
                GROUP BY CN.MaCN, CN.TenChiNhanh, CN.NguoiQuanLy
                ORDER BY CN.MaCN ASC;
            ";

            DataTable dt = DatabaseHelper.ExecuteQuery(query, null); // Không có tham số

            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SoTheoChiNhanhReportDTO
                {
                    MaCN = Convert.ToInt32(row["MaCN"]),
                    TenChiNhanh = row["TenChiNhanh"].ToString(),
                    NguoiQuanLy = row["NguoiQuanLy"] is DBNull ? string.Empty : row["NguoiQuanLy"].ToString(),
                    TongSoSo = Convert.ToInt32(row["TongSoSo"]),
                    TongSoTienGoc = Convert.ToDecimal(row["TongSoTienGoc"])
                });
            }
            return result;
        }
    }
}
