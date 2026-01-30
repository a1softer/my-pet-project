namespace Domain.Booking
{
    public class Дата_начала
    {
        public DateOnly Date { get; }

        private Дата_начала(DateOnly date)
        {
            Date = date;
        }

        public static Дата_начала Create(DateOnly date)
        {
            if (date < DateOnly.FromDateTime(DateTime.Now))
                throw new ArgumentException("Дата начала не может быть в прошлом.");
            return new Дата_начала(date);
        }
        public static Дата_начала CreateToday()
        {
            return new Дата_начала(DateOnly.FromDateTime(DateTime.Now));
        }
    }
}
