using Domain.Shared;

namespace Domain.Клиент
{
    public abstract class СтатусБронирования : Enumeration<СтатусБронирования>
    {
        protected СтатусБронирования(int key, string name)
            : base(key, name) { }

        public abstract bool МожноОтменить();
        public abstract bool МожноПодтвердить();
        public abstract string ПолучитьЦвет();
    }

    public sealed class СтатусБронированияВОжидании : СтатусБронирования
    {
        public СтатусБронированияВОжидании()
            : base(1, "В ожидании") { }

        public override bool МожноОтменить() => true;
        public override bool МожноПодтвердить() => true;
        public override string ПолучитьЦвет() => "желтый";
    }

    public sealed class СтатусБронированияПодтверждено : СтатусБронирования
    {
        public СтатусБронированияПодтверждено()
            : base(2, "Подтверждено") { }

        public override bool МожноОтменить() => true;
        public override bool МожноПодтвердить() => false;
        public override string ПолучитьЦвет() => "зеленый";
    }

    public sealed class СтатусБронированияОтменено : СтатусБронирования
    {
        public СтатусБронированияОтменено()
            : base(3, "Отменено") { }

        public override bool МожноОтменить() => false;
        public override bool МожноПодтвердить() => false;
        public override string ПолучитьЦвет() => "красный";
    }

    public sealed class СтатусБронированияЗавершено : СтатусБронирования
    {
        public СтатусБронированияЗавершено()
            : base(4, "Завершено") { }

        public override bool МожноОтменить() => false;
        public override bool МожноПодтвердить() => false;
        public override string ПолучитьЦвет() => "синий";
    }
}
