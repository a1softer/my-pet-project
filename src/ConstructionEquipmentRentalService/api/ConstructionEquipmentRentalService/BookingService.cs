using Domain.Бронирование.Contracts;
using Domain.Клиент.Contracts;
using Domain.Оборудование.Contracts;

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
            return "Dependency Injection работает! BookingService создан.";
        }
    }
}
