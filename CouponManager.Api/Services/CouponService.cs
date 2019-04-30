using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CouponManager.Api.Data;
using CouponManager.Api.Models;
using CouponManager.Api.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CouponManager.Api.Services
{
    public class CouponService : ICouponService
    {
        private readonly AppDbContext _context;

        public CouponService(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Coupon> GetAsync(int limit, int offset)
        {
            return _context.Coupons.Skip(limit * offset).Take(limit).AsNoTracking();
        }

        public async Task<Coupon> GetAsync(string couponName, CancellationToken _cancellationToken = default)
        {
            return await _context.Coupons.FirstOrDefaultAsync(c => c.Name == couponName, _cancellationToken);
        }

        public async Task AddAsync(CouponViewModel model, string userId, CancellationToken _cancellationToken = default)
        {
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
        }
    }
}
