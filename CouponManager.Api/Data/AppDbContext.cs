using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CouponManager.Api.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CouponManager.Api.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new EntityDbConfigurationCompany());
            builder.Entity<Company>(c =>
            {
                c.HasIndex(co => co.UserName).IsUnique();
                c.HasIndex(co => co.Url).IsUnique();
            });

            builder.ApplyConfiguration(new EntityDbConfigurationDomain());
            builder.Entity<Domain>(b => b.HasIndex(d => d.Url).IsUnique());

            builder.Entity<Coupon>(b =>
            {
                b.HasIndex(c => new { c.CompanyId, c.DomainId, c.CategoryId, c.Code }).IsUnique();
            });
        }

        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Domain> Domains { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
    }

    public class EntityDbConfigurationCompany : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(e => e.Url)
                    .HasConversion(c => c.ToString(), c => new Uri(c));
        }
    }

    public class EntityDbConfigurationDomain : IEntityTypeConfiguration<Domain>
    {
        public void Configure(EntityTypeBuilder<Domain> builder)
        {
            builder.Property(e => e.Url)
                    .HasConversion(d => d.ToString(), d => new Uri(d));
        }
    }

    public class EntityDbConfigurationCoupon : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.HasAlternateKey("CompanyId", "Code");

        }
    }
}
