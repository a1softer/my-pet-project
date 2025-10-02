namespace Domain.Equipment
{
    public class IDEquipment
    {
        public Guid Id { get; }

        private IDEquipment(Guid id)
        {
            Id = id;
        }

        public static IDEquipment Create(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Идентификатор оборудования не может быть пустым.");

            return new IDEquipment(id);
        }

        public static IDEquipment CreateNew()
        {
            return new IDEquipment(Guid.NewGuid());
        }
    }
}
