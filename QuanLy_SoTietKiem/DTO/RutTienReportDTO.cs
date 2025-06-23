using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy_SoTietKiem.DTO
{
    public class RutTienReportDTO
    {
        // Thông tin Giao dịch
        public int MaGD { get; set; }
        public DateTime NgayGD { get; set; }
        public string LoaiGiaoDich { get; set; } = "Rút tiền"; // Luôn là "Rút tiền"
        public string PhuongThucRut { get; set; } // "Rút một phần" hoặc "Rút toàn bộ"
        public string HoTen_NV { get; set; } // Tên nhân viên thực hiện giao dịch

        // Thông tin Khách hàng
        public string HoTen_KH { get; set; } // Tên khách hàng
        public string CMND_CCCD { get; set; }
        public string DiaChi_KH { get; set; }

        // Thông tin Sổ Tiết Kiệm
        public int MaSo { get; set; }
        public string TenLoai { get; set; } // Tên loại sổ tiết kiệm
        public DateTime NgayMo { get; set; }
        public int? KyHan { get; set; }
        public DateTime NgayDaoHan { get; set; } // Ngày đáo hạn dự kiến
        public decimal SoTienGoc { get; set; } // Hoặc là số tiền gốc tại thời điểm mở, hoặc gốc trước khi rút

        // Chi tiết số tiền
        public decimal SoTienYeuCauRut { get; set; } // Số tiền khách hàng muốn rút (nhập vào txtSoTienRut)
        public decimal LaiThucTe { get; set; } // txtLaiThucTe
        public decimal PhiPhatTruocHan { get; set; } // Nếu có và được tính
        public decimal TongTienThucNhan { get; set; } // txtTongTienNhan
        public string TongTienBangChu { get; set; } // lbl_SoTienBangChu (từ TongTienThucNhan)
        public string GhiChuGiaoDich { get; set; }
    }
}
