namespace Domain.Equipment
{
    public class Equipment
    {
        public Equipment(IDEquipment id, RentalCost rentalCostPerHour, EquipmentModel model, ТипОборудования type, LastDateTO lastMaintenanceDate, StateProcent wearPrecentage)
        {
            Id = id;
            RentalCostPerHour = rentalCostPerHour;
            Model = model;
            Type = type;
            LastMaintenanceDate = lastMaintenanceDate;
            WearPrecentage = wearPrecentage;
            IsActive = true;
        }

        public IDEquipment Id { get; }
        public RentalCost RentalCostPerHour { get; private set; }
        public EquipmentModel Model { get; private set; }
        public ТипОборудования Type { get; private set; }
        public LastDateTO LastMaintenanceDate { get; private set; }
        public StateProcent WearPrecentage { get; private set; }
        public bool IsActive { get; private set; }

        public void DeactivateIfWornOut()
        {
            if (WearPrecentage.Procent >= 100)
            {
                IsActive = false;
            }
        }

        public void IncreaseWear(double amount)
        {
            DeactivateIfWornOut();
        }
    }
}
