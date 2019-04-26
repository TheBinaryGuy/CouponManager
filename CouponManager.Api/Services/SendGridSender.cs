using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

public class SendGridSender : IMailSender
{
    private IConfiguration _configuration;

    public SendGridSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<HttpStatusCode> SendEmailAsync(SendEmailViewModel model)
    {
        var client = new SendGridClient(_configuration["SENDGRID:API_KEY"]);
        var msg = new SendGridMessage()
        {
            From = new EmailAddress(model.FromEmail, model.Name),
            Subject = model.Subject,
            HtmlContent = model.HtmlBody
        };
        model.ToEmails.ToList().ForEach(e => msg.AddTo(new EmailAddress(e)));
        var response = await client.SendEmailAsync(msg);
        return response.StatusCode;
    }
}