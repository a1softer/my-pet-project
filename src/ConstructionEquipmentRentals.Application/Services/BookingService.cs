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

        public async Task<string> CreateBookingAsync(Guid equipmentId, Guid clientId)
        {
            try
            {
                // 1. Получаем оборудование через репозиторий
                GetEquipmentOptions equipmentOptions = new GetEquipmentOptions(Id: equipmentId);
                var equipment = await _equipmentStore.Получитьоборудование(equipmentOptions);

                if (equipment is null)
                    throw new ArgumentException($"Оборудование с ID {equipmentId} не найдено");

                // 2. Получаем клиента через репозиторий
                var clientOptions = new GetClientOptions(Id: clientId);
                var client = await _clientStore.Получитьклиента(clientOptions);

                if (client is null)
                    throw new ArgumentException($"Клиент с ID {clientId} не найден");

                // 3. Проверяем возможность бронирования
                if (!equipment.CanBeBooked())
                    throw new InvalidOperationException("Оборудование не может быть забронировано");

                // 4. Создаем бронирование
                var booking = CreateBooking(equipment, client);

                // 5. Проверяем возможность создания
                if (!booking.CanBeCreated(equipment))
                    throw new InvalidOperationException("Невозможно создать бронирование");

                // 6. Сохраняем через репозиторий
                await _bookingStore.SaveBooking(booking);

                return $"Бронирование успешно создано. ID: {booking.Id.Id}";
            }
            catch (ArgumentException ex)
            {
                return $"Ошибка: {ex.Message}";
            }
            catch (InvalidOperationException ex)
            {
                return $"Ошибка: {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"Внутренняя ошибка: {ex.Message}";
            }
        }

        private Бронирование CreateBooking(Equipment equipment, Клиент client)
        {
            var bookingId = Ид_бронирования.CreateNew();
            var equipmentId = Ид_оборудования.Create(equipment.Id.Id);
            var clientIdDomain = Domain.Booking.Ид_клиента.Create(client.Id.Id);
            var startDate = Дата_начала.CreateToday();
            var endDate = Дата_окончания.Create(startDate.Date.AddDays(7), startDate);

            var rentalCostPerDay = equipment.RentalCostPerHour.Value * 24;
            var totalRentalCost = rentalCostPerDay * 7;
            var depositAmount = Сумма_залога.Create(totalRentalCost * 0.3m, rentalCostPerDay, 7);

            var status = new СтатусБронированияПодтверждено();

            return new Бронирование(
                bookingId,
                clientIdDomain,
                equipmentId,
                startDate,
                endDate,
                depositAmount,
                status,
                client
            );
        }
    }
}