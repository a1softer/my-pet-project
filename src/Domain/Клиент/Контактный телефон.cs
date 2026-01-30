using System.Text.RegularExpressions;

namespace Domain.Клиент
{
    public record Контактный_телефон
    {
        public string Номер { get; }

        // Статическое регулярное выражение для производительности
        private static readonly Regex _phoneValidationRegex = new Regex(@"\B[+]\d\s[(]\d{3}[)]\s\d{3}\s\d{2}[-]\d{2}\b", RegexOptions.Compiled);

        private Контактный_телефон(string номер)
        {
            Номер = номер;
        }

        public static Контактный_телефон Create(string номер)
        {
            if (string.IsNullOrWhiteSpace(номер))
                throw new ArgumentException("Контактный телефон не может быть пустым.");

            //Проверка на минимальную длину
            if (номер.Length < 10)
                throw new ArgumentException("Номер телефона слишком короткий.");

            // Проверка на максимальную длину
            if (номер.Length > 20)
                throw new ArgumentException("Номер телефон слишком длинный.");

            // Проверка формата с помощью регулярного выражения
            Match match = _phoneValidationRegex.Match(номер);
            if (!match.Success)
                throw new ArgumentException("Номер телефона имеет некорректный формат. Используйте формата: +7 (123) 456 78-90");

            return new Контактный_телефон(номер);
        }
    }
}
