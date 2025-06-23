using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy_SoTietKiem.DTO
{
    public class LichSuThaoTacDTO
    {
        public int MaLS { get; set; }
        public int MaTaiKhoan { get; set; }
        public string ThaoTac { get; set; }
        public DateTime ThoiGian { get; set; }
        public string DoiTuong { get; set; }

        public LichSuThaoTacDTO(int maLichSu, int maTaiKhoan, string thaoTac, DateTime thoiGian, string doiTuong)
        {
            MaLS = maLichSu;
            MaTaiKhoan = maTaiKhoan;
            ThaoTac = thaoTac;
            ThoiGian = thoiGian;
            DoiTuong = doiTuong;
        }

        public LichSuThaoTacDTO() { }
    }
}
