using System.Text.RegularExpressions;

namespace Domain.Клиент
{
    public record Почта
    {
        public string Email { get; }

        // Статическое регулярное выражение для email
        private static readonly Regex _emailValidationRegex = new Regex(@"\b([^\d]\w+)[@]([^\d]\w+)[.](com|ru)\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private Почта(string email)
        {
            Email = email;
        }

        public static Почта Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException("Email не может быть пустым.");

            // Проверка на минимальную длину
            if (email.Length < 5)
                throw new ArgumentException("Email слишком короткий.");

            // Проверка формата с помощью регулярного выражения
            Match match = _emailValidationRegex.Match(email);
            if (!match.Success)
                throw new ArgumentException("Email имеет некорректный формат. Используйте формат: user@mail.com или user@mail.ru");

            return new Почта(email);
        }
    }
}
