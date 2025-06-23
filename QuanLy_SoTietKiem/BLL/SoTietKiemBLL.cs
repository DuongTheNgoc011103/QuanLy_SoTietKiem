using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QuanLy_SoTietKiem.DTO;
using QuanLy_SoTietKiem.DAL;
using System.Windows.Forms;

namespace QuanLy_SoTietKiem.BLL
{
    public class SoTietKiemBLL
    {
        // Lấy danh sách sổ tiết kiệm theo mã loại sổ tiết kiệm
        public static List<SoTietKiemDTO> GetSoTietKiemByMaLoai(int maLoai)
        {
            return SoTietKiemDAL.GetSoTietKiemByMaLoai(maLoai);
        }

        // Lấy tất cả sổ tiết kiệm
        public static List<SoTietKiemDTO> GetAllSoTietKiem()
        {
            return SoTietKiemDAL.GetAllSoTietKiem();
        }

        // Search Ho Ten Khach Hang
        public static List<SoTietKiemDTO> SearchHoTenKhachHang(string hoTen)
        {
            return SoTietKiemDAL.SearchSoTietKiemByHoTen(hoTen);
        }

        // Thêm sổ tiết kiệm
        public static bool AddSoTietKiem(SoTietKiemDTO soTietKiem)
        {
            return SoTietKiemDAL.AddSoTietKiem(soTietKiem);
        }

        // Cập nhật sổ tiết kiệm
        public static bool UpdateSoTietKiem(SoTietKiemDTO soTietKiem)
        {
            return SoTietKiemDAL.UpdateSoTietKiem(soTietKiem);
        }

        // Xóa sổ tiết kiệm
        //public static bool DeleteSoTietKiem(int maSo)
        //{
        //    return SoTietKiemDAL.DeleteSoTietKiem(maSo);
        //}

        public static bool DeleteSoTietKiem(int maSo)
        {
            // BƯỚC 1: KIỂM TRA GIAO DỊCH CỦA SỔ TIẾT KIỆM
            if (GiaoDichTietKiemBLL.KiemTraGiaoDichCuaSoTietKiem(maSo)) // Hàm này sẽ được tạo
            {
                MessageBox.Show("Không thể xóa sổ tiết kiệm này vì đã có các giao dịch phát sinh. Vui lòng tất toán và đóng sổ nếu cần.", "Lỗi xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false; // Chặn xóa
            }

            // Nếu không có giao dịch, tiến hành xóa sổ
            return SoTietKiemDAL.DeleteSoTietKiem(maSo);
        }

        // Lấy MaSo lớn nhất trong bảng SoTietKiem
        public static int GetMaxMaSo()
        {
            return SoTietKiemDAL.GetMaxMaSo() ?? 0;
        }

        // Lấy danh sách sổ tiết kiệm theo mã khách hàng
        public static List<SoTietKiemDTO> GetSoTietKiemByMaKH(int maKH)
        {
            return SoTietKiemDAL.GetSoTietKiemByMaKH(maKH);
        }

        // GetSoTietKiemByCCCDKH
        public static List<SoTietKiemDTO> GetSoTietKiemByCCCDKH(string cccd)
        {
            return SoTietKiemDAL.GetSoTietKiemByCCCDKH(cccd);
        }

        // Fix for CS0127: Removed the 'return' keyword since the method returns void.
        public static void CapNhatTrangThaiSoTietKiem(int maKH, string trangThai)
        {
            SoTietKiemDAL.CapNhatTrangThaiSoTietKiem(maKH, trangThai);
        }

        // Update trạng thái sổ tiết kiệm
        public static void UpdateTrangThaiSoTietKiem(int maSo, string trangThai)
        {
            SoTietKiemDAL.UpdateTrangThaiSoTietKiem(maSo, trangThai);
        }

        //CapNhatSoTienGoc khi gửi
        public static void CapNhatSoTienGoc(int maSo, decimal soTienGoc)
        {
            SoTietKiemDAL.CapNhatSoTienGoc(maSo, soTienGoc);
        }

        // CapNhatSoTienGoc khi rút
        public static void CapNhatSoTienGocKhiRut(int maSo, decimal soTienGoc)
        {
            SoTietKiemDAL.CapNhatSoTienGocKhiRut(maSo, soTienGoc);
        }

        // CapNhatSoDaoHan
        public static List<int> CapNhatSoDaoHan()
        {
            return SoTietKiemDAL.CapNhatSoDaoHan();
        }

        // GetSoTietKiemChiTiet
        public static SoTietKiemDTO GetSoTietKiemChiTiet(int maSo)
        {
            return SoTietKiemDAL.GetSoTietKiemChiTiet(maSo);
        }

        // CapNhatNgayMo
        public static void CapNhatNgayMo(int maSo)
        {
            SoTietKiemDAL.CapNhatNgayMo(maSo);
        }

        // GetSoTietKiemInfoForCalculation
        public static SoTietKiemDTO GetSoTietKiemInfoForCalculation(int maSo)
        {
            return SoTietKiemDAL.GetSoTietKiemInfoForCalculation(maSo);
        }

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

        // CountSoTietKiemByMaKH
        public static bool KiemTraSoTietKiemCuaKhachHang(int maKH)
        {
            return SoTietKiemDAL.CountSoTietKiemByMaKH(maKH) > 0;
        }

        // CountSoTietKiemByMaLoai
        public static bool KiemTraSoTietKiemByMaLoai(int maLoai)
        {
            return SoTietKiemDAL.CountSoTietKiemByMaLoai(maLoai) > 0;
        }
    }
}
