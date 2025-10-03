namespace Domain.Клиент
{
    public class Клиент
    {
        private readonly List<Бронирование_клиента> _бронирования;

        public Клиент(Ид_клиента id, ФИО_клиента фИО, Адрес_клиента адрес, Контактный_телефон телефон)
        {
            Id = id;
            ФИО = фИО;
            Адрес = адрес;
            Телефон = телефон;
            _бронирования = new List<Бронирование_клиента>();
        }

        public Ид_клиента Id { get; }
        public ФИО_клиента ФИО { get; private set; }
        public Адрес_клиента Адрес { get; private set; }
        public Контактный_телефон Телефон { get; private set; }

        public IReadOnlyList<Бронирование_клиента> Бронирование => _бронирования.AsReadOnly();
    }
}
