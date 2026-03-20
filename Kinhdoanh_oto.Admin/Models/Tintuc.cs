using System;

namespace Kinhdoanh_oto.Admin.Models
{
    public class Tintuc
    {
        public int Id { get; set; }

        public string? Tieude { get; set; }

        public string? Noidung { get; set; }

        public DateTime Ngaydang { get; set; }

        public bool Trangthai { get; set; }
    }
}