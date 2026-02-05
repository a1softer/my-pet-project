namespace Domain.Booking
{
    public class Сумма_залога
    {
        public decimal Amount { get; }

        private Сумма_залога(decimal amount)
        {
            Amount = amount;
        }

        public static Сумма_залога Create(decimal amount)
        {
           
            return new Сумма_залога(amount);
        }

        public static Сумма_залога Create(decimal amount, decimal rentalCostPerDay, int days)
        {
            decimal expectedDeposit = rentalCostPerDay * days * 0.3m;
            if (amount < expectedDeposit * 0.8m)
                throw new ArgumentException($"Сумма залог слишком мала. Минимальная сумма: {expectedDeposit * 0.8m:C}");

            return new Сумма_залога(amount);
        }
    }
}
