using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy_SoTietKiem.Utils
{
    public class TienHelper
    {
        private static readonly string[] ChuSo = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
        private static readonly string[] DonViNho = { "", "nghìn", "triệu", "tỷ" };

        public static string DocTienBangChu(decimal soTien)
        {
            if (soTien == 0) return "Không đồng";

            // Handle negative numbers (optional, but good practice)
            string sign = "";
            if (soTien < 0)
            {
                sign = "âm ";
                soTien = Math.Abs(soTien);
            }

            // Only process the integer part for now, as Vietnamese money reading typically doesn't include cents/decimals in words
            string s = ((long)soTien).ToString();
            string ketQua = "";
            int donViIndex = 0; // Represents nghìn, triệu, tỷ

            // Pad the number with leading zeros to make its length a multiple of 3
            // This simplifies processing groups of three digits
            int remainder = s.Length % 3;
            if (remainder != 0)
            {
                s = new string('0', 3 - remainder) + s;
            }

            // Process the number in groups of 3 digits from right to left
            for (int i = s.Length - 1; i >= 0; i -= 3)
            {
                if (i - 2 < 0) break; // Should not happen with padding, but a safeguard

                string group = s.Substring(i - 2, 3); // Get a 3-digit group (e.g., "123", "045")
                string chuNhom = DocNhom3So(group, donViIndex > 0); // Pass a flag if it's not the first group (for "linh" considerations)

                if (!string.IsNullOrEmpty(chuNhom) || (donViIndex == 0 && string.IsNullOrEmpty(chuNhom) && s.Length == 3))
                {
                    // Add the unit (nghìn, triệu, tỷ), but only if the group is not all zeros
                    // and it's not the very first group if it's empty (e.g., "000" at the beginning of "000123")
                    if (!string.IsNullOrEmpty(chuNhom))
                    {
                        ketQua = chuNhom + (donViIndex > 0 ? " " + DonViNho[donViIndex] : "") + (string.IsNullOrEmpty(ketQua) ? "" : " " + ketQua);
                    }
                    else if (donViIndex > 0 && long.Parse(group) > 0) // Handle cases like "một tỷ không trăm linh năm"
                    {
                        ketQua = DonViNho[donViIndex] + " " + ketQua;
                    }
                }
                donViIndex++;
            }

            ketQua = ketQua.Trim();
            if (string.IsNullOrEmpty(ketQua)) return "Không đồng"; // Should not happen with soTien == 0 check

            // Capitalize the first letter and append "đồng"
            ketQua = char.ToUpper(ketQua[0]) + ketQua.Substring(1);
            return sign + ketQua + " đồng";
        }

        /// <summary>
        /// Reads a 3-digit number group (e.g., "123", "050", "007").
        /// </summary>
        /// <param name="group">The 3-digit string group.</param>
        /// <param name="isTrailingGroup">True if this is not the last (rightmost) group of 3 digits in the full number.</param>
        /// <returns>The Vietnamese word representation of the 3-digit group.</returns>
        private static string DocNhom3So(string group, bool isTrailingGroup)
        {
            int tram = int.Parse(group[0].ToString());
            int chuc = int.Parse(group[1].ToString());
            int donVi = int.Parse(group[2].ToString());

            string ketQua = "";

            // If the whole group is zero, return empty unless it's the very first group for zero value (handled by caller)
            if (tram == 0 && chuc == 0 && donVi == 0)
            {
                return "";
            }

            // Read hundreds
            if (tram != 0)
            {
                ketQua += ChuSo[tram] + " trăm";
            }

            // Read tens and units
            if (chuc == 0)
            {
                if (donVi != 0)
                {
                    // "lẻ" for 0 in tens place when there's a unit (e.g., "một trăm lẻ năm")
                    // But only if there was a hundred or it's not the very beginning of the number (e.g. "005" shouldn't be "lẻ năm")
                    if (tram != 0 || isTrailingGroup)
                    {
                        ketQua += " lẻ " + ChuSo[donVi];
                    }
                    else
                    {
                        ketQua += ChuSo[donVi];
                    }
                }
            }
            else if (chuc == 1)
            {
                ketQua += (tram != 0 ? " " : "") + "mười"; // Add space if there's a hundred
                if (donVi != 0)
                {
                    ketQua += " " + DocDonVi(donVi, chuc);
                }
            }
            else // chuc is 2-9
            {
                ketQua += (tram != 0 ? " " : "") + ChuSo[chuc] + " mươi"; // Add space if there's a hundred
                if (donVi != 0)
                {
                    ketQua += " " + DocDonVi(donVi, chuc);
                }
            }

            return ketQua.Trim();
        }

        /// <summary>
        /// Reads a single unit digit, handling special cases like "mốt" and "lăm".
        /// </summary>
        /// <param name="donVi">The unit digit (0-9).</param>
        /// <param name="chuc">The tens digit (used for special cases).</param>
        /// <returns>The Vietnamese word representation of the unit digit.</returns>
        private static string DocDonVi(int donVi, int chuc = -1)
        {
            if (donVi == 1)
            {
                if (chuc == 0) return "một"; // "lẻ một", "không trăm linh một"
                if (chuc >= 2) return "mốt"; // "hai mươi mốt", "ba mươi mốt"
            }
            if (donVi == 5 && chuc >= 1) // "mười lăm", "hai mươi lăm"
                return "lăm";
            if (donVi == 0 && chuc != -1) return ""; // Don't say "không" if it's "mươi không" (e.g., "hai mươi")
            return ChuSo[donVi];
        }
    }
}