using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace Kinhdoanh_oto.Core.ViewModels
    {
    public class DatcocViewModel
    {
        public int CarId { get; set; }

        public string? TenXe { get; set; }
        public decimal GiaXe { get; set; }
        public string? Thumbnail { get; set; }
        public decimal TienDatCoc { get; set; }

        [Required]
        public string? Hoten { get; set; }
        public string? Sodienthoai { get; set; }
        public string? Email { get; set; }
        public string? CCCD { get; set; }
        public string? Diachi { get; set; }
    }
}

