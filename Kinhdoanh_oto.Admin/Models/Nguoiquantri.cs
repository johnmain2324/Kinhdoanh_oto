using System;
using System.ComponentModel.DataAnnotations;

namespace Kinhdoanh_oto.Admin.Models
{
    public class Nguoiquantri
    {
        public int Id { get; set; }

        [Required]
        public string Hoten { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Matkhau { get; set; } = string.Empty;

        [Required]
        public string Mabaomat { get; set; } = string.Empty;

        [Required]
        public string Vaitro { get; set; } = string.Empty;

        public bool Trangthai { get; set; } = true;

        public int Solandangsai { get; set; } = 0;

        public DateTime? ThoigianKhoa { get; set; }

        public DateTime Ngaytao { get; set; } = DateTime.Now;
    }
}