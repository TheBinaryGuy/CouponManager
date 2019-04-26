using Microsoft.Extensions.Configuration;
using SocketLabs.InjectionApi;
using SocketLabs.InjectionApi.Message;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace CouponManager.Api.Services
{
    public class SocketLabSender : IMailSender
    {
        private readonly IConfiguration _configuration;

        public SocketLabSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<HttpStatusCode> SendEmailAsync(SendEmailViewModel model, CancellationToken cancellationToken = default)
        {
            int serverId;
            var result = int.TryParse(_configuration["serverId"], out serverId);
            if (!result)
            {
                return HttpStatusCode.BadRequest;
            }

            var client = new SocketLabsClient(serverId, _configuration["injectionApiKey"]);

            var message = new BulkMessage
            {
                HtmlBody = model.HtmlBody,
                Subject = model.Subject
            };
            message.From.Email = model.FromEmail;

            var _emails = model.ToEmails.ToArray();
            for (int i = 0; i < _emails.Count(); i++)
            {
                message.To.Add(_emails[i]).MergeData.Add($"Name{i}", $"Recipient {i + 1}");
            }

            var response = await client.SendAsync(message);
            if (response.Result == SendResult.Success)
            {
                return HttpStatusCode.OK;
            }
            else
            {
                return HttpStatusCode.BadRequest;
            }
        }
    }
}
