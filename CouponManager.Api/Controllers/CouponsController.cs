using CouponManager.Api.Data;
using CouponManager.Api.Services;
using CouponManager.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CouponManager.Api.Controllers
{
    [Produces("application/json")]
    [Route("cm/api/[controller]")]
    [ApiController]
    [Authorize]
    public class CouponsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ICouponService _couponService;
        private readonly CancellationTokenSource _tokenSource;
        private readonly CancellationToken _cancellationToken;

        public CouponsController(AppDbContext context, ICouponService couponService)
        {
            _context = context;
            _couponService = couponService;

            _tokenSource = new CancellationTokenSource();
            _cancellationToken = _tokenSource.Token;
        }

        [HttpGet("{limit?}/{offset?}")]
        public async Task<IActionResult> GetAsync(int limit = 50, int offset = 0)
        {
            return new JsonResult(await _couponService.GetAsync(limit, offset).ToListAsync(_cancellationToken));
        }

        [HttpGet("{couponName}")]
        public async Task<IActionResult> GetAsync(string couponName)
        {
            return new JsonResult(await _couponService.GetAsync(couponName, _cancellationToken));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(CouponViewModel model)
        {
            var userId = User.Claims.Where(c => c.Type == "UserId").Select(c => c.Value).SingleOrDefault();
            await _couponService.AddAsync(model, userId, _cancellationToken);
            return NoContent();
        }
    }
}
