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
        /// Принимает деактивацию от бронирования и применяет износ
        /// </summary>
        /// <param name="booking">Бронирование, которое завершается</param>
        /// <param name="bookingDurationDays">Длительность бронирования в днях</param>
        public void AcceptDeactivationBy(Domain.Booking.Бронирование booking, double bookingDurationDays)
        {
            if (booking == null)
                throw new ArgumentNullException(nameof(booking));

            if (booking.EquipmentId.Id != Id.Id)
                throw new InvalidOperationException("Оборудование не соответствует бронированию");

            var wearIncrease = Math.Max(1, bookingDurationDays * 0.5);
            IncreaseWearInternal(wearIncrease);

            if (WearPrecentage.Procent + wearIncrease >= 100)
            {
                IsActive = false;
            }
        }

        /// <summary>
        /// Низкоуровневая логика увеличения износа (приватная)
        /// </summary>
        private void IncreaseWearInternal(double amount)
        {
            var newWear = WearPrecentage.Procent + amount;
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
