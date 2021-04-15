namespace Weather.Domain.Models
{
    public class SendEmailModel
    {
        public string FromEmail { get; set; }

        public string FromName { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string To { get; set; }

        public string Subject { get; set; }

        public string PlainTextContent { get; set; }

        public string HtmlContent { get; set; }
    }
}
