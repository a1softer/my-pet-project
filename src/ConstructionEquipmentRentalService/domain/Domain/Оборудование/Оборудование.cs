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

        /// <summary>
        /// Увеличивает износ оборудования и деактивирует при достижении 100%
        /// </summary>
        /// <param name="amount">Количество износа для добавления</param>
        public void IncreaseWear(double amount)
        {
            var newWear = WearPrecentage.Procent + amount;

            if (newWear >= 100)
            {
                IsActive = false;
            }
        }

        /// <summary>
        /// Деактивирует оборудование (например, при критическом износе)
        /// </summary>
        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
