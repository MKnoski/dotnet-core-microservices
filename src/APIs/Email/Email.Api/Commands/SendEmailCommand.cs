using Email.Domain.Models;
using MediatR;

namespace Email.Api.Commands
{
    public class SendEmailCommand : IRequest<bool>
    {
        public SendEmailCommand(SendEmailModel sendEmail)
        {
            SendEmail = sendEmail;
        }

        public SendEmailModel SendEmail { get; }
    }
}