using MonyCore.Interfases;
using MonyCore.Logic;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonyCore.Model
{

    /// <summary>
    /// Класс представляющий расходы
    /// </summary>
    public class Consumption : IItemForContext, IFinObjecte
    {
        public int id { get ; set ; }
        public string Data { get; set; }
        public string Time { get; set; }
        public decimal Summ { get; set; }
        public Many Many { get; set; }

        public Consumption()
        {
            Data = DateTime.Now.ToLongDateString();
            Time = DateTime.Now.ToLongTimeString();
            Summ = 0;
        }

        public Consumption(decimal sum)
        {
            Data = DateTime.Now.ToLongDateString();
            Time = DateTime.Now.ToLongTimeString();
            Summ = sum;
        }

        public ViewData GetDataView()
        {
            return new ViewData { Text = $"Потрачено {Summ.ToString("c")}", Deteil = $"{Data} : {Time}" };
        }
    }
}
