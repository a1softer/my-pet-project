using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Клиент.Database.Configurations;

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

        builder.ComplexProperty(
            x => x.ФИО,
            cpb =>
            {
                cpb.Property(f => f.Значние)
                    .HasColumnName("full_name")
                    .IsRequired()
                    .HasMaxLength(200);
            }
        );

        builder.ComplexProperty(
            x => x.Email,
            cpb =>
            {
                cpb.Property(e => e.Email)
                    .HasColumnName("email")
                    .IsRequired()
                    .HasMaxLength(100);
            }
        );

        builder.ComplexProperty(
            x => x.Телефон,
            cpb =>
            {
                cpb.Property(t => t.Номер)
                    .HasColumnName("phone")
                    .IsRequired()
                    .HasMaxLength(20);
            }
        );

        builder.ComplexProperty(
            x => x.Адрес,
            cpb =>
            {
                cpb.Property(a => a.Значение)
                    .HasColumnName("address")
                    .IsRequired()
                    .HasMaxLength(300);
            }
        );

        // Конфигурация для коллекции бронирований
        builder.HasMany(x => x.Бронирование)
            .WithOne()
            .HasForeignKey("ClientId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.Email.Email).IsUnique();
        builder.HasIndex(x => x.Телефон.Номер).IsUnique();
    }
}
