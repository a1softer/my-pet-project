using Domain.Equipment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace ConstructionEquipmentRentals.Infrastructure.Configurations;

public sealed class EquipmentEntityConfiguration : IEntityTypeConfiguration<Equipment>
{
    public void Configure(EntityTypeBuilder<Equipment> builder)
    {
        builder.ToTable("equipment");

        builder.HasKey(x => x.Id).HasName("pk_equipment");

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasConversion(
                toDb => toDb.Id,
                fromDb => IDEquipment.Create(fromDb)
            );

        builder.Property(x => x.RentalCostPerHour)
                    .HasColumnName("rental_cost_per_hour")
                    .HasConversion(
            x => x.Value,
            x => RentalCost.Create(x)
            )
                    .IsRequired()
                    .HasPrecision(18, 2);

        builder.Property(x => x.Model)
                    .HasColumnName("model")
                    .HasConversion(
            x => x.Value,
            x => EquipmentModel.Create(x)
            )
                    .IsRequired()
                    .HasMaxLength(100);

        builder.ComplexProperty(
            x => x.Type,
            cpb =>
            {
                cpb.Property(t => t.Key).HasColumnName("type_key").IsRequired();
                cpb.Property(t => t.Name).HasColumnName("type_name").IsRequired();
            }
        );

        builder.Property(x => x.LastMaintenanceDate)
                    .HasColumnName("last_maintenance_date")
                    .HasConversion(
            x => x.Date,
            x => LastDateTO.Create(x)
            )
                    .IsRequired();


        builder.Property(x => x.WearPrecentage)
                    .HasColumnName("wear_percentage")
                    .HasConversion(
            x => x.Procent,
            x => StateProcent.Create(x)
            )
                    .IsRequired();

        //builder.HasIndex(x => x.Model);
    }
}
