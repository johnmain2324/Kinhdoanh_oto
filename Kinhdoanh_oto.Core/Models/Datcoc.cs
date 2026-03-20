using System;

namespace Kinhdoanh_oto.Models
{
    public class Datcoc
    {
        public int Id { get; set; }

        public int KhachhangId { get; set; }

        public int XeId { get; set; }

        public decimal Sotien { get; set; }

        public DateTime Ngaydat { get; set; }

        public Khachhang? Khachhang { get; set; }

        public Car? Xe { get; set; }
    }
}