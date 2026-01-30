using Domain.Booking;
using Domain.Клиент;
using Domain.Equipment;
using Microsoft.AspNetCore.Mvc;
using RentalService.Common;

namespace RentalService.Presentation.Controllers
{
    /// <summary>
    /// Контроллер для управления бронированиями
    /// </summary>
    [ApiController]
    [Route("api/bookings")]
    public class BookingsController : ControllerBase
    {
        private static readonly List<Бронирование> _bookingStorage = new();
        private static readonly List<Клиент> _clientStorage = new();
        private static readonly List<Equipment> _equipmentStorage = new();

        [HttpPost("di-test")]
        public IResult TestDependencyInjection()
        {
            return Results.Ok(new
            {
                message = "Dependency Injection готов к использованию",
                implementedInterfaces = new[]
                {
            "ICanStoreBooking → BookingRepository",
            "ICanStoreClient → ClientRepository",
            "ICanStoreEquipment → EquipmentRepository"
        },
                applicationService = "BookingService создан в Application слое"
            });
        }

        /// <summary>
        /// Создает новое бронирование
        /// </summary>
        /// <param name="request">Данные для создания бронирования</param>
        /// <returns>Созданное бронирование</returns>
        [HttpPost]
        public IResult CreateBooking([FromBody] CreateBookingRequest request)
        {
            try
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
                );

                return Envelope<BookingResponse>.Ok(response);
            }
            catch (ArgumentException ex)
            {
                return Envelope<BookingResponse>.BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return Envelope<BookingResponse>.InternalError("Внутренняя ошибка сервера");
            }
        }

        /// <summary>
        /// Получает бронирование по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор бронирования</param>
        /// <returns>Бронирование</returns>
        [HttpGet("{id:guid}")]
        public IResult GetById(Guid id)
        {
            try
            {
                var booking = _bookingStorage.FirstOrDefault(b => b.Id.Id == id);
                if (booking is null)
                    return Envelope<BookingResponse>.NotFound("Бронирование не найдено");

                var equipment = _equipmentStorage.FirstOrDefault(e => e.Id.Id == booking.EquipmentId.Id);
                var equipmentIsActive = equipment?.IsActive ?? false;

                var response = new BookingResponse(
                    booking.Id.Id,
                    booking.EquipmentId.Id,
                    booking.CustomerId.Id,
                    booking.StartDate.Date,
                    booking.EndDate.Date,
                    booking.DepositAmount.Amount,
                    booking.Статус.Name,
                    equipmentIsActive
                );

                return Envelope<BookingResponse>.Ok(response);
            }
            catch (Exception)
            {
                return Envelope<BookingResponse>.InternalError("Внутренняя ошибка сервера");
            }
        }

        /// <summary>
        /// Завершает бронирование и рассчитывает износ оборудования
        /// </summary>
        /// <param name="id">Идентификатор бронирования</param>
        /// <returns>Завершенное бронирование</returns>
        [HttpPost("{id:guid}/complete")]
        public IResult CompleteBooking(Guid id)
        {
            try
            {
                var booking = _bookingStorage.FirstOrDefault(b => b.Id.Id == id);
                if (booking is null)
                    return Envelope<BookingResponse>.NotFound("Бронирование не найдено");

                var equipment = _equipmentStorage.FirstOrDefault(e => e.Id.Id == booking.EquipmentId.Id);
                if (equipment is null)
                    return Envelope<BookingResponse>.NotFound("Оборудование не найдено");

                var equipmentDeactivated = booking.Complete(equipment);

                var response = new BookingResponse(
                    booking.Id.Id,
                    booking.EquipmentId.Id,
                    booking.CustomerId.Id,
                    booking.StartDate.Date,
                    booking.EndDate.Date,
                    booking.DepositAmount.Amount,
                    booking.Статус.Name,
                    equipment.IsActive
                );

                return Envelope<BookingResponse>.Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return Envelope<BookingResponse>.BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return Envelope<BookingResponse>.InternalError("Внутренняя ошибка сервера");
            }
        }
    }

    /// <summary>
    /// Запрос на создание бронирования
    /// </summary>
    /// <param name="EquipmentId">Идентификатор оборудования</param>
    /// <param name="ClientId">Идентификатор клиента</param>
    public record CreateBookingRequest(Guid EquipmentId, Guid ClientId);

    /// <summary>
    /// Ответ с информацией о бронировании
    /// </summary>
    /// <param name="Id">Идентификатор</param>
    /// <param name="EquipmentId">Идентификатор оборудования</param>
    /// <param name="ClientId">Идентификатор клиента</param>
    /// <param name="StartDate">Дата начала</param>
    /// <param name="EndDate">Дата окончания</param>
    /// <param name="DepositAmount">Сумма залога</param>
    /// <param name="Status">Статус</param>
    /// <param name="EquipmentIsActive">Статус активности оборудования</param>
    public record BookingResponse(
        Guid Id,
        Guid EquipmentId,
        Guid ClientId,
        DateOnly StartDate,
        DateOnly EndDate,
        decimal DepositAmount,
        string Status,
        bool EquipmentIsActive = true
    );
}
