namespace Domain.Equipment
{
    public class Equipment
    {
        public Equipment(IDEquipment id, RentalCost rentalCostPerHour, EquipmentModel model, EquipmentType type, LastDateTO lastMaintenanceDate, StateProcent wearPrecentage)
        {
            Id = id;
            RentalCostPerHour = rentalCostPerHour;
            Model = model;
            Type = type;
            LastMaintenanceDate = lastMaintenanceDate;
            WearPrecentage = wearPrecentage;
        }
        public IDEquipment Id { get; }
        public RentalCost RentalCostPerHour { get; private set; }
        public EquipmentModel Model { get; private set; }
        public EquipmentType Type { get; private set; }
        public LastDateTO LastMaintenanceDate { get; private set; }
        public StateProcent WearPrecentage { get; private set; }
    }
}
