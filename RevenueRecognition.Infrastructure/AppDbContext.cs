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
    public DbSet<SoftwareProduct> SoftwareProducts { get; set; } = null!;
    public DbSet<Discount> Discounts { get; set; } = null!;
    public DbSet<UpfrontContract> UpfrontContracts { get; set; } = null!;
    public DbSet<Payment> Payments { get; set; } = null!;
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(_connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UpfrontContract>(e =>
        {
            e.ToTable("UpfrontContracts");
            e.HasKey(c => c.Id);
            e.Property(c => c.Id).ValueGeneratedOnAdd();

            e.Property(c => c.SoftwareVersion)
                .IsRequired()
                .HasMaxLength(50);

            e.Property(c => c.StartDate).IsRequired();
            e.Property(c => c.EndDate).IsRequired();
            e.Property(c => c.BaseCost)
                .IsRequired()
                .HasColumnType("numeric(18,2)");
            e.Property(c => c.SupportYears)
                .IsRequired();
            e.Property(c => c.AppliedDiscountPct)
                .IsRequired()
                .HasColumnType("decimal(5,2)");
            e.Property(c => c.TotalCost)
                .IsRequired()
                .HasColumnType("numeric(18,2)");
            e.Property(c => c.Status)
                .HasConversion<int>()
                .IsRequired();

            e.HasMany(c => c.Payments)
                .WithOne(p => p.UpfrontContract)
                .HasForeignKey(p => p.UpfrontContractId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(c => c.IndividualClient)
                .WithMany()
                .HasForeignKey(c => c.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(c => c.CompanyClient)
                .WithMany()
                .HasForeignKey(c => c.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(c => c.SoftwareProduct)
                .WithMany(p => p.UpfrontContracts)
                .HasForeignKey(c => c.SoftwareProductId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        modelBuilder.Entity<Payment>(e =>
        {
            e.ToTable("Payments");
            e.HasKey(p => p.Id);
            e.Property(p => p.Id).ValueGeneratedOnAdd();
            e.Property(p => p.Amount)
                .IsRequired()
                .HasColumnType("numeric(18,2)");
            e.Property(p => p.PaidAt)
                .IsRequired();
        });
        
        modelBuilder.Entity<SoftwareProduct>(b =>
        {
            b.ToTable("SoftwareProducts");
            b.HasKey(p => p.Id);
            b.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            b.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);
            b.Property(p => p.Description)
                .HasMaxLength(1000);
            b.Property(p => p.Version)
                .IsRequired()
                .HasMaxLength(50);
            b.Property(p => p.Category)
                .IsRequired()
                .HasMaxLength(100);

            b.Property(p => p.UpfrontCost)
                .HasColumnType("numeric(18,2)");
            b.Property(p => p.SubscriptionCost)
                .HasColumnType("numeric(18,2)");

            b.HasMany(p => p.Discounts)
                .WithOne(d => d.SoftwareProduct)
                .HasForeignKey(d => d.SoftwareProductId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasMany<UpfrontContract>() 
                .WithOne(c => c.SoftwareProduct)
                .HasForeignKey(c => c.SoftwareProductId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Discount>(b =>
        {
            b.ToTable("Discounts");
            b.HasKey(d => d.Id);
            b.Property(d => d.Id).ValueGeneratedOnAdd();
            b.Property(d => d.Name).IsRequired().HasMaxLength(200);
            b.Property(d => d.Percentage)
                .IsRequired()
                .HasColumnType("decimal(5,2)");
            b.Property(d => d.StartDate)
                .IsRequired();
            b.Property(d => d.EndDate)
                .IsRequired();
            b.Property(d => d.AppliesTo)
                .HasConversion<int>(); 
        });
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