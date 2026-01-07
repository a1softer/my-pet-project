using ConstructionEquipmentRentals.Infrastructure.Configurations;
using ConstructionEquipmentRentals.Infrastructure.Common.Configurations;
using Domain.Booking;
using Domain.Equipment;
using Domain.Клиент;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ConstructionEquipmentRentals.Infrastructure.Common;

public sealed class ApplicationDbContext : DbContext
{
    private readonly string _connectionString;

    public ApplicationDbContext(IOptions<PostgresConnectionOptions> options)
    {
        _connectionString = options.Value.AsConnectionString();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
    }

    public DbSet<Бронирование> Bookings { get; set; }
    public DbSet<Клиент> Clients { get; set; }
    public DbSet<Equipment> Equipment { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new БронированиеEntityConfiguration());
        modelBuilder.ApplyConfiguration(new КлиентEntityConfiguration());
        modelBuilder.ApplyConfiguration(new EquipmentEntityConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
