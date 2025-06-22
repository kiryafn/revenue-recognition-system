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
    
    public DbSet<Client> Clients { get; set; } = null!;
    public DbSet<CompanyClient> CompanyClients { get; set; } = null!;
    public DbSet<IndividualClient> IndividualClients { get; set; } = null!;
    public DbSet<SoftwareProduct> SoftwareProducts { get; set; } = null!;
    public DbSet<Discount> Discounts { get; set; } = null!;
    public DbSet<UpfrontContract> UpfrontContracts { get; set; } = null!;
    public DbSet<Payment> Payments { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    
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
            e.Property(c => c.StartDate)
                .IsRequired();
            e.Property(c => c.EndDate)
                .IsRequired();
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
            e.HasOne(c => c.Client)
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
        
         modelBuilder.Entity<Client>(b =>
            {
                b.ToTable("Clients");
                b.HasKey(c => c.Id);
                b.Property(c => c.Id).ValueGeneratedOnAdd();

                b.HasDiscriminator<string>("ClientType")
                 .HasValue<IndividualClient>("Individual")
                 .HasValue<CompanyClient>("Company");

                b.Property(c => c.Email)
                 .HasMaxLength(200)
                 .IsRequired();
                b.Property(c => c.PhoneNumber)
                 .HasMaxLength(50);
                b.Property(ic => ic.IsDeleted)
                 .HasDefaultValue(false);

                b.OwnsOne(c => c.Address, a =>
                {
                    a.Property(x => x.Street).HasColumnName("Street").HasMaxLength(200).IsRequired();
                    a.Property(x => x.City).HasColumnName("City").HasMaxLength(100).IsRequired();
                    a.Property(x => x.PostalCode).HasColumnName("PostalCode").HasMaxLength(20).IsRequired();
                    a.Property(x => x.Country).HasColumnName("Country").HasMaxLength(100).IsRequired();
                });
            });

            modelBuilder.Entity<IndividualClient>(b =>
            {
                b.Property(ic => ic.FirstName).HasMaxLength(100).IsRequired();
                b.Property(ic => ic.LastName).HasMaxLength(100).IsRequired();
                b.Property(ic => ic.Pesel).HasMaxLength(20).IsRequired();
                
            });

            modelBuilder.Entity<CompanyClient>(b =>
            {
                b.Property(cc => cc.CompanyName).HasMaxLength(200).IsRequired();
                b.Property(cc => cc.KrsNumber).HasMaxLength(50).IsRequired();
            });
            
            modelBuilder.Entity<User>(b =>
            {
                b.ToTable("Employees");
                b.HasKey(e => e.Id);
                b.Property(e => e.Id).ValueGeneratedOnAdd();
                b.Property(e => e.Login).IsRequired().HasMaxLength(100);
                b.Property(e => e.PasswordHash).IsRequired();
                b.Property(e => e.Role).IsRequired().HasMaxLength(50);
            });
    }
}