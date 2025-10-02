namespace Domain.Booking
{
    public class Ид_бронирования
    {
        public Guid Id { get; }

        private Ид_бронирования(Guid id)
        {
            Id = id;
        }

        public static Ид_бронирования Create(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Идентификатор бронирования не может быть пустым.");
            return new Ид_бронирования(id);
        }

        public static Ид_бронирования CreateNew()
        {
            return new Ид_бронирования(Guid.NewGuid());
        }
    }
}
