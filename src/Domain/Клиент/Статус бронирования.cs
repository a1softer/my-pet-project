using Domain.Shared;

namespace Domain.Клиент
{
    public class СтатусБронирования : Enumeration<СтатусБронирования>
    {
        protected СтатусБронирования(int key, string name)
            : base(key, name) { }

        public virtual bool МожноОтменить()
        {
            return false;
        }
        public virtual bool МожноПодтвердить()
        {
            return false;
        }
        public virtual string ПолучитьЦвет()
        {
            return string.Empty;
        }
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
