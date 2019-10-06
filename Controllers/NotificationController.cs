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
        public Task<bool> SendRequestReceivedEmailAsync(string requestedBy, string description, string heroName, string heroEmail)
        {
            SendEmail($"Your received a visit request by {requestedBy}", BuildRequestReceivedMessage(requestedBy, description, heroName), heroName, heroEmail);
            return Task.FromResult(true);
        }

        /// <summary>
        /// Send an email to Requester informing a Hero has accepted its visit request. 
        /// </summary>
        /// <returns>True, if email was sent. False, otherwise.</returns>
        public Task<bool> SendRequestAcceptedEmailAsync(string acceptedBy, string heroEmail, string requesterName, string requesterEmail)
        {
            SendEmail($"Your visit request was accepted by {acceptedBy}", BuildRequestAcceptedMessage(acceptedBy, heroEmail, requesterName), requesterName, requesterEmail);
            return Task.FromResult(true);
        }

        private MimeEntity BuildRequestReceivedMessage(string requestedBy, string description, string heroName)
        {
            var builder = new BodyBuilder();

            builder.HtmlBody = string.Format(@"<p>Hey {0},<br>
                                <p>Your assistance has been requested by {1}.<br>
                                <p>{1} says: '-{2}'<br>
                                <p>Don't forget to accept or decline this mission on your profile<br>", heroName, requestedBy, description);

            return builder.ToMessageBody();
        }

        private MimeEntity BuildRequestAcceptedMessage(string acceptedBy, string heroEmail, string requesterName)
        {
            var builder = new BodyBuilder();

            builder.HtmlBody = string.Format(@"<p>Hey {0},<br>
                                <p>Your request has been accepted by {1}.<br>
                                <p>You can contact your hero at:<br>
                                <p>{2}<br>", requesterName, acceptedBy, heroEmail);

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