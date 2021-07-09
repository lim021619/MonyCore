using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MonyCore.Model
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HelloPage : ContentPage
    {
        public Context.Context Context { get; set; }
        public HelloPage()
        {
            InitializeComponent();
            Loadtext.IsVisible = false;
            Context = new Context.Context();
        }

        private async void Button_Clicked(object sender, EventArgs e) {



            await Navigation.PushModalAsync(new MainPage());
            //Loadtext.IsVisible = true;
            //string row = load.Text;
            

            //for (int i = 0; true; i++)
            //{
            //    if (i == 3)
            //    {
            //        load.Text = row;
            //        i = 0;

            //    }
            //    Thread.Sleep(500);

            //    load.Text += ".";
            //}
            
            
        }
        
    }
}