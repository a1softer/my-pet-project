using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Клиент.Database.Configurations;

public sealed class БронированиеКлиентаEntityConfiguration : IEntityTypeConfiguration<Бронирование_клиента>
{
    public void Configure(EntityTypeBuilder<Бронирование_клиента> builder)
    {
        builder.ToTable("client_bookings");

        builder.HasKey("IdБронирования").HasName("pk_client_bookings");

        builder.Property(x => x.IdБронирования)
            .HasColumnName("booking_id")
            .HasConversion(
                toDb => toDb.Id,
                fromDb => Domain.Booking.Ид_бронирования.Create(fromDb)
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
    }
}
