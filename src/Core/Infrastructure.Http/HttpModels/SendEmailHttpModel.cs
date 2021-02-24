namespace Infrastructure.Http.HttpModels
{
    public class SendEmailHttpModel
    {
        public string FromEmail { get; set; }

        public string FromName { get; set; }
        
        public string To { get; set; }
        
        public string Subject { get; set; }
        
        public string PlainTextContent { get; set; }
        
        public string HtmlContent { get; set; }
    }
}