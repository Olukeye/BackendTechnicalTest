using BackendTechnicalTest.Application.DTOs;
using BackendTechnicalTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BackendTechnicalTest.Infrastructure;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Country> Countries => Set<Country>();

    public DbSet<CountryDetail> CountryDetails => Set<CountryDetail>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.CountryCode)
                .IsRequired()
                .HasMaxLength(10);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(x => x.CountryIso)
                .IsRequired()
                .HasMaxLength(10);

            entity.HasMany(x => x.CountryDetails)
                .WithOne(x => x.Country)
                .HasForeignKey(x => x.CountryId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<CountryDetail>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Operator)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(x => x.OperatorCode)
                .IsRequired()
                .HasMaxLength(50);
        });
    }
}
