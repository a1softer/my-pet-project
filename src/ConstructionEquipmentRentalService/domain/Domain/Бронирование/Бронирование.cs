using Domain.Клиент;

namespace Domain.Booking
{
    public class Бронирование
    {
        public Бронирование(Ид_бронирования id, Ид_клиента customerId, Ид_оборудования equipmentId, Дата_начала startDate, Дата_окончания endDate, Сумма_залога depositAmount, Domain.Клиент.СтатусБронирования статус, Domain.Клиент.Клиент клиент)
        {
            Id = id;
            CustomerId = customerId;
            EquipmentId = equipmentId;
            StartDate = startDate;
            EndDate = endDate;
            DepositAmount = depositAmount;
            Статус = статус;
            Клиент = клиент;
        }

        public Ид_бронирования Id { get; }
        public Ид_клиента CustomerId { get; }
        public Ид_оборудования EquipmentId { get; }
        public Дата_начала StartDate { get; private set; }
        public Дата_окончания EndDate { get; private set; }
        public Сумма_залога DepositAmount { get; }
        public Domain.Клиент.СтатусБронирования Статус { get; private set; }
        public Domain.Клиент.Клиент Клиент { get; }

        /// <summary>
        /// Завершает бронирование и рассчитывает износ оборудования
        /// </summary>
        /// <param name="equipment">Оборудование для расчета износа</param>
        /// <returns>Результат завершения с информацией об износе</returns>
        public (bool completed, double wearIncrease, bool equipmentDeactivated) Complete(Domain.Equipment.Equipment equipment)
        {
            if (Статус is СтатусБронированияЗавершено or СтатусБронированияОтменено)
                return (false, 0, false);

            var bookingDuration = (DateTime.Now - StartDate.Date.ToDateTime(TimeOnly.MinValue)).TotalDays;
            var wearIncrease = Math.Max(1, bookingDuration * 0.5);

            equipment.IncreaseWear(wearIncrease);

            var equipmentDeactivated = !equipment.IsActive;

            Статус = new СтатусБронированияЗавершено();
            EndDate = Дата_окончания.Create(DateOnly.FromDateTime(DateTime.Now), StartDate);

            return (true, wearIncrease, equipmentDeactivated);
        }
    }
}
