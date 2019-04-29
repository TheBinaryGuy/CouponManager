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
            builder.ApplyConfiguration(new EntityDbConfigurationCompany());
            builder.Entity<Company>(c => c.HasIndex(co => co.UserName).IsUnique());
            builder.ApplyConfiguration(new EntityDbConfigurationDomain());
            builder.Entity<Coupon>(b =>
            {
                b.HasIndex(c => new { c.CompanyId, c.DomainId, c.CategoryId, c.Code }).IsUnique();
            });
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
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
