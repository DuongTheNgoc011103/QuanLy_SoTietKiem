using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy_SoTietKiem.DTO
{
    public class TongHopTienGuiReportDTO
    {
        public DateTime NgayGiaoDich { get; set; } // Ngày của giao dịch (nếu muốn nhóm theo ngày)
        public string LoaiGiaoDich { get; set; } // Loại giao dịch (Mở sổ, Gửi tiền)
        public decimal TongTien { get; set; } // Tổng số tiền của loại giao dịch đó
        public int SoLuongGiaoDich { get; set; } // Số lượng giao dịch của loại đó
    }
}