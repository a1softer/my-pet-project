using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Booking.Database.Configurations;

public sealed class БронированиеEntityConfiguration : IEntityTypeConfiguration<Бронирование>
{
    public void Configure(EntityTypeBuilder<Бронирование> builder)
    {
        builder.ToTable("bookings");

        builder.HasKey(x => x.Id).HasName("pk_bookings");

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasConversion(
                toDb => toDb.Id,
                fromDb => Ид_бронирования.Create(fromDb)
            );

        builder.Property(x => x.CustomerId)
            .HasColumnName("customer_id")
            .HasConversion(
                toDb => toDb.Id,
                fromDb => Ид_клиента.Create(fromDb)
            )
            .IsRequired();

        builder.Property(x => x.EquipmentId)
            .HasColumnName("equipment_id")
            .HasConversion(
                toDb => toDb.Id,
                fromDb => Ид_оборудования.Create(fromDb)
            )
            .IsRequired();

        builder.ComplexProperty(
            x => x.StartDate,
            cpb =>
            {
                cpb.Property(s => s.Date)
                    .HasColumnName("start_date")
                    .IsRequired();
            }
        );

        builder.ComplexProperty(
            x => x.EndDate,
            cpb =>
            {
                cpb.Property(s => s.Date)
                    .HasColumnName("end_date")
                    .IsRequired();
            }
        );

        builder.ComplexProperty(
            x => x.DepositAmount,
            cpb =>
            {
                cpb.Property(s => s.Amount)
                    .HasColumnName("deposit_amount")
                    .IsRequired()
                    .HasPrecision(18, 2);
            }
        );

        builder.ComplexProperty(
            x => x.Статус,
            cpb =>
            {
                cpb.ComplexProperty(
                    s => s,
                    statusBuilder =>
                    {
                        statusBuilder.Property(s => s.Key).HasColumnName("status_key").IsRequired();
                        statusBuilder.Property(s => s.Name).HasColumnName("status_name").IsRequired();
                    }
                );
            }
        );

        builder.HasOne(x => x.Клиент)
            .WithMany(c => c.Бронирование)
            .HasForeignKey(x => x.CustomerId)
            .IsRequired();
    }
}
