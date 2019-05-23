using CouponManager.Api.Models;
using CouponManager.Api.ViewModels;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CouponManager.Api.Services
{
    public interface ICouponService
    {
        IQueryable<Coupon> GetAsync(int limit, int offset);
        Task<Coupon> GetAsync(string couponName, CancellationToken _cancellationToken = default);
        Task AddAsync(CouponViewModel model, string userId, CancellationToken _cancellationToken = default);
        Task PutAsync(CouponViewModel model, string userId, CancellationToken _cancellationToken = default);
        Task DeleteAsync(CouponDeleteViewModel model, string userId, CancellationToken _cancellationToken = default);
    }
}
