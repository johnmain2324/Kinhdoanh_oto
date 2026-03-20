using Kinhdoanh_oto.Data;
using Kinhdoanh_oto.Models;
using Kinhdoanh_oto.Core.Interfaces;

namespace Kinhdoanh_oto.Core.Service
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Khachhang? Login(string sodienthoai, string matkhau)
        {
            var kh = _context.Khachhang
                .FirstOrDefault(x => x.Sodienthoai == sodienthoai);

            if (kh == null)
                return null;

            if (!BCrypt.Net.BCrypt.Verify(matkhau, kh.Matkhau))
                return null;

            return kh;
        }
        public bool Register(string hoten, string email, string sodienthoai)
        {
            var exist = _context.Khachhang
                .FirstOrDefault(x => x.Sodienthoai == sodienthoai);

            if (exist != null)
                return false;

            var kh = new Khachhang
            {
                Hoten = hoten,
                Email = email,
                Sodienthoai = sodienthoai,
                Ngaytao = DateTime.Now
            };

            _context.Khachhang.Add(kh);
            _context.SaveChanges();

            return true;
        }
    }
}