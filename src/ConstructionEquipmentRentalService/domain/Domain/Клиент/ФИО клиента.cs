namespace Domain.Клиент
{
    public record ФИО_клиента
    {
        public string Значние { get; }

        private ФИО_клиента(string значение)
        {
            Значние = значение;
        }

        public static ФИО_клиента Create(string значение)
        {
            if (string.IsNullOrWhiteSpace(значение))
                throw new ArgumentException("ФИО клиент не может быть пустым.");

            if (значение.Trim().Length < 5)
                throw new ArgumentException("ФИО клиента должно содержать минимум 5 символов.");

            return new ФИО_клиента(значение.Trim());
        }
    }
}
