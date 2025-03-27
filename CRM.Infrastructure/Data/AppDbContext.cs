using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using CRM.Application.Interfaces;

namespace CRM.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Customer> Customers => Set<Customer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Ek konfigurasyonlar buraya

        // Seed mock data
        modelBuilder.Entity<Customer>().HasData(
            new Customer
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Region = "North America",
                RegistrationDate = new DateTime(2023, 6, 15, 0, 0, 0, DateTimeKind.Utc)
            },
            new Customer
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                Region = "Europe",
                RegistrationDate = new DateTime(2023, 5, 10, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
