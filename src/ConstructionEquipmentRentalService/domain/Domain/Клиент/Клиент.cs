namespace Domain.Клиент
{
    public class Клиент
    {
        private readonly List<Бронирование_клиента> _бронирования;

        public Клиент(Ид_клиента id, ФИО_клиента фИО, Почта email, Контактный_телефон телефон, Адрес_клиента адрес)
        {
            Id = id;
            ФИО = фИО;
            Email = email;
            Адрес = адрес;
            Телефон = телефон;
            _бронирования = new List<Бронирование_клиента>();
        }

        public Ид_клиента Id { get; }
        public ФИО_клиента ФИО { get; private set; }
        public Почта Email { get; private set; }
        public Адрес_клиента Адрес { get; private set; }
        public Контактный_телефон Телефон { get; private set; }

        public IReadOnlyList<Бронирование_клиента> Бронирование => _бронирования.AsReadOnly();

        public static Клиент CreateNew(ФИО_клиента фио, Почта email, Контактный_телефон телефон, Адрес_клиента адрес)
        {
            return new Клиент(Ид_клиента.CreateNew(), фио, email, телефон, адрес);
        }

        public void ДобавитьБронирование(Бронирование_клиента бронирование)
        {
            _бронирования.Add(бронирование);
        }
    }
}
