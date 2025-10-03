namespace Domain.Клиент
{
    public record Бронирование_клиента
    {
        public Domain.Booking.Ид_бронирования IdБронирования { get; }
        public Статус_бронирования Статус {  get; }

        private Бронирование_клиента(Domain.Booking.Ид_бронирования idБронирования, Статус_бронирования статус)
        {
            IdБронирования = idБронирования;
            Статус = статус;
        }

        public static Бронирование_клиента Create(Domain.Booking.Ид_бронирования idБронирования, Статус_бронирования статус)
        {
            if (idБронирования == null)
                throw new ArgumentException(nameof(idБронирования));

            return new Бронирование_клиента(idБронирования, статус);
        }
    }
}
