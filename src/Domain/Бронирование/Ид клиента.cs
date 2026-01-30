namespace Domain.Booking
{
    public class Ид_клиента
    {
        public Guid Id { get; }

        private Ид_клиента(Guid id)
        {
            Id = id;
        }

        public static Ид_клиента Create(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Идентификатор клиента не может быть пустым.");

            return new Ид_клиента(id);
        }
    }
}
