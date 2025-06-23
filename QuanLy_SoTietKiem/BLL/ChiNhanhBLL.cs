using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QuanLy_SoTietKiem.DAL;
using QuanLy_SoTietKiem.DTO;

namespace QuanLy_SoTietKiem.BLL
{
    public class ChiNhanhBLL
    {
        public static List<ChiNhanhDTO> GetAllChiNhanh()
        {
            return ChiNhanhDAL.GetAllChiNhanh();
        }

        // AddChiNhanh
        public static bool AddChiNhanh(ChiNhanhDTO cn)
        {
            return ChiNhanhDAL.AddChiNhanh(cn);
        }

        // EditChiNhanh
        public static bool EditChiNhanh(ChiNhanhDTO cn)
        {
            return ChiNhanhDAL.Update(cn);
        }

        // DeleteChiNhanh
        public static bool DeleteChiNhanh(int maCN)
        {
            return ChiNhanhDAL.DeleteChiNhanh(maCN);
        }

        // SearchChiNhanh
        public static List<ChiNhanhDTO> SearchChiNhanh(string keyword)
        {
            return ChiNhanhDAL.SearchChiNhanh(keyword);
        }
    }
}
