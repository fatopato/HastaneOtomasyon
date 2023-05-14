using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

public class MyDbContext : DbContext
{
    public DbSet<Hasta> Hastalar { get; set; }
    public DbSet<Doktor> Doktorlar { get; set; }
    public DbSet<Randevu> Randevular { get; set; }
    public DbSet<Tedavi> Tedaviler { get; set; }
    public DbSet<Ilac> Ilaclar { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=MyDatabase.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define the relationships between tables
        modelBuilder.Entity<Randevu>()
            .HasOne(r => r.Hasta)
            .WithMany(h => h.Randevular)
            .HasForeignKey(r => r.HastaID);

        modelBuilder.Entity<Randevu>()
            .HasOne(r => r.Doktor)
            .WithMany(d => d.Randevular)
            .HasForeignKey(r => r.DoktorID);

        modelBuilder.Entity<Tedavi>()
            .HasOne(t => t.Randevu)
            .WithMany(r => r.Tedaviler)
            .HasForeignKey(t => t.RandevuID);

        modelBuilder.Entity<Tedavi>()
            .HasOne(t => t.Doktor)
            .WithMany(d => d.Tedaviler)
            .HasForeignKey(t => t.DoktorID);
    }
}