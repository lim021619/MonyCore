using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MonyCore.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddInc : ContentPage
    {
        List<Frame> Frames { get; set; }
        double HeightDivHisytory { get; set; }
        bool toggleHistory = true;
        public AddInc()
        {
            InitializeComponent();
            Frames = new List<Frame>();
       
        }

        protected async override void OnAppearing()
        {
            await ClearFrames();
          //CountCon.Text = MainPage.CountConsumption.ToString();
        }

        async Task InitWinData()
        {
            using (Context.Context context = new Context.Context())
            {
                Model.Many many = context.Manies.
                    Include(i => i.Consumptions).
                    FirstOrDefault(i => i.id == 1);

                List<Model.Consumption> con = many.Consumptions;

                con.Reverse();
                //MainPage.CountConsumption = inc.Count;
                foreach (var item in con)
                {
                    Frame frame = new Frame();
                    frame.HorizontalOptions = LayoutOptions.FillAndExpand;
                    frame.CornerRadius = 10f;
                    
                    frame.Margin = new Thickness(0);
                    frame.HasShadow = true;


                    Label label = new Label();
                    
                    label.TextColor = Color.Beige;
                    label.HorizontalOptions = LayoutOptions.FillAndExpand;
                    label.Text = $" Сумма - {item.Summ.ToString("c")} Дата - {item.Data} Время - {item.Time}";
                    label.FontSize = 18;
                    label.TextColor = Color.Gray;
                    frame.Content = label;

                    History.Children.Add(frame);
                    Frames.Add(frame);
                }

                CountCon.Text = con.Count.ToString();


            }
        }


        async Task  ClearFrames()
        {
            if (Frames.Count != 0)
            {
                foreach (var item in Frames)
                {
                    History.Children.Remove(item);
                }

                Frames.Clear();
            }

            InitWinData();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            using (Context.Context context = new Context.Context())
            {
                Model.Consumption consumption = new Model.Consumption(Convert.ToDecimal(Number.Text));

                Model.Many many = context.Manies.FirstOrDefault(m => m.id == 1);

                if (many != null)
                {
                    try
                    {
                        many.AddConsumptions(consumption);
                        context.SaveChanges();
                        CountCon.Text = Convert.ToString(Convert.ToInt32(CountCon.Text) + 1);
                        await ClearFrames();
                    }
                    catch (Exception ex)
                    {

                    }

                   


                }
            }
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            if (toggleHistory)
            {
                HeightDivHisytory = DivHistory.Height;
                DivHistory.HeightRequest = 0;
                toggleHistory = false;
            }
            else
            {

                DivHistory.HeightRequest = HeightDivHisytory;
                toggleHistory = true;
            }
        }
    }
}