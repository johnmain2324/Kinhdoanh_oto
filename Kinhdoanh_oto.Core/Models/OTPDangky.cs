using System;

namespace Kinhdoanh_oto.Models
{
    public class OTPDangky
    {
        public int Id { get; set; }
        public string Sodienthoai { get; set; } = string.Empty;
        public string MaOTP { get; set; } = string.Empty;
        public DateTime ThoigianTao { get; set; }
        public DateTime HetHan { get; set; }
        public bool Trangthai { get; set; }
    }
}