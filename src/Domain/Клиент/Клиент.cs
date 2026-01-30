namespace Domain.Клиент
{
    public class Клиент
    {
        private readonly List<Domain.Booking.Бронирование> _бронирования;

        public Клиент(Ид_клиента id, ФИО_клиента фИО, Почта email, Контактный_телефон телефон, Адрес_клиента адрес)
        {
            Id = id;
            ФИО = фИО;
            Email = email;
            Адрес = адрес;
            Телефон = телефон;
            _бронирования = new List<Domain.Booking.Бронирование>();
        }

        public Ид_клиента Id { get; }
        public ФИО_клиента ФИО { get; private set; }
        public Почта Email { get; private set; }
        public Адрес_клиента Адрес { get; private set; }
        public Контактный_телефон Телефон { get; private set; }
        public IReadOnlyList<Domain.Booking.Бронирование> Бронирование => _бронирования.AsReadOnly();

        public void ДобавитьБронирование(Domain.Booking.Бронирование бронирование)
        {
            _бронирования.Add(бронирование);
        }

        public void ИзменитьСтатусБронирования(Guid idБронирования, СтатусБронирования новыйСтатус)
        {
            var бронирование = _бронирования.FirstOrDefault(b => b.Id.Id == idБронирования);
            if (бронирование != null)
            {
            }
        }

        public IEnumerable<Domain.Booking.Бронирование> ПолучитьБронированияПоСтатусу(СтатусБронирования статус)
        {
            return _бронирования.Where(b => b.Статус.Equals(статус));
        }
    }
}
