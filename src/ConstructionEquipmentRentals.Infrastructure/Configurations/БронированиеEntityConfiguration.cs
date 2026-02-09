using Domain.Booking;
using Domain.Бронирование;
using Domain.Клиент;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstructionEquipmentRentals.Infrastructure.Configurations;

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

        builder.Property(x => x.StartDate).HasColumnName("start_date")
            .IsRequired().HasConversion(toDb => toDb.Date, fromDb => Дата_начала.Create(fromDb));

        builder.Property(x => x.EndDate)
            .HasColumnName("end_date")
            .IsRequired()
            .HasConversion(toDb => toDb.Date, fromDb => Дата_окончания.Create(fromDb));

        builder.Property(
            x => x.DepositAmount).IsRequired().HasColumnName("deposit_amount").HasConversion(toDb => toDb.Amount, fromDb => Сумма_залога.Create(fromDb));

        builder.ComplexProperty(
            x => x.Статус,
            cpb =>
            {
                cpb.Property(s => s.Key).HasColumnName("status_key").IsRequired();
                cpb.Property(s => s.Name).HasColumnName("status_name").IsRequired();
            }
        );

        //builder.HasOne(x => x.Клиент)
        //    .WithMany(c => c.Бронирование)
        //    .HasForeignKey(x => x.CustomerId)
        //    .IsRequired();
    }
}
