using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RevenueRecognition.Domain.Models;

namespace RevenueRecognition.Infrastructure;

public class AppDbContext : DbContext
{
    private readonly string? _connectionString;
    
    public AppDbContext() { }

    public AppDbContext(IConfiguration configuration, DbContextOptions<AppDbContext> options)
        : base(options)
    {
        _connectionString = configuration.GetConnectionString("Default") ??
                            throw new ArgumentNullException(nameof(configuration), "Connection string is not set");
    }
    
    public DbSet<CompanyClient> CompanyClients { get; set; } = null!;
    public DbSet<IndividualClient> IndividualClients { get; set; } = null!;
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CompanyClient>(builder =>
        {
            builder.ToTable("CompanyClients");
            builder.HasKey(cc => cc.Id);
            builder.Property(cc => cc.Id)
                .ValueGeneratedOnAdd();

            builder.Property(cc => cc.CompanyName)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(cc => cc.Email)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(cc => cc.PhoneNumber)
                .HasMaxLength(50);
            builder.Property(cc => cc.KrsNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.OwnsOne(cc => cc.Address, a =>
            {
                a.Property(ad => ad.Country)
                    .HasColumnName("Country")
                    .IsRequired()
                    .HasMaxLength(100);

                a.Property(ad => ad.City)
                    .HasColumnName("City")
                    .IsRequired()
                    .HasMaxLength(100);

                a.Property(ad => ad.Street)
                    .HasColumnName("Street")
                    .IsRequired()
                    .HasMaxLength(200);

                a.Property(ad => ad.PostalCode)
                    .HasColumnName("PostalCode")
                    .IsRequired()
                    .HasMaxLength(20);
            });
        });

        modelBuilder.Entity<IndividualClient>(builder =>
        {
            builder.ToTable("IndividualClients");
            builder.HasKey(ic => ic.Id);
            builder.Property(ic => ic.Id)
                .ValueGeneratedOnAdd();

            builder.Property(ic => ic.FirstName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(ic => ic.LastName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(ic => ic.Email)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(ic => ic.PhoneNumber)
                .HasMaxLength(50);
            builder.Property(ic => ic.Pesel)
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(ic => ic.IsDeleted)
                .HasDefaultValue(false);

            builder.OwnsOne(ic => ic.Address, a =>
            {
                a.Property(ad => ad.Country)
                    .HasColumnName("Country")
                    .IsRequired()
                    .HasMaxLength(100);

                a.Property(ad => ad.City)
                    .HasColumnName("City")
                    .IsRequired()
                    .HasMaxLength(100);

                a.Property(ad => ad.Street)
                    .HasColumnName("Street")
                    .IsRequired()
                    .HasMaxLength(200);

                a.Property(ad => ad.PostalCode)
                    .HasColumnName("PostalCode")
                    .IsRequired()
                    .HasMaxLength(20);
            });
        });
    }
}