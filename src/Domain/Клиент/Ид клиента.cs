namespace Domain.Клиент
{
    public record Ид_клиента
    {
        public Guid Id { get; }

        public Ид_клиента()
        {
            Id = Guid.NewGuid();
        }

        private Ид_клиента(Guid id)
        {
            Id = id;
        }

        public static Ид_клиента Create(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Идентификатор клиента не может быть пустым");
            return new Ид_клиента(id);
        }

        public static Ид_клиента CreateNew()
        {
            return new Ид_клиента(Guid.NewGuid());
        }
    }
}
