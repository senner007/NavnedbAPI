﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NavnedbAPI.Models
{
    public partial class NavnedbContext : DbContext
    {
        public NavnedbContext()
        {
        }

        public NavnedbContext(DbContextOptions<NavnedbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Navne> Navne { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(AppSettingsClass.MyConnection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Navne>(entity =>
            {
                entity.ToTable("navne");

                entity.HasIndex(e => new { e.Navn, e.Id })
                    .HasName("nameindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Køn)
                    .HasColumnName("køn")
                    .HasColumnType("varchar(2)");

                entity.Property(e => e.Navn)
                    .HasColumnName("navn")
                    .HasColumnType("varchar(70)");
            });
        }
    }
}
