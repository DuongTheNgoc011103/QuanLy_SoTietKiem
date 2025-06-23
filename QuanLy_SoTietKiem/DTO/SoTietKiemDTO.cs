using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy_SoTietKiem.DTO
{
    public class SoTietKiemDTO
    {
        public int MaSo { get; set; }
        public int MaKH { get; set; }
        public string HoTen { get; set; } // Thêm thuộc tính tên khách hàng
        public int MaLoai { get; set; }
        public string TenLoai { get; set; } // Thêm thuộc tính tên loại sổ tiết kiệm
        public DateTime NgayMo { get; set; }
        public int? KyHan { get; set; }
        public decimal SoTienGoc { get; set; }
        public string TrangThai { get; set; }
        public float LaiSuatCoBan { get; set; }
        public DateTime? NgayDaoHan { get; set; }

        // Thêm các thuộc tính để chứa kết quả tính toán
        public decimal LaiDuKien { get; set; }
        public decimal TongSoTienCuoiKy { get; set; }

        // Constructor đầy đủ
        public SoTietKiemDTO(int maSo, int maKH, string tenKH, int maLoai, string tenLoai, DateTime ngayMo, int? kyHan, decimal soTienGoc, string trangThai, DateTime ngayDaoHan)
        {
            MaSo = maSo;
            MaKH = maKH;
            HoTen = tenKH; // Gán tên khách hàng
            MaLoai = maLoai;
            TenLoai = tenLoai; // Gán tên loại sổ tiết kiệm
            NgayMo = ngayMo;
            KyHan = kyHan;
            SoTienGoc = soTienGoc;
            TrangThai = trangThai;
            NgayDaoHan = ngayDaoHan; // Gán ngày đáo hạn

        }

        // Constructor rút gọn
        public SoTietKiemDTO(int maSo, int maKH, string tenKH, int maLoai, decimal soTienGoc)
        {
            MaSo = maSo;
            MaKH = maKH;
            HoTen = tenKH; // Gán tên khách hàng
            MaLoai = maLoai;
            SoTienGoc = soTienGoc;
        }

        public SoTietKiemDTO(int maSo, int maKH, DateTime? ngaMo, int maLoai, decimal soTienGoc, float laiSuatCB, int kyHan, string trangThai)
        {
            MaSo = maSo;
            SoTienGoc = soTienGoc;
            NgayMo = ngaMo ?? DateTime.Now; // Gán ngày mở, nếu null thì mặc định là ngày hiện tại
            LaiSuatCoBan = laiSuatCB;
            KyHan = kyHan;
            TrangThai = trangThai;
        }

        // Constructor mặc định
        public SoTietKiemDTO() { }
    }
}
