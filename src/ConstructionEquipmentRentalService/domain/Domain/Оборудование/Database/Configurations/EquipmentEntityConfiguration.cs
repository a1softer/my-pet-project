using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Equipment.Database.Configurations;

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

        builder.ComplexProperty(
            x => x.RentalCostPerHour,
            cpb =>
            {
                cpb.Property(r => r.Value)
                    .HasColumnName("rental_cost_per_hour")
                    .IsRequired()
                    .HasPrecision(18, 2);
            }
        );

        builder.ComplexProperty(
            x => x.Model,
            cpb =>
            {
                cpb.Property(m => m.Value)
                    .HasColumnName("model")
                    .IsRequired()
                    .HasMaxLength(100);
            }
        );

        builder.ComplexProperty(
            x => x.Type,
            cpb =>
            {
                cpb.ComplexProperty(
                    t => t,
                    typeBuilder =>
                    {
                        typeBuilder.Property(t => t.Key).HasColumnName("type_key").IsRequired();
                        typeBuilder.Property(t => t.Name).HasColumnName("type_name").IsRequired();
                    }
                );
            }
        );

        builder.ComplexProperty(
            x => x.LastMaintenanceDate,
            cpb =>
            {
                cpb.Property(l => l.Date)
                    .HasColumnName("last_maintenance_date")
                    .IsRequired();
            }
        );

        builder.ComplexProperty(
            x => x.WearPrecentage,
            cpb =>
            {
                cpb.Property(w => w.Procent)
                    .HasColumnName("wear_percentage")
                    .IsRequired();
            }
        );

        builder.HasIndex(x => x.Model.Value);
        builder.HasIndex(x => x.Type.Key);
    }
}
