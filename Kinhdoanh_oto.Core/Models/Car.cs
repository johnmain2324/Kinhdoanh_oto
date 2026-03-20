using System;

namespace Kinhdoanh_oto.Models
{
    public class Car
    {
        public int Id { get; set; }

        public string? TenXe { get; set; }

        public string? HangXe { get; set; }

        public string? DongXe { get; set; }

        public int? NamSanXuat { get; set; }

        public decimal? GiaNiemYet { get; set; }

        public decimal? GiaKhuyenMai { get; set; }

        public string? NhienLieu { get; set; }

        public string? HopSo { get; set; }

        public int? CongSuat { get; set; }

        public string? MauNgoaiThat { get; set; }

        public int? SoCho { get; set; }

        public string? Thumbnail { get; set; }

        public string? Noidung { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;
    }
}