using System;

namespace Kinhdoanh_oto.Models
{
    public class Khachhang
    {
        public int Id { get; set; }

        public string? Hoten { get; set; }

        public string? Gioitinh { get; set; }

        public DateTime Ngaysinh { get; set; }

        public string? Sodienthoai { get; set; }

        public string? Email { get; set; }

        public string? CCCD { get; set; }

        public string? Diachi { get; set; }

        public string? Matkhau { get; set; }

        public DateTime Ngaytao { get; set; }
    }
}