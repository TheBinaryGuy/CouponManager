using System.Net;
using System.Threading;
using System.Threading.Tasks;

public interface IMailSender
{
    Task<HttpStatusCode> SendEmailAsync(SendEmailViewModel model, CancellationToken cancellationToken = default);
}