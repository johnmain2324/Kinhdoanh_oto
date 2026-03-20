namespace Kinhdoanh_oto.Admin.Models
{
    public class Khachhang
    {
        public int Id { get; set; }

        public string? Hoten { get; set; }

        public string? Sodienthoai { get; set; }

        public string? Email { get; set; }

        public string? Khuvuc { get; set; }

        public DateTime Ngaytao { get; set; }

        public string? Matkhau { get; set; }

        public bool Trangthai { get; set; }

        public int Solandangsai { get; set; }

        public DateTime? ThoigianKhoa { get; set; }

        public string? Gioitinh { get; set; }

        public DateTime? Ngaysinh { get; set; }

        public string? CCCD { get; set; }

        public string? Diachi { get; set; }

    }
}