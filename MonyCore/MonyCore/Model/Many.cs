using MonyCore.Interfases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonyCore.Model
{

    /// <summary>
    /// Класс представляющий сводку финансов
    /// </summary>
   public class Many : IItemForContext
    {
        public event EventHandler Click;


        /// <summary>
        /// Доходы
        /// </summary>
        public decimal AllMany { get; set; }

        /// <summary>
        /// Расходы
        /// </summary>
        public decimal AllManyIncom { get; set; }

        public List<Incom> Incoms { get; private set; }

        public List<Consumption> Consumptions { get; private set; }

        public int id { get; set; }

        public int NumberInArhive { get; set; }

        public Many()
        {
            Incoms = new List<Incom>();
            Consumptions = new List<Consumption>();
        }

        public void AddIncoms(Incom incom)
        {
            AllMany += incom.Summ;
            Incoms.Add(incom);
        }
        public void AddConsumptions(Consumption consumption)
        {
            
            AllManyIncom += consumption.Summ;
            Consumptions.Add(consumption);

        }

    }
}
