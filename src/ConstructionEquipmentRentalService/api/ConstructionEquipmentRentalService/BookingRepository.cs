using Domain.Бронирование.Contracts;
using Domain.Booking;

namespace Infrastructure.Repositories
{
    public class BookingRepository : ICanStoreBooking
    {
        private static readonly List<Бронирование> _storage = new();

        public Task SaveBooking(Бронирование booking)
        {
            _storage.Add(booking);
            Console.WriteLine($"Бронирование сохранено: {booking.Id.Id}");
            return Task.CompletedTask;
        }
    }
}
