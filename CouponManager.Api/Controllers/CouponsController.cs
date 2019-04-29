using CouponManager.Api.Data;
using CouponManager.Api.Models;
using CouponManager.Api.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CouponsController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly CancellationTokenSource _tokenSource;
        private readonly CancellationToken _cancellationToken;

        public CouponsController(AppDbContext context)
        {
            _context = context;

            _tokenSource = new CancellationTokenSource();
            _cancellationToken = _tokenSource.Token;
        }

        [HttpGet("{limit?}/{offset?}")]
        public async Task<IActionResult> GetAsync(int limit = 50, int offset = 0)
        {
            var coupons = await _context.Coupons.Skip(limit * offset).Take(limit).AsNoTracking().ToListAsync(_cancellationToken);
            return new JsonResult(coupons);
        }

        [HttpGet("{couponName}")]
        public async Task<IActionResult> GetAsync(string couponName)
        {
            var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.Name == couponName, _cancellationToken);
            return new JsonResult(coupon);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(CouponViewModel model)
        {
            var userId = User.Claims.Where(c => c.Type == "UserId").Select(c => c.Value).SingleOrDefault();
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == model.CategoryName);
            var domain = await _context.Domains.FirstOrDefaultAsync(c => c.Name == model.DomainName && c.UserId == userId);
            await _context.AddAsync(
                new Coupon
                {
                    Name = model.Name,
                    Code = model.Code,
                    CategoryId = category.Id,
                    DomainId = domain.Id,
                    UserId = userId
                }
            );
            await _context.SaveChangesAsync(_cancellationToken);
            return NoContent();
        }
    }
}
