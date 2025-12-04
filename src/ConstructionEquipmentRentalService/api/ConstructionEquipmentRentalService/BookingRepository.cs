using Domain.Бронирование.Contracts;
using Domain.Booking;

namespace RentalService.Infrastructure.Repositories
{
    public class BookingRepository : ICanStoreBooking
    {
        private static readonly List<Бронирование> _storage = new();

        public Task SaveBooking(Бронирование booking)
        {
            var existing = _storage.FirstOrDefault(b => b.Id.Id == booking.Id.Id);
            if (existing != null)
            {
                _storage.Remove(existing);
            }
            _storage.Add(booking);

            return Task.CompletedTask;
        }
    }
}
