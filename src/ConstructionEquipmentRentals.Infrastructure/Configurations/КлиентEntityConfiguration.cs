using Domain.Клиент;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalService.Application.Services;

namespace ConstructionEquipmentRentals.Infrastructure.Configurations;

public sealed class КлиентEntityConfiguration : IEntityTypeConfiguration<Клиент>
{
    public void Configure(EntityTypeBuilder<Клиент> builder)
    {
        builder.ToTable("clients");

        builder.HasKey(x => x.Id).HasName("pk_clients");

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasConversion(
                toDb => toDb.Id,
                fromDb => Ид_клиента.Create(fromDb)
            );


        builder.Property(f => f.ФИО)
            .HasColumnName("full_name")
            .HasConversion(
                x => x.Значние,
                x => ФИО_клиента.Create(x)
             )
                    .IsRequired()
                    .HasMaxLength(200);

                builder.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasConversion(
                    x => x.Email,
                    x => Почта.Create(x)
                    )
                    .IsRequired()
                    .HasMaxLength(100);

                builder.Property(t => t.Телефон)
                    .HasColumnName("phone")
                    .HasConversion(
                    x => x.Номер,
                    x => Контактный_телефон.Create(x)
                    )
                    .IsRequired()
                    .HasMaxLength(20);

                builder.Property(a => a.Адрес)
                    .HasColumnName("address")
                    .HasConversion(
                    x => x.Значение,
                    x => Адрес_клиента.Create(x)
                    )
                    .IsRequired()
                    .HasMaxLength(300);

        builder.HasMany(x => x.Бронирование)
            .WithOne(b => b.Клиент)
            .HasForeignKey(b => b.CustomerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.Email).IsUnique();
        builder.HasIndex(x => x.Телефон).IsUnique();
    }
}
