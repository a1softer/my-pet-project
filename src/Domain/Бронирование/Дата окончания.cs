namespace Domain.Booking
{
    public class Дата_окончания
    {
        private Дата_окончания()
        {

        }

        public DateOnly Date { get; }

        private Дата_окончания(DateOnly date)
        {
            Date = date;
        }

        public static Дата_окончания Create(DateOnly date, Дата_начала дата_Начала)
        {
            if (date <= дата_Начала.Date)
                throw new ArgumentException("Дата окончания должна быть после даты начала.");
            if (date > дата_Начала.Date.AddDays(30))
                throw new ArgumentException("Бронирование не может превышать 30 дней.");

            return new Дата_окончания(date);
        }

        public static Дата_окончания Create(DateOnly date)
        {
            

            return new Дата_окончания(date);
        }
    }
}
