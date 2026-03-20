using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinhdoanh_oto.Core.Models
{
    public class ServiceBooking
    {
        public int Id { get; set; }
        public string? Hoten { get; set; }
        public string? Sodienthoai { get; set; }
        public string? Dichvu { get; set; }
        public DateTime NgayHen { get; set; }
        public DateTime NgayTao { get; set; }

    }
}
