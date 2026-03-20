using MailKit.Net.Smtp;
using MimeKit;

namespace Kinhdoanh_oto.Services
{
    public class EmailService
    {
        public void SendOTP(string email, string otp)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("Kinh Doanh Oto", "yourgmail@gmail.com"));
            message.To.Add(new MailboxAddress("", email));

            message.Subject = "Ma OTP xac nhan";

            message.Body = new TextPart("plain")
            {
                Text = $"Ma OTP cua ban la: {otp}. Ma co hieu luc 5 phut."
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);

                client.Authenticate("tinpham2324@gmail.com", "pxjnfcuyalmkpnsr");

                client.Send(message);

                client.Disconnect(true);
            }
        }
    }
}