using Bank.Domain.Users;
using Bank.Domain.Users.Entities;
using Bank.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;

namespace Bank.Persistence.Context;

internal class ApplicationContext : DbContext
{
    private readonly string _connectionString;
    public DbSet<User> Users { get; set; }

    public ApplicationContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Postgres")!; // required for application startup
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(user =>
        {
            user.ToTable("users");

            user.HasKey(u => u.Tz);

            user.Property(p => p.Tz)
                .HasConversion(new ValueConverter<Tz, string>(
                    tz => tz.Value,
                    value => Tz.Create(value).Value))
                .HasColumnName("tz");

            user.OwnsOne(p => p.FullName, b =>
            {
                b.Property(p => p.Value)
                    .HasColumnName("full_name")
                    .HasMaxLength(FullName.MaxCharacters);
            });

            user.OwnsOne(p => p.EnglishFullName, b =>
            {
                b.Property(p => p.Value)
                    .HasColumnName("english_full_name")
                    .HasMaxLength(EnglishFullName.MaxCharacters);
            });

            user.OwnsOne(p => p.DateOfBirthInUtc, b =>
            {
                b.Property(p => p.Value)
                .HasColumnName("date_of_birth");
            });

            user.OwnsOne(p => p.Password, b =>
            {
                b.Property(p => p.Value)
                .HasColumnName("password");
            });

            user.OwnsOne(p => p.Account, b =>
            {
                b.Property(p => p.Id)
                    .HasColumnName("id")
                    .HasMaxLength(Account.MaxAccountId);

                b.Property(p => p.Currency)
                    .HasColumnName("currency");

                b.OwnsMany(p => p.Operations, b =>
                {
                    b.ToTable("operations");

                    b.Property(p => p.Value)
                        .HasColumnName("value");

                    b.Property(p => p.Type)
                     .HasColumnName("type");
                });
            });

        });

    }

}
