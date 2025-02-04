﻿using System;
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

        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<SampleDataEntity> SampleDataEntity { get; set; }
        public DbSet<Photos> Photos { get; set; }
        public DbSet<HeroPhotos> HeroPhotos { get; set; }
        public DbSet<Hero> Hero { get; set; }
        public DbSet<Requestor> Requestor { get; set; }

        public DbSet<Request> Request { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SampleDataEntity>().ToTable("SampleDataEntity");

            builder.Entity<Photos>().ToTable("Photos");

            builder.Entity<HeroPhotos>().ToTable("HeroPhotos");

            builder.Entity<Request>().ToTable("Requests");

            builder.Entity<Hero>()
                .HasMany(c => c.HeroPhotos)
                .WithOne(e => e.Hero);
            builder.Entity<Hero>()
                .HasOne(c => c.User)
                .WithOne(e => e.Hero)
                .HasForeignKey<Hero>();
            builder.Entity<Hero>().ToTable("Hero");

            builder.Entity<Requestor>()
                .HasOne(e => e.User)
                .WithOne(c => c.Requestor)
                .HasForeignKey<Requestor>();
            builder.Entity<Requestor>()
                .ToTable("Requestor");


            base.OnModelCreating(builder);
        }
    }
}
