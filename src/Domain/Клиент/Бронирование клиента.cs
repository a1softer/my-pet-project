namespace Domain.Клиент
{
    public record Бронирование_клиента
    {
        public Domain.Booking.Ид_бронирования IdБронирования { get; }
        public СтатусБронирования Статус {  get; }

        private Бронирование_клиента(Domain.Booking.Ид_бронирования idБронирования, СтатусБронирования статус)
        {
            IdБронирования = idБронирования;
            Статус = статус;
        }

        public static Бронирование_клиента Create(Domain.Booking.Ид_бронирования idБронирования, СтатусБронирования статус)
        {
            if (idБронирования == null)
                throw new ArgumentException(nameof(idБронирования));

            if (статус == null)
                throw new ArgumentNullException(nameof(статус));

            return new Бронирование_клиента(idБронирования, статус);
        }
    }
}
