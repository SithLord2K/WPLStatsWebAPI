using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WPLStatsCoreWebAPI.Models;

namespace WPLStatsCoreWebAPI.Data;

public partial class WPLStatsDbContext : DbContext
{
    public WPLStatsDbContext()
    {
    }

    public WPLStatsDbContext(DbContextOptions<WPLStatsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<TeamDetail> TeamDetails { get; set; }

    public virtual DbSet<Week> Weeks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("wiley");

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.EntryId);

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<TeamDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Teams");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Captain).HasMaxLength(50);
            entity.Property(e => e.TeamName).HasMaxLength(50);
        });

        modelBuilder.Entity<Week>(entity =>
        {
            entity.HasKey(e => e.WeekNumber);

            entity.Property(e => e.WeekNumber).ValueGeneratedNever();
            entity.Property(e => e.DatePlayed).HasColumnType("date");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
