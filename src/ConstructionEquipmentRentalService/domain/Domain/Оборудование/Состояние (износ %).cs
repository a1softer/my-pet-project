namespace Domain.Equipment
{
    public class StateProcent
    {
        public double Procent { get; }

        private StateProcent(double procent)
        {
            Procent = procent;
        }

        public static StateProcent Create(double procent)
        {
            if (procent < 0 || procent > 100)
                throw new ArgumentException("Процент износа должен быть в диапазоне от 0 до 100.");
            return new StateProcent(procent);
        }

        /// <summary>
        /// Создает новый объект с увеличенным износом
        /// </summary>
        /// <param name="amount">Количество для увеличения</param>
        /// <returns>Новый объект StateProcent</returns>
        public StateProcent Increase(double amount)
        {
            var newProcent = Procent + amount;
            return Create(newProcent);
        }
    }
}
