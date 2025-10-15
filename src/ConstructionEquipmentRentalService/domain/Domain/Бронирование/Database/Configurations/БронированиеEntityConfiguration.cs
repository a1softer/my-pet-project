using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Бронирование.Database.Configurations;

public sealed class БронированиеEntityConfiguration : IEntityTypeConfiguration<global::Domain.Booking.Бронирование>
{
    public void Configure(EntityTypeBuilder<global::Domain.Booking.Бронирование> builder)
    {
        builder.ToTable("bookings");

        builder.HasKey(x => x.Id).HasName("pk_bookings");

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasConversion(
                toDb => toDb.Id,
                fromDb => global::Domain.Booking.Ид_бронирования.Create(fromDb)
            );

        builder.Property(x => x.CustomerId)
            .HasColumnName("customer_id")
            .HasConversion(
                toDb => toDb.Id,
                fromDb => global::Domain.Booking.Ид_клиента.Create(fromDb)
            )
            .IsRequired();

        builder.Property(x => x.EquipmentId)
            .HasColumnName("equipment_id")
            .HasConversion(
                toDb => toDb.Id,
                fromDb => global::Domain.Booking.Ид_оборудования.Create(fromDb)
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
    }
}
