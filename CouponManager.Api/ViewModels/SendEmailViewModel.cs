using System.Collections.Generic;

public class SendEmailViewModel
{
    public string FromEmail { get; set; }
    public string Name { get; set; }
    public string Subject { get; set; }
    public string HtmlBody { get; set; }
    public IEnumerable<string> ToEmails { get; set; }
}