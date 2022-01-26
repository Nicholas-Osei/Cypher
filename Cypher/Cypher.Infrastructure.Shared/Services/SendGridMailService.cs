using Cypher.Application.DTOs.Mail;
using Cypher.Application.Interfaces.Shared;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace Cypher.Infrastructure.Shared.Services
{
    public class SendGridMailService : IMailService
    {
        //public MailSettings _mailSettings { get; set; }
        public ILogger<SendGridMailService> _logger { get; set; }

        public SendGridMailService(IOptions<MailSettings> mailSettings, ILogger<SendGridMailService> logger)
        {
            //_mailSettings = mailSettings.Value;
            _logger = logger;
        }

        public async Task SendAsync(MailRequest request)
        {
            try
            {
                var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
                var client = new SendGridClient(apiKey);

                //var email = new MimeMessage();
                //email.Sender = MailboxAddress.Parse(request.From ?? _mailSettings.Mail);
                //email.To.Add(MailboxAddress.Parse(request.To));
                //email.Subject = request.Subject;
                //var builder = new BodyBuilder();
                //builder.HtmlBody = request.Body;
                //email.Body = builder.ToMessageBody();


                var from = new EmailAddress(request.From ?? "devtester.devin@gmail.com", "Cypher Admin");
                var subject = request.Subject;
                var to = new EmailAddress(request.To);
                var plainTextContent = "";
                var htmlContent = request.Body;
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            }
            catch (System.Exception ex)
            {

                _logger.LogError(ex.Message, ex);
            }
        }
    }
}