namespace Kinhdoanh_oto.Models
{
    public class Baogia
    {
        public int Id { get; set; }

        public int KhachhangId { get; set; }

        public int XeId { get; set; }

        public decimal Giabaogia { get; set; }

        public string? Trangthai { get; set; }

        public Khachhang? Khachhang { get; set; }

        public Car? Xe { get; set; }
    }
}