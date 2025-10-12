namespace Domain.Shared
{
    public abstract class Enumeration<TEnum>
        where TEnum : Enumeration<TEnum>
    {
        private static readonly Dictionary<int, Func<TEnum>> _keyFactories = FetchKeyFactories();
        private static readonly Dictionary<string, Func<TEnum>> _nameFactories = FetchNameFactories();

        public int Key { get; }
        public string Name { get; }

        protected Enumeration(int key, string name)
        {
            Key = key;
            Name = name;
        }

        public static TEnum FromKey(int key)
        {
            return _keyFactories.TryGetValue(key, out Func<TEnum>? factory)
                ? factory()
                : throw new ArgumentException($"Не поддерживаемый ключ перечисления: {key}");
        }

        public static TEnum FromName(string name)
        {
            return _nameFactories.TryGetValue(name, out Func<TEnum>? factory) 
                ? factory() 
                : throw new ArgumentException($"Не поддерживаемое название перечисления: {name}");
        }

        public static IReadOnlyCollection<TEnum> GetAll()
        {
            return _keyFactories.Values.Select(factory => factory()).ToList().AsReadOnly();
        }

        private static Dictionary<int, Func<TEnum>> FetchKeyFactories()
        {
            var factories = new Dictionary<int, Func<TEnum>>();
            var types = FetchTypes();

            foreach ( var type in types)
            {
                var factory = CreateFactoryFromConstructor(type);
                var enumeration = factory();
                factories.Add(enumeration.Key, factory);
            }

            return factories;
        }

        private static Dictionary<string, Func<TEnum>> FetchNameFactories()
        {
            var factories = new Dictionary<string, Func<TEnum>>();
            var types = FetchTypes();

            foreach (var type in types)
            {
                var factory = CreateFactoryFromConstructor(type);
                var enumeration = factory();
                factories.Add(enumeration.Name, factory);
            }
            
            return factories;
        }

        private static Func<TEnum> CreateFactoryFromConstructor(Type type)
        {
            var constructor = type.GetConstructors()
                .First(c => c.GetParameters().Length == 0);

            Func<TEnum> factory = () => (TEnum)constructor.Invoke(null);
            return factory;
        }

        private static IEnumerable<Type> FetchTypes()
        {
            Type enumType = typeof(TEnum);
            return enumType.Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(enumType) && !t.IsAbstract);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Enumeration<TEnum> other) return false;
            return Key == other.Key && Name == other.Name;
        }

        public override int GetHashCode() => HashCode.Combine(Key, Name);
    }
}
