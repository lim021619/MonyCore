using MonyCore.Interfases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonyCore.Model
{
    public class Consumption : IItemForContext
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
    }
}
