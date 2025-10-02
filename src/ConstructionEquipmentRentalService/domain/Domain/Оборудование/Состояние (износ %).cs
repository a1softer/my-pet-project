﻿namespace Domain.Equipment
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
    }
}
