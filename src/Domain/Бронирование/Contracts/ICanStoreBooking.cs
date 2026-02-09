namespace Domain.Бронирование.Contracts
{
    public interface ICanStoreBooking
    {
        Task SaveBooking(Domain.Booking.Бронирование booking);
    }
}
