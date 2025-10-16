using ConstructionEquipmentRentals.Infrastructure.Configurations;
using Domain.Booking;
using Domain.Equipment;
using Domain.Клиент;
using Microsoft.EntityFrameworkCore;

namespace ConstructionEquipmentRentals.Infrastructure.Common;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
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
