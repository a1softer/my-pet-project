using Domain.Бронирование.Contracts;
using Domain.Клиент.Contracts;
using Domain.Оборудование.Contracts;
using Domain.Booking;
using Domain.Клиент;
using Domain.Equipment;

namespace Application.Services
{
    public class BookingService
    {
        private readonly ICanStoreBooking _bookingStore;
        private readonly ICanStoreClient _clientStore;
        private readonly ICanStoreEquipment _equipmentStore;

        public BookingService(
            ICanStoreBooking bookingStore,
            ICanStoreClient clientStore,
            ICanStoreEquipment equipmentStore)
        {
            _bookingStore = bookingStore;
            _clientStore = clientStore;
            _equipmentStore = equipmentStore;
        }

        public async Task<string> CreateBookingDemo(Guid equipmentId, Guid clientId)
        {
            var equipment = _equipmentStorage.FirstOrDefault(e => e.Id.Id == request.EquipmentId);
            var client = _clientStorage.FirstOrDefault(c => c.Id.Id == request.ClientId);

            if (equipment is null)
                return Envelope<BookingResponse>.NotFound("Оборудование не найдено");
            if (client is null)
                return Envelope<BookingResponse>.NotFound("Клиент не найден");

            if (!equipment.CanBeBooked())
                return Envelope<BookingResponse>.Conflict("Оборудование не может быть забронировано (изношено или неактивно)");

            var hasActiveBooking = _bookingStorage.Any(b =>
                b.EquipmentId.Id == request.EquipmentId &&
                b.Статус is not СтатусБронированияОтменено and not СтатусБронированияЗавершено);

            if (hasActiveBooking)
                return Envelope<BookingResponse>.Conflict("Оборудование уже забронировано");

            var bookingId = Ид_бронирования.CreateNew();
            var equipmentId = Ид_оборудования.Create(request.EquipmentId);
            var clientId = Domain.Booking.Ид_клиента.Create(request.ClientId);
            var startDate = Дата_начала.CreateToday();
            var endDate = Дата_окончания.Create(startDate.Date.AddDays(7), startDate);

            var rentalCostPerDay = equipment.RentalCostPerHour.Value * 24;
            var totalRentalCost = rentalCostPerDay * 7;
            var depositAmount = Сумма_залога.Create(totalRentalCost * 0.3m, rentalCostPerDay, 7);

            var status = new СтатусБронированияПодтверждено();

            var booking = new Бронирование(bookingId, clientId, equipmentId, startDate, endDate, depositAmount, status, client);

            if (!booking.CanBeCreated(equipment))
                return Envelope<BookingResponse>.Conflict("Невозможно создать бронирование для данного оборудования");

            _bookingStorage.Add(booking);

            var response = new BookingResponse(
                booking.Id.Id,
                booking.EquipmentId.Id,
                booking.CustomerId.Id,
                booking.StartDate.Date,
                booking.EndDate.Date,
                booking.DepositAmount.Amount,
                booking.Статус.Name,
                equipment.IsActive
        }
    }
}
