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
        private int numberInArhive;

        public event EventHandler Click;


        /// <summary>
        /// Доходы
        /// </summary>
        public decimal AllMany { get; set; }

        /// <summary>
        /// Расходы
        /// </summary>
        public decimal AllManyConsumption { get; set; }

        public List<Incom> Incoms { get; private set; }

        public List<Consumption> Consumptions { get; private set; }

        public int id { get; set; }

        public int NumberInArhive { get => numberInArhive; set
            {
                numberInArhive = value;
                if (numberInArhive != 0 )
                {
                    DateArhive = DateTime.Now.ToString();
                }
            }
        }

        /// <summary>
        /// Дата архивации
        /// </summary>
        public string DateArhive { get; set; }
        /// <summary>
        /// Дата начала ведения
        /// </summary>
        public string DateCreate { get; set; }
        /// <summary>
        /// Количество записей по доходам
        /// </summary>
        public int CountIncom { get; set; }
        /// <summary>
        /// Количество записей расходов
        /// </summary>
        public int CountCounsuption { get; set; }

        public Many()
        {
            Incoms = new List<Incom>();
            Consumptions = new List<Consumption>();
            DateCreate = DateTime.Now.ToString();
        }

        public void AddIncoms(Incom incom)
        {
            AllMany += incom.Summ;
            Incoms.Add(incom);
            CountIncom++;
        }
        public void AddConsumptions(Consumption consumption)
        {

            AllManyConsumption += consumption.Summ;
            Consumptions.Add(consumption);
            CountCounsuption++;

        }

        public void AddConsumptionsRange(IEnumerable<Consumption> listCon)
        {
            foreach (var item in listCon)
            {
                AddConsumptions(item);
            }
        }

        public void AddIncomRange(IEnumerable<Incom> listInc)
        {
            foreach (var item in listInc)
            {
                AddIncoms(item);
            }
        }

        public int GetNumberArhive()
        {
            return NumberInArhive - 1;
        }

        

    }
}
