using System.ComponentModel.DataAnnotations;

namespace Kinhdoanh_oto.Admin.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        public string? TenXe { get; set; }

        public string? HangXe { get; set; }

        public string? DongXe { get; set; }

        public int NamSanXuat { get; set; }

        public string? TinhTrang { get; set; }

        public decimal GiaNiemYet { get; set; }

        public decimal? GiaKhuyenMai { get; set; }

        public string? SoKhung { get; set; }

        public string? SoMay { get; set; }

        public string? BodyStyle { get; set; }

        public string? MauNgoaiThat { get; set; }

        public int SoCua { get; set; }

        public string? DenXe { get; set; }

        public string? MamXe { get; set; }

        public string? NhienLieu { get; set; }

        public string? DungTich { get; set; }

        public string? HopSo { get; set; }

        public string? DanDong { get; set; }

        public int CongSuat { get; set; }

        public int MoMenXoan { get; set; }

        public string? TieuHaoNhienLieu { get; set; }

        public string? MauNoiThat { get; set; }

        public int SoCho { get; set; }

        public string? ChatLieuGhe { get; set; }

        public string? AmThanh { get; set; }

        public int SoTuiKhi { get; set; }

        public string? Camera { get; set; }

        public string? Thumbnail { get; set; }

        public string? MoTa { get; set; }
        public string? Noidung { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;
    }
}