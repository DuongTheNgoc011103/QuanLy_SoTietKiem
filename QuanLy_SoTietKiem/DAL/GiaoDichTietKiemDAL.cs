using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QuanLy_SoTietKiem.DTO;
using System.Data;
using System.Data.SqlClient;
using QuanLy_SoTietKiem.Utils;
using QuanLy_SoTietKiem.DAL; // Thêm namespace DAL để sử dụng DatabaseHelper

namespace QuanLy_SoTietKiem.DAL
{
    public class GiaoDichTietKiemDAL
    {
        // Lấy danh sách giao dịch gửi tiền tiết kiệm
        public static List<GiaoDichTietKiemDTO> GetGiaoDichGuiTien()
        {
            string query = "SELECT GD.MaGD, GD.MaSo, GD.LoaiGiaoDich, GD.SoTien, GD.NgayGD, GD.LaiSuatApDung, GD.GhiChu, NV.MaNV, NV.HoTen " +
                           "FROM GiaoDichTietKiem AS GD " +
                           "JOIN NhanVien AS NV ON GD.MaNV = NV.MaNV ";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);
            List<GiaoDichTietKiemDTO> giaoDichList = new List<GiaoDichTietKiemDTO>();
            foreach (DataRow row in dt.Rows)
            {
                GiaoDichTietKiemDTO giaoDich = new GiaoDichTietKiemDTO
                {
                    MaGD = Convert.ToInt32(row["MaGD"]),
                    MaSo = Convert.ToInt32(row["MaSo"]),
                    LoaiGiaoDich = row["LoaiGiaoDich"].ToString(),
                    SoTien = Convert.ToDecimal(row["SoTien"]),
                    NgayGD = row["NgayGD"] as DateTime?,
                    LaiSuatApDung = row["LaiSuatApDung"] is DBNull ? (float?)null : Convert.ToSingle(row["LaiSuatApDung"]),
                    GhiChu = row["GhiChu"].ToString(),
                    MaNV = row["MaNV"] is DBNull ? (int?)null : Convert.ToInt32(row["MaNV"]),
                    HoTen_NV = row["HoTen"].ToString(), // Sửa thành HoTen_NV để rõ ràng
                    HoTen = row["HoTen"].ToString()
                };
                giaoDichList.Add(giaoDich);
            }
            return giaoDichList;
        }

        // Thêm giao dịch tiết kiệm
        public static bool ThemGiaoDich(GiaoDichTietKiemDTO giaoDich)
        {
            string query = "INSERT INTO GiaoDichTietKiem (MaSo, LoaiGiaoDich, SoTien, NgayGD, LaiSuatApDung, GhiChu, MaNV) " +
                           "VALUES (@MaSo, @LoaiGiaoDich, @SoTien, @NgayGD, @LaiSuatApDung, @GhiChu, @MaNV)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaSo", giaoDich.MaSo),
                new SqlParameter("@LoaiGiaoDich", giaoDich.LoaiGiaoDich),
                new SqlParameter("@SoTien", giaoDich.SoTien),
                new SqlParameter("@NgayGD", (object)giaoDich.NgayGD ?? DBNull.Value),
                new SqlParameter("@LaiSuatApDung", (object)giaoDich.LaiSuatApDung ?? DBNull.Value),
                new SqlParameter("@GhiChu", giaoDich.GhiChu ?? (object)DBNull.Value),
                new SqlParameter("@MaNV", (object)giaoDich.MaNV ?? DBNull.Value)
            };
            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        // Lấy mã giao dịch lớn nhất
        public static int? GetMaxMaGD()
        {
            string query = "SELECT MAX(MaGD) FROM GiaoDichTietKiem";
            object result = DatabaseHelper.ExecuteScalar(query);

            if (result != DBNull.Value && int.TryParse(result.ToString(), out int maGD))
            {
                return maGD;
            }
            return null;
        }

        // Search giao dịch tiết kiệm theo loại giao dịch
        public static List<GiaoDichTietKiemDTO> SearchGiaoDichByType(string loaiGiaoDich)
        {
            string query = "SELECT GD.MaGD, GD.MaSo, GD.LoaiGiaoDich, GD.SoTien, GD.NgayGD, GD.LaiSuatApDung, GD.GhiChu, NV.MaNV, NV.HoTen " +
                           "FROM GiaoDichTietKiem AS GD " +
                           "JOIN NhanVien AS NV ON GD.MaNV = NV.MaNV " +
                           "WHERE GD.LoaiGiaoDich LIKE @LoaiGiaoDich";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@LoaiGiaoDich", "%" + loaiGiaoDich + "%")
            };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
            List<GiaoDichTietKiemDTO> giaoDichList = new List<GiaoDichTietKiemDTO>();
            foreach (DataRow row in dt.Rows)
            {
                GiaoDichTietKiemDTO giaoDich = new GiaoDichTietKiemDTO
                {
                    MaGD = Convert.ToInt32(row["MaGD"]),
                    MaSo = Convert.ToInt32(row["MaSo"]),
                    LoaiGiaoDich = row["LoaiGiaoDich"].ToString(),
                    SoTien = Convert.ToDecimal(row["SoTien"]),
                    NgayGD = row["NgayGD"] as DateTime?,
                    LaiSuatApDung = row["LaiSuatApDung"] is DBNull ? (float?)null : Convert.ToSingle(row["LaiSuatApDung"]),
                    GhiChu = row["GhiChu"].ToString(),
                    MaNV = row["MaNV"] is DBNull ? (int?)null : Convert.ToInt32(row["MaNV"]),
                    HoTen = row["HoTen"].ToString()
                };
                giaoDichList.Add(giaoDich);
            }
            return giaoDichList;
        }

        // Count giao dịch theo mã nhân viên
        public static int CountGiaoDichByMaNV(int maNV)
        {
            string query = "SELECT COUNT(*) FROM GiaoDichTietKiem WHERE MaNV = @MaNV";
            SqlParameter[] parameters = { new SqlParameter("@MaNV", maNV) };
            object result = DatabaseHelper.ExecuteScalar(query, parameters);
            return (result == null || result is DBNull) ? 0 : Convert.ToInt32(result);
        }

        // Count giao dịch theo mã sổ tiết kiệm
        public static int CountGiaoDichByMaSo(int maSo)
        {
            string query = "SELECT COUNT(*) FROM GiaoDichTietKiem WHERE MaSo = @MaSo";
            SqlParameter[] parameters = { new SqlParameter("@MaSo", maSo) };
            object result = DatabaseHelper.ExecuteScalar(query, parameters);
            return (result == null || result is DBNull) ? 0 : Convert.ToInt32(result);
        }

        // Lấy danh sách giao dịch theo mã giao dịch
        public static GiaoDichTietKiemDTO GetGiaoDichByMaGD(int maGD)
        {
            string query = @"
                SELECT GD.MaGD, GD.MaSo, GD.LoaiGiaoDich, GD.SoTien, GD.NgayGD, GD.LaiSuatApDung, GD.GhiChu,
                       NV.MaNV, NV.HoTen AS HoTen_NV,
                       KH.HoTen AS HoTen_KH, KH.CMND_CCCD, KH.DiaChi AS DiaChi_KH,
                       LSTK.TenLoai
                        FROM GiaoDichTietKiem GD
                        LEFT JOIN NhanVien NV ON GD.MaNV = NV.MaNV
                        JOIN SoTietKiem STK ON GD.MaSo = STK.MaSo
                        JOIN KhachHang KH ON STK.MaKH = KH.MaKH
                        JOIN LoaiSoTietKiem LSTK ON STK.MaLoai = LSTK.MaLoai
                        WHERE GD.MaGD = @MaGD";

            SqlParameter[] parameters = {
                new SqlParameter("@MaGD", maGD)
            };

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
            if (dt.Rows.Count == 0) return null;

            DataRow row = dt.Rows[0];

            return new GiaoDichTietKiemDTO
            {
                MaGD = Convert.ToInt32(row["MaGD"]),
                MaSo = Convert.ToInt32(row["MaSo"]),
                LoaiGiaoDich = row["LoaiGiaoDich"].ToString(),
                SoTien = Convert.ToDecimal(row["SoTien"]),
                NgayGD = row["NgayGD"] as DateTime?,
                LaiSuatApDung = row["LaiSuatApDung"] != DBNull.Value ? (float?)Convert.ToSingle(row["LaiSuatApDung"]) : null,
                GhiChu = row["GhiChu"].ToString(),

                MaNV = row["MaNV"] != DBNull.Value ? (int?)Convert.ToInt32(row["MaNV"]) : null,
                HoTen_NV = row["HoTen_NV"].ToString(),

                HoTen_KH = row["HoTen_KH"].ToString(),
                CMND_CCCD = row["CMND_CCCD"].ToString(),
                DiaChi_KH = row["DiaChi_KH"].ToString(),

                TenLoai = row["TenLoai"].ToString(),
            };
        }

        // Lấy dữ liệu báo cáo rút tiền tiết kiệm cơ bản (không bao gồm các trường tính toán)
        public static RutTienReportDTO GetRutTienReportDataBasic(int maGD) // Đổi tên hàm để rõ mục đích
        {
            RutTienReportDTO reportData = null;

            string query = @"
            SELECT
                GD.MaGD, GD.NgayGD, GD.LoaiGiaoDich, GD.SoTien AS SoTienGiaoDich, GD.LaiSuatApDung, GD.GhiChu AS GhiChuGD,
                NV.HoTen AS TenNhanVien,
                STK.MaSo AS MaSoSTK, STK.NgayMo AS NgayMoSTK, STK.KyHan AS KyHanSTK, STK.SoTienGoc AS SoTienGocSTK,
                KH.HoTen AS HoTenKH, KH.CMND_CCCD, KH.DiaChi AS DiaChiKH, KH.DienThoai AS DienThoaiKH,
                LSTK.TenLoai AS TenLoaiSo
            FROM
                GiaoDichTietKiem AS GD
            JOIN NhanVien AS NV ON GD.MaNV = NV.MaNV
            JOIN SoTietKiem AS STK ON GD.MaSo = STK.MaSo
            JOIN KhachHang AS KH ON STK.MaKH = KH.MaKH
            JOIN LoaiSoTietKiem AS LSTK ON STK.MaLoai = LSTK.MaLoai
            WHERE
                GD.MaGD = @MaGD";

            SqlParameter[] parameters = { new SqlParameter("@MaGD", maGD) };

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                reportData = new RutTienReportDTO
                {
                    MaGD = Convert.ToInt32(row["MaGD"]),
                    NgayGD = Convert.ToDateTime(row["NgayGD"]),
                    LoaiGiaoDich = row["LoaiGiaoDich"].ToString(),
                    PhuongThucRut = row["GhiChuGD"].ToString().Contains("toàn bộ") ? "Rút toàn bộ" : "Rút một phần",
                    HoTen_NV = row["TenNhanVien"].ToString(),

                    HoTen_KH = row["HoTenKH"].ToString(),
                    CMND_CCCD = row["CMND_CCCD"].ToString(),
                    DiaChi_KH = row["DiaChiKH"] is DBNull ? string.Empty : row["DiaChiKH"].ToString(),

                    MaSo = Convert.ToInt32(row["MaSoSTK"]),
                    TenLoai = row["TenLoaiSo"].ToString(),
                    NgayMo = Convert.ToDateTime(row["NgayMoSTK"]),
                    KyHan = row["KyHanSTK"] is DBNull ? (int?)null : Convert.ToInt32(row["KyHanSTK"]),
                    NgayDaoHan = (row["KyHanSTK"] is DBNull || Convert.ToInt32(row["KyHanSTK"]) == 0) ? Convert.ToDateTime(row["NgayMoSTK"]).Date : Convert.ToDateTime(row["NgayMoSTK"]).AddMonths(Convert.ToInt32(row["KyHanSTK"])).Date,
                    SoTienGoc = Convert.ToDecimal(row["SoTienGocSTK"]),

                    GhiChuGiaoDich = row["GhiChuGD"].ToString(),

                    // Các trường này sẽ được truyền từ GUI, không lấy từ DB ở đây
                    SoTienYeuCauRut = 0, // Giá trị mặc định
                    LaiThucTe = 0,
                    PhiPhatTruocHan = 0,
                    TongTienThucNhan = 0,
                    TongTienBangChu = ""
                };
            }
            return reportData;
        }

        // Giao dịch theo ngày Report
        public static List<GiaoDichTheoNgayReportDTO> GetGiaoDichTheoNgay(DateTime tuNgay, DateTime denNgay)
        {
            List<GiaoDichTheoNgayReportDTO> result = new List<GiaoDichTheoNgayReportDTO>();
            string query = @"
                SELECT
                    GD.MaGD, GD.LoaiGiaoDich, GD.SoTien, GD.NgayGD, GD.LaiSuatApDung, GD.GhiChu,
                    KH.HoTen AS HoTenKH, KH.CMND_CCCD,
                    NV.HoTen AS TenNhanVien,
                    STK.MaSo, LSTK.TenLoai AS TenLoaiSo
                FROM GiaoDichTietKiem AS GD
                JOIN SoTietKiem AS STK ON GD.MaSo = STK.MaSo
                JOIN KhachHang AS KH ON STK.MaKH = KH.MaKH
                LEFT JOIN NhanVien AS NV ON GD.MaNV = NV.MaNV -- LEFT JOIN vì MaNV có thể NULL
                JOIN LoaiSoTietKiem AS LSTK ON STK.MaLoai = LSTK.MaLoai
                WHERE GD.NgayGD >= @TuNgay AND GD.NgayGD < @DenNgayPlusOneDay -- Lấy đến hết ngày DenNgay
                ORDER BY GD.NgayGD ASC";

            SqlParameter[] parameters = {
                new SqlParameter("@TuNgay", tuNgay.Date),
                new SqlParameter("@DenNgayPlusOneDay", denNgay.Date.AddDays(1)) // Lấy đến hết ngày kết thúc
            };

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters); // Sử dụng DatabaseHelper

            foreach (DataRow row in dt.Rows)
            {
                result.Add(new GiaoDichTheoNgayReportDTO
                {
                    MaGD = Convert.ToInt32(row["MaGD"]),
                    LoaiGiaoDich = row["LoaiGiaoDich"].ToString(),
                    SoTien = Convert.ToDecimal(row["SoTien"]),
                    NgayGD = Convert.ToDateTime(row["NgayGD"]),
                    LaiSuatApDung = row["LaiSuatApDung"] is DBNull ? (float?)null : Convert.ToSingle(row["LaiSuatApDung"]),
                    GhiChu = row["GhiChu"] is DBNull ? string.Empty : row["GhiChu"].ToString(),
                    HoTenKH = row["HoTenKH"].ToString(),
                    CMND_CCCD = row["CMND_CCCD"].ToString(),
                    TenNhanVien = row["TenNhanVien"] is DBNull ? "N/A" : row["TenNhanVien"].ToString(),
                    MaSo = Convert.ToInt32(row["MaSo"]),
                    TenLoaiSo = row["TenLoaiSo"].ToString()
                });
            }
            return result;
        }
    }
}
