namespace Domain.Equipment
{
    public sealed record EquipmentModel
    {
        public string Value { get; }

        private EquipmentModel(string value)
        {
            Value = value;
        }

        public static EquipmentModel Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("Модель оборудования не может быть пустой.");
            return new EquipmentModel(value);
        }
    }
}
