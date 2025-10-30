using Domain.Booking;
using Domain.Клиент;
using Domain.Equipment;
using Microsoft.AspNetCore.Mvc;
using RentalService.Common;

namespace RentalService.Presentation.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingsController : ControllerBase
    {
        private static readonly List<Бронирование> _bookingStorage = new();
        private static readonly List<Клиент> _clientStorage = new();
        private static readonly List<Equipment> _equipmentStorage = new();

        [HttpPost]
        public IActionResult CreateBooking([FromBody] CreateBookingRequest request)
        {
            try
            {
                var equipment = _equipmentStorage.FirstOrDefault(e => e.Id.Id == request.EquipmentId);
                if (equipment is null)
                    return NotFound(Envelope<BookingResponse>.Error("Оборудование не найдено"));

                var client = _clientStorage.FirstOrDefault(c => c.Id.Id == request.ClientId);
                if (client is null)
                    return NotFound(Envelope<BookingResponse>.Error("Клиент не найден"));

                var hasActiveBooking = _bookingStorage.Any(b =>
                    b.EquipmentId.Id == request.EquipmentId &&
                    b.Статус is not СтатусБронированияОтменено and not СтатусБронированияЗавершено);

                if (hasActiveBooking)
                    return BadRequest(Envelope<BookingResponse>.Error("Оборудование уже забронировано"));

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

                _bookingStorage.Add(booking);

                var response = new BookingResponse(
                    booking.Id.Id,
                    booking.EquipmentId.Id,
                    booking.CustomerId.Id,
                    booking.StartDate.Date,
                    booking.EndDate.Date,
                    booking.DepositAmount.Amount,
                    booking.Статус.Name
                );

                return Ok(Envelope<BookingResponse>.Ok(response));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(Envelope<BookingResponse>.Error(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, Envelope<BookingResponse>.Error("Внутренняя ошибка сервера"));
            }
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var booking = _bookingStorage.FirstOrDefault(b => b.Id.Id == id);
                if (booking is null)
                    return NotFound(Envelope<BookingResponse>.Error("Бронирование не найдено"));

                var response = new BookingResponse(
                    booking.Id.Id,
                    booking.EquipmentId.Id,
                    booking.CustomerId.Id,
                    booking.StartDate.Date,
                    booking.EndDate.Date,
                    booking.DepositAmount.Amount,
                    booking.Статус.Name
                );

                return Ok(Envelope<BookingResponse>.Ok(response));
            }
            catch (Exception)
            {
                return StatusCode(500, Envelope<BookingResponse>.Error("Внутренняя ошибка сервера"));
            }
        }

        // Окончание бронирования:
        [HttpPost("{id:guid}/complete")]
        public IActionResult CompleteBooking(Guid id)
        {
            try
            {
                var booking = _bookingStorage.FirstOrDefault(b => b.Id.Id == id);
                if (booking is null)
                    return NotFound(Envelope<BookingResponse>.Error("Бронирование не найдено"));

                if (booking.Статус is СтатусБронированияЗавершено or СтатусБронированияОтменено)
                    return BadRequest(Envelope<BookingResponse>.Error("Бронирование уже завершено или отменено"));

                var response = new BookingResponse(
                    booking.Id.Id,
                    booking.EquipmentId.Id,
                    booking.CustomerId.Id,
                    booking.StartDate.Date,
                    DateOnly.FromDateTime(DateTime.Now),
                    booking.DepositAmount.Amount,
                    "Завершено"
                );

                return Ok(Envelope<BookingResponse>.Ok(response));
            }
            catch (Exception)
            {
                return StatusCode(500, Envelope<BookingResponse>.Error("Внутренняя ошибка сервера"));
            }
        }
    }

    public record CreateBookingRequest(Guid EquipmentId, Guid ClientId);

    public record BookingResponse(
        Guid Id,
        Guid EquipmentId,
        Guid ClientId,
        DateOnly StartDate,
        DateOnly EndDate,
        decimal DepositAmount,
        string Status
    );
}
