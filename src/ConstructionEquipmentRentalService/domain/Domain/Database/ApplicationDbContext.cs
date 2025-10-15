using Domain.Booking;
using Domain.Equipment;
using Domain.Equipment.Database.Configurations;
using Domain.Бронирование.Database.Configurations;
using Domain.Клиент;
using Domain.Клиент.Database.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ConstructionEquipmentRentals.Data;

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
        modelBuilder.ApplyConfiguration(new БронированиеКлиентаEntityConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
