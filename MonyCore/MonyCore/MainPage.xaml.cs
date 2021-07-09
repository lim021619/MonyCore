using MonyCore.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MonyCore
{
    public partial class MainPage : ContentPage
    {
        public Model.Many Many { get; set; }

        public MainPage()
        {
            InitializeComponent();
        }




        protected override void OnAppearing()
        {


            using (Context.Context context = new Context.Context())
            {
                bool one = false;
                bool two = false;
                decimal result = 0;
                Model.Many many = context.Manies.FirstOrDefault(m => m.id == 1);

                if (many != null)
                {
                    if (many.AllMany != 0)
                    {
                        ConsumptionText.Text = many.AllMany.ToString("c");
                        one = true;
                    }
                    if (many.AllManyIncom != 0)
                    {
                        IncomText.Text = many.AllManyIncom.ToString("c");
                        two = true;
                    }
                    if (one && two)
                    {
                        result = many.AllManyIncom - many.AllMany ;
                        AllManyNow.Text = "";
                        AllManyNow.Text = result.ToString("c");
                    }
                    else if(one)
                    {
                        AllManyNow.Text = many.AllMany.ToString("c");
                    }
                }
                else
                {
                    Model.Many many1 = new Model.Many();
                    context.Manies.Add(many1);
                    context.SaveChanges();
                }
            }

        }

        /// <summary>
        /// Проверка на символов.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTextChanged(object sender, TextChangedEventArgs e)
        { 
           //lets the Entry be empty
            if (string.IsNullOrEmpty(e.NewTextValue)) return;

            if (!double.TryParse(e.NewTextValue, out double value))
            {
                ((Entry)sender).Text = e.OldTextValue;
            }

            Context.Context context1 = new Context.Context();

            Model.Many many = context1.Manies.FirstOrDefault(m => m.id == 1);
        }

        private async void NewCon_Clicked(object sender, EventArgs e) => await Navigation.PushModalAsync(new AddCon());

        private async void NewInc_Clicked(object sender, EventArgs e) => await Navigation.PushModalAsync(new AddInc());
      
    }
}
