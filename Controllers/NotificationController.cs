using System;
using System.Threading.Tasks;
using MyHero.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using MimeKit.Utils;

namespace MyHero.Controllers
{
    public class NotificationController : Controller
    {
        private readonly NotificationSettings notificationSettings;

        public NotificationController(IOptions<NotificationSettings> notificationSettingsAccessor)
        {
            notificationSettings = notificationSettingsAccessor.Value;
        }

        /// <summary>
        /// Send an email to Hero informing a visit request was received.
        /// </summary>
        /// <returns>True, if email was sent. False, otherwise.</returns>
        public Task<bool> SendRequestReceivedEmailAsync()
        {
            var name = "testing";
            var email = "danieldnds@gmail.com";

            SendEmail($"Your received a visit request by {name}", BuildRequestReceivedMessage(), name, email);
            return Task.FromResult(true);
        }

        /// <summary>
        /// Send an email to Requester informing a Hero has accepted its visit request. 
        /// </summary>
        /// <returns>True, if email was sent. False, otherwise.</returns>
        public Task<bool> SendRequestAcceptedEmailAsync()
        {
            var name = "testing";
            var email = "danieldnds@gmail.com";

            SendEmail($"Your visit request was accepted by {name}", BuildRequestAcceptedMessage(), name, email);
            return Task.FromResult(true);
        }

        private MimeEntity BuildRequestReceivedMessage()
        {
            var builder = new BodyBuilder();

            var image = builder.LinkedResources.Add(@"C:\Projects\MyHero\TempFiles\hero1.jpg");
            image.ContentId = MimeUtils.GenerateMessageId();

            builder.HtmlBody = string.Format(@"<p>Hey Alice,<br>
                                <p>What are you up to this weekend? Monica is throwing one of her parties on
                                Saturday and I was hoping you could make it.<br>
                                <p>Will you be my +1?<br>
                                <p>-- Joey<br>
                                <center><img src=""cid:{0}""></center>", image.ContentId);

            return builder.ToMessageBody();
        }

        private MimeEntity BuildRequestAcceptedMessage()
        {
            var builder = new BodyBuilder();

            var image = builder.LinkedResources.Add(@"C:\Projects\MyHero\TempFiles\hero1.jpg");
            image.ContentId = MimeUtils.GenerateMessageId();

            builder.HtmlBody = string.Format(@"<p>Hey Alice,<br>
                                <p>What are you up to this weekend? Monica is throwing one of her parties on
                                Saturday and I was hoping you could make it.<br>
                                <p>Will you be my +1?<br>
                                <p>-- Joey<br>
                                <center><img src=""cid:{0}""></center>", image.ContentId);

            return builder.ToMessageBody();
        }

        private void SendEmail(string subject, MimeEntity mimeEntity, string toName, string toEmail)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(notificationSettings.EmailNotificationSettings.Name, notificationSettings.EmailNotificationSettings.EmailSender));
            mimeMessage.To.Add(new MailboxAddress(toName, toEmail));
            mimeMessage.Subject = subject;
            mimeMessage.Body = mimeEntity;

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