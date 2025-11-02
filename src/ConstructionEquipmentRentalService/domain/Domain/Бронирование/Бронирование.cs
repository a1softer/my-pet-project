using Domain.Клиент;

namespace Domain.Booking
{
    public class Бронирование
    {
        private readonly Domain.Equipment.Equipment _equipment;

        public Бронирование(Ид_бронирования id, Ид_клиента customerId, Ид_оборудования equipmentId, Дата_начала startDate, Дата_окончания endDate, Сумма_залога depositAmount, Domain.Клиент.СтатусБронирования статус, Domain.Клиент.Клиент клиент, Domain.Equipment.Equipment equipment)
        {
            Id = id;
            CustomerId = customerId;
            EquipmentId = equipmentId;
            StartDate = startDate;
            EndDate = endDate;
            DepositAmount = depositAmount;
            Статус = статус;
            Клиент = клиент;
            _equipment = equipment ?? throw new ArgumentNullException(nameof(equipment));
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
        /// Завершает бронирование и применяет износ к оборудованию
        /// </summary>
        /// <returns>Было ли оборудование деактивировано из-за износа</returns>
        public bool Complete()
        {
            if (Статус is СтатусБронированияЗавершено or СтатусБронированияОтменено)
                throw new InvalidOperationException("Бронирование уже завершено или отменено");

            var bookingDuration = (DateTime.Now - StartDate.Date.ToDateTime(TimeOnly.MinValue)).TotalDays;

            _equipment.ApplyBookingWear(bookingDuration);

            Статус = new СтатусБронированияЗавершено();
            EndDate = Дата_окончания.Create(DateOnly.FromDateTime(DateTime.Now), StartDate);

            return !_equipment.IsActive;
        }

        /// <summary>
        /// Проверяет, можно ли создать бронирование для этого оборудования
        /// </summary>
        public bool CanBeCreated()
        {
            return _equipment.CanBeBooked() && Статус is not СтатусБронированияОтменено;
        }
    }
}
