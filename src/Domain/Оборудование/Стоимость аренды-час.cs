namespace Domain.Equipment
{
    public sealed record RentalCost
    {
        public decimal Value { get; }

        private RentalCost(decimal value)
        {
            Value = value;
        }

        public static RentalCost Create(decimal value)
        {
            if (value <= 0)
                throw new ArgumentException("Стоимость аренды должна быть больше нуля.");
            return new RentalCost(value);
        }
    }
}
