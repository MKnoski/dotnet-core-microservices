using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Email.Api.Commands;
using Email.Domain.Models;
using MediatR;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Email.Api.CommandHandlers
{
    public class SendEmailCommandHandler  : IRequestHandler<SendEmailCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public SendEmailCommandHandler(IMapper mapper, IConfiguration config)
        {
            _mapper = mapper;
            _config = config;
        }

        public async Task<bool> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var sendGridClient = new SendGridClient(_config["SendGridApiKey"]);

            var sendEmail = _mapper.Map<SendEmailModel>(request);

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