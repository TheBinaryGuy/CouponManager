using System.Threading;
using System.Threading.Tasks;
using CouponManager.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CouponManager.Api.Controllers
{
    [Produces("application/json")]
    [Route("cm/api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TestController : ControllerBase
    {
        private readonly WaitForSeconds _waitForSeconds;
        private readonly IMailSender _mailSender;

        private readonly CancellationTokenSource _tokenSource;
        private readonly CancellationToken _cancellationToken;

        public TestController(WaitForSeconds waitForSeconds, IMailSender mailSender)
        {
            _waitForSeconds = waitForSeconds;
            _mailSender = mailSender;

            _tokenSource = new CancellationTokenSource();
            _cancellationToken = _tokenSource.Token;
        }

        [HttpGet]
        public async Task<IActionResult> WaitSeconds()
        {
            await _waitForSeconds.WaitSecondsAsync(2000, _cancellationToken);
            return new JsonResult(new { Result = "Done" });
        }

        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail(SendEmailViewModel model)
        {
            var cancellationToken = new CancellationToken();
            var responseStatus = await _mailSender.SendEmailAsync(model, cancellationToken);

            // To cancel the task and clean up threads
            //if (!_tokenSource.IsCancellationRequested)
            //{
            //    _tokenSource.Cancel();
            //}

            Response.StatusCode = (int)responseStatus;
            return new JsonResult(new { Result = responseStatus.ToString() });
        }
    }
}