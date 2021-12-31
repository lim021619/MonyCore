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
    public partial class OpenArhive : ContentPage
    {
        Model.Many Many { get; set; }   
        
        public OpenArhive()
        {
            InitPage();
        }

        public OpenArhive(Model.Many many)
        {

            InitProp();
            Many = many;
            InitPage();
            
        }


        /// <summary>
        /// Инициализация страницы
        /// </summary>
        void InitPage()
        {
            InitializeComponent();
            Title = $"Aрхив №{Many.GetNumberArhive()}";
        }


      /// <summary>
      /// Инициализация свойст класса
      /// </summary>
        void InitProp()
        {
            Many = new Model.Many();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModels.ViewArhiveOpen viewArhive = new ViewModels.ViewArhiveOpen(Many);
            
            foreach (var item in viewArhive.GetListRowInfo())
            {
                Root.Children.Add(item);
            }


        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            using (Context.Context context = new Context.Context())
            {
                Model.Many many = context.Manies.Include(i => i.Incoms).Include(i => i.Consumptions).FirstOrDefault(i => i.id == Many.id);
                context.Incoms.RemoveRange(many.Incoms);
                context.Consumptions.RemoveRange(many.Consumptions);
                context.Manies.Remove(many);
                context.SaveChanges();
            }
            await Navigation.PopAsync();
        }
    }
}