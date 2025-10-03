namespace Domain.Клиент
{
    public record Адрес_клиента
    {
        public string Значение { get; }

        private Адрес_клиента(string значение)
        {
            Значение = значение;
        }

        public static Адрес_клиента Create(string значение)
        {
            if (string.IsNullOrWhiteSpace(значение))
                throw new ArgumentException("Адрес клиента не может быть пустым.");

            return new Адрес_клиента(значение.Trim());
        }
    }
}
