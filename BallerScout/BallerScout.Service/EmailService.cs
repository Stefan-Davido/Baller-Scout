using BallerScout.Service.ServiceInterfaces;
using MailKit.Net.Smtp;
using MimeKit;
using System;

namespace BallerScout.Service
{
    public class MyEmailService : IMyEmailService
    {
        public void SendEmail(string email, string link)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Baller Scout", "ballerscoutmk@gmail.com"));
            message.To.Add(new MailboxAddress("Baller Scout Verification", email));
            message.Subject = $"Email in {DateTime.Now}";
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = string.Format($"<a href =\"{link}\">Verify Email</a>")
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("ballerscoutmk@gmail.com", "MyPassword");

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
