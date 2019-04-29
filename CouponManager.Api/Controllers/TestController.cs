using System.Net;
using System.Threading;
using System.Threading.Tasks;
using CouponManager.Api.Data;
using CouponManager.Api.Models;
using CouponManager.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CouponManager.Api.Controllers
{
    [Produces("application/json")]
    [Route("cm/api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly WaitForSeconds _waitForSeconds;
        private readonly IMailSender _mailSender;

        private readonly AppDbContext _context;

        private readonly CancellationTokenSource _tokenSource;
        private readonly CancellationToken _cancellationToken;

        public TestController(WaitForSeconds waitForSeconds, IMailSender mailSender, AppDbContext context)
        {
            _waitForSeconds = waitForSeconds;
            _mailSender = mailSender;
            _context = context;

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

        [HttpPost("AddCoupon")]
        public async Task<IActionResult> AddCoupon(Coupon coupon)
        {
            await _context.AddAsync(coupon, _cancellationToken);
            var result = await _context.SaveChangesAsync(_cancellationToken);
            if (result == 1) return NoContent();
            else
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = "Something bad happened." });
            }
        }
    }
}