namespace Domain.Equipment
{
    public class LastDateTO
    {
        private LastDateTO() 
        {
            
        }

        public DateOnly Date { get; }

        private LastDateTO(DateOnly date)
        {
            Date = date;
        }

        public  static LastDateTO Create(DateOnly date)
        {
            if (date > DateOnly.FromDateTime(DateTime.Now))
                throw new ArgumentException("Дата последнего ТО не может быть в будущем.");

            return new LastDateTO(date);
        }

        public static LastDateTO CreateToday()
        {
            return new LastDateTO(DateOnly.FromDateTime(DateTime.Now));
        }
    }
}
