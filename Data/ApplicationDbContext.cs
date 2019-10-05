using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyHero.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<SampleDataEntity> SampleDataEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SampleDataEntity>().ToTable("SampleDataEntity");

            base.OnModelCreating(builder);
        }
    }
}
