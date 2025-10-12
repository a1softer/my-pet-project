using Domain.Shared;

namespace Domain.Equipment
{
    public abstract class ТипОборудования: Enumeration<ТипОборудования>
    {
        protected ТипОборудования(int key, string name)
            : base(key, name) { }

        public abstract decimal РассчитатьКоэффициентСтоимости();
        public abstract int МаксимальныйСрокАрендыВДнях();
    }

    public sealed class ТипОборудованияЭкскаватор : ТипОборудования
    {
        public ТипОборудованияЭкскаватор()
            : base(1, "Экскаватор") { }

        public override decimal РассчитатьКоэффициентСтоимости() => 1.5m;
        public override int МаксимальныйСрокАрендыВДнях() => 30;
    }

    public sealed class ТипОборудованияКран : ТипОборудования
    {
        public ТипОборудованияКран()
            : base(2, "Кран") { }

        public override decimal РассчитатьКоэффициентСтоимости() => 2.0m;
        public override int МаксимальныйСрокАрендыВДнях() => 14;
    }

    public sealed class ТипОборудованияБетономешалка : ТипОборудования
    {
        public ТипОборудованияБетономешалка()
            : base(3, "Бетономешалка") { }

        public override decimal РассчитатьКоэффициентСтоимости() => 1.2m;
        public override int МаксимальныйСрокАрендыВДнях() => 60;
    }

    public sealed class ТипОборудованияБульдозер : ТипОборудования
    {
        public ТипОборудованияБульдозер()
            : base(4, "Бульдозер") { }

        public override decimal РассчитатьКоэффициентСтоимости() => 1.8m;
        public override int МаксимальныйСрокАрендыВДнях() => 45;
    }
}
