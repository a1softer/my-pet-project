namespace Domain.Booking
{
    public class Бронирование
    {
        public Бронирование(Ид_бронирования id, Ид_клиента customerId, Ид_оборудования equipmentId, Дата_начала startDate, Дата_окончания endDate, Сумма_залога depositAmount, Domain.Клиент.СтатусБронирования статус)
        {
            Id = id;
            CustomerId = customerId;
            EquipmentId = equipmentId;
            StartDate = startDate;
            EndDate = endDate;
            DepositAmount = depositAmount;
            Статус = статус;
        }

        public Ид_бронирования Id { get; }
        public Ид_клиента CustomerId { get; }
        public Ид_оборудования EquipmentId { get; }
        public Дата_начала StartDate { get; private set; }
        public Дата_окончания EndDate { get; private set; }
        public Сумма_залога DepositAmount { get; }
        public Domain.Клиент.СтатусБронирования Статус { get; private set; }
    }
}
