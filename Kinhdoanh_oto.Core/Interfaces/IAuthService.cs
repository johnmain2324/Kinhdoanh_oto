using Kinhdoanh_oto.Models;

namespace Kinhdoanh_oto.Core.Interfaces
{
    public interface IAuthService
    {
        Khachhang? Login(string sodienthoai, string matkhau);

        bool Register(string hoten, string email, string sodienthoai);
    }
}