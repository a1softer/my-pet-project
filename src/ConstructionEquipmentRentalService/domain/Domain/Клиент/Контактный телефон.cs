namespace Domain.Клиент
{
    public record Контактный_телефон
    {
        public string Номер { get; }

        private Контактный_телефон(string номер)
        {
            Номер = номер;
        }

        public static Контактный_телефон Create(string номер)
        {
            if (string.IsNullOrWhiteSpace(номер))
                throw new ArgumentException("Контактный телефон не может быть пустым.");

            // Проверка формата: +7XXXYYYYYYY (11 цифр)
            string очищенныйНомер = new string(номер.Where(char.IsDigit).ToArray());

            if (очищенныйНомер.Length != 11)
                throw new ArgumentException("Контактный телефон должен содержать 11 цифр.");

            if (!очищенныйНомер.StartsWith("7"))
                throw new ArgumentException("Контактный телефон должен начинаться с 7.");

            return new Контактный_телефон(очищенныйНомер);
        }
    }
}
