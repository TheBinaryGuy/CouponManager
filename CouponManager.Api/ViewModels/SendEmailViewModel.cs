using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class SendEmailViewModel
{
    [Required, EmailAddress]
    public string FromEmail { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Subject { get; set; }

    [Required]
    public string HtmlBody { get; set; }

    [Required]
    public IEnumerable<string> ToEmails { get; set; }
}