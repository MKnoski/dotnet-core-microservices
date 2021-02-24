using System;
using System.Threading;
using System.Threading.Tasks;
using Email.Api.Commands;
using MediatR;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Email.Api.CommandHandlers
{
    public class SendEmailCommandHandler  : IRequestHandler<SendEmailCommand, bool>
    {
        private static readonly string ApiKey = Environment.GetEnvironmentVariable("SendGridApiKey");
        
        public async Task<bool> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var sendGridClient = new SendGridClient(ApiKey);

            var sendEmail = request.SendEmail;

            var from = new EmailAddress(sendEmail.From.Email, sendEmail.From.Name);
            var subject = sendEmail.Subject;
            var to = new EmailAddress(sendEmail.To);
            var plainTextContent = sendEmail.PlainTextContent;
            var htmlContent = sendEmail.HtmlContent;

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = await sendGridClient.SendEmailAsync(msg, cancellationToken);

            return response.IsSuccessStatusCode;
        }
    }
}