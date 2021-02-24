namespace Email.Domain.Models
{
    public class SendEmailModel
    {
        public From From { get; set; }
        
        public string To { get; set; }
        
        public string Subject { get; set; }
        
        public string PlainTextContent { get; set; }
        
        public string HtmlContent { get; set; }
    }
    
    public class From
    {
        public string Email { get; set; }
        
        public string Name { get; set; }
    }
}