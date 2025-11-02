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
        /// Высокоуровневая бизнес-логика: применение износа от завершения бронирования
        /// </summary>
        /// <param name="bookingDurationDays">Длительность бронирования в днях</param>
        public void ApplyBookingWear(double bookingDurationDays)
        {
            var wearIncrease = Math.Max(1, bookingDurationDays * 0.5);
            IncreaseWearInternal(wearIncrease);
        }

        /// <summary>
        /// Низкоуровневая логика увеличения износа (приватная)
        /// </summary>
        /// <param name="amount">Количество износа для добавления</param>
        private void IncreaseWearInternal(double amount)
        {
            var newWear = WearPrecentage.Procent + amount;

            if (newWear >= 100)
            {
                IsActive = false;
            }
        }

        /// <summary>
        /// Проверяет, можно ли забронировать оборудование
        /// </summary>
        public bool CanBeBooked()
        {
            return IsActive && WearPrecentage.Procent < 100;
        }
    }
}
