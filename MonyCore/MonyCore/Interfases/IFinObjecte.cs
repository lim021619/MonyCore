using System;
using System.Collections.Generic;
using System.Text;

namespace MonyCore.Interfases
{
    /// <summary>
    /// Представляет интерфейс вида операции
    /// </summary>
    public interface IFinObjecte
    {

        /// <summary>
        /// Представляет дату оперции
        /// </summary>
        public string Data { get; set; }
        /// <summary>
        /// Представляет время оперции
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// Представляет сумму операции
        /// </summary>
        public decimal Summ { get; set; }

        public Logic.ViewData GetDataView();

    }
}
