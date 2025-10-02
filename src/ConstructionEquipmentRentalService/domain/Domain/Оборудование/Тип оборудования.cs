namespace Domain.Equipment
{
    public sealed record EquipmentType
    {
        public string Value { get; }

        private EquipmentType(string value)
        {
            Value = value;
        }

        public static EquipmentType Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Тип оборудования не может быть пустым.");
            return new EquipmentType(value);
        }
    }
}
