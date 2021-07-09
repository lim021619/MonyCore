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

        public AddInc()
        {
            InitializeComponent();
            Frames = new List<Frame>();
        }

        protected async override void OnAppearing()
        {
            await ClearFrames();
        }

        async Task InitWinData()
        {
            using (Context.Context context = new Context.Context())
            {

                foreach (var item in context.Incoms.ToList())
                {
                    Frame frame = new Frame();
                    frame.HorizontalOptions = LayoutOptions.FillAndExpand;
                    frame.CornerRadius = 10f;
                    frame.BackgroundColor = Color.FromRgb(60, 213, 150);
                    

                    Label label = new Label();
                    
                    label.TextColor = Color.Beige;
                    label.HorizontalOptions = LayoutOptions.FillAndExpand;
                    label.Text = $" Сумма - {item.Summ.ToString("c")} Дата - {item.Data} Время - {item.Time}";
                    label.FontSize = 18;
                    frame.Content = label;

                    History.Children.Add(frame);
                    Frames.Add(frame);
                }


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
                Model.Incom incom = new Model.Incom(Convert.ToDecimal(Number.Text));

                Model.Many many = context.Manies.FirstOrDefault(m=>m.id == 1);
                if (many != null)
                {
                    many.AddIncoms(incom);
                    context.SaveChanges();

                    await ClearFrames();
                }
             
            }
        }
    }
}