using System;
using System.Threading.Tasks;
using MyHero.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;

namespace HeroApp.Controllers
{
    public class NotificationController : Controller
    {
        private readonly NotificationSettings notificationSettings;

        public NotificationController(IOptions<NotificationSettings> notificationSettingsAccessor)
        {
            notificationSettings = notificationSettingsAccessor.Value;
        }

        public Task<bool> SendEmailAsync()
        {
            SendEmail("You have received a visit request", "Hello Colton, you are being requested to visit this kid...bla bla bla", "Michael Dantas", "mhknutson@hotmail.com");
            return Task.FromResult(true);
        }

        private void SendEmail(string subject, string message, string toName, string toEmail)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(notificationSettings.EmailNotificationSettings.Name, notificationSettings.EmailNotificationSettings.EmailSender));
            mimeMessage.To.Add(new MailboxAddress(toName, toEmail));
            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart("plain")
            {
                Text = message
            };

            try
            {
                using (var client = new SmtpClient())
                {
                    client.Connect(notificationSettings.EmailNotificationSettings.SmtpClient, notificationSettings.EmailNotificationSettings.Port, false);

                    client.Authenticate(notificationSettings.EmailNotificationSettings.EmailSender, notificationSettings.EmailNotificationSettings.EmailPassword);

                    client.Send(mimeMessage);
                    client.Disconnect(true);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}