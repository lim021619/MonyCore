using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MonyCore.ViewModels
{
    public class ViewArhiveOpen
    {
        public Model.Many Many { get; set; }

        public ViewArhiveOpen(Model.Many many)
        {
            if (many != null)
            {
                Many = many;
            }
            else
            {
                throw new ArgumentException("Параметр Many равен null");
            }
        }

        /// <summary>
        /// Получает писок всех параметров объкта Many
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetParameters()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            foreach (var item in GetStateProp())
            {
                parameters.Add(item.Key, item.Value);
            }

            return parameters;
        }

        
        /// <summary>
        /// Получает список основых свойств объекта Many
        /// </summary>
        /// <returns></returns>
        Dictionary<string, string> GetStateProp()
        {
            Dictionary<string, string> stateProp = new Dictionary<string, string>();

            stateProp.Add("Средств на момент архивации : ", $"{(Many.AllMany - Many.AllManyConsumption).ToString("c")}");
            stateProp.Add("Доходов", $"{Many.AllMany.ToString("c")}");
            stateProp.Add("Расходы", $"{Many.AllManyConsumption.ToString("c")}");
            stateProp.Add("Количества записей доходов", Many.CountIncom.ToString());
            stateProp.Add("Количества записей расходов", Many.CountCounsuption.ToString());
            stateProp.Add("Начало ведения - ", Many.DateCreate);
            stateProp.Add("Окончание ведения - ", Many.DateArhive);
            
            return stateProp;
        }
        /// <summary>
        /// Формирует форму вывода инфорации типа имя - значение
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public StackLayout RowInfo(string name, string value)
        {
            StackLayout cont = new  StackLayout();
            cont.Orientation = StackOrientation.Horizontal;
            
            Label labelName = new Label() { Text = name};

            Label labelValue = new Label() { Text = value };

            labelName.FontSize = labelValue.FontSize = 16;
            labelValue.FontAttributes = FontAttributes.Bold;

            cont.Children.Add(labelName);
            cont.Children.Add(labelValue);

            return cont;
        }


        /// <summary>
        /// Возвращает список форм объекта типа Many
        /// </summary>
        /// <returns></returns>
        public List<StackLayout> GetListRowInfo()
        {
            List<StackLayout> list = new List<StackLayout>();


            foreach (var item in GetParameters())
            {
                list.Add(RowInfo(item.Key, item.Value));
            }


            return list;

        }
    }
}
