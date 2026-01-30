namespace Domain.Booking
{
    public class Ид_оборудования
    {
        public Guid Id { get; }

        private Ид_оборудования(Guid id)
        {
            Id = id;
        }

        public static Ид_оборудования Create(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Идентификатор оборудования не может быть пустым.");
            
            return new Ид_оборудования(id);
        }
    }
}
