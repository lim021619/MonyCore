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
    public partial class ListArhive : ContentPage
    {
        public object lokingdb = new object();

        public List<Model.Many > Manies { get; set; }

        public ListArhive()
        {
            InitializeComponent();
            Manies = new List<Model.Many>();
            listArhive.BindingContext = this;
           
            ToolbarItem delAllitems = new ToolbarItem();
            delAllitems.Text = "Удалить всё";
            delAllitems.Clicked += DelAllitems_Clicked;
            Title = "Архивы";
            ToolbarItems.Add(delAllitems);

        }

        /// <summary>
        /// Удаляет все элемет из бд.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DelAllitems_Clicked(object sender, EventArgs e)
        {
            using (Context.Context context =new Context.Context())
            {


                foreach (var item in Manies)
                {
                    Model.Many many = context.Manies.Include(i => i.Incoms).
                        Include(i => i.Consumptions).FirstOrDefault(i => i.id == item.id);

                    if (many.Incoms.Count!=0)
                    {
                        context.Incoms.RemoveRange(many.Incoms);
                    }

                    if (many.Consumptions.Count != 0)
                    {
                        context.Consumptions.RemoveRange(many.Consumptions);
                    }

                    context.Manies.Remove(many);
               
                }

                context.SaveChanges();
                listArhive.ItemsSource = null;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            InitContextAsync();
            
        }

        /// <summary>
        /// Асинхронно загружает элемены из базы данных в приложение
        /// </summary>
        async void  InitContextAsync()
        {
            await Task.Run(() => InitContext());
        }

        /// <summary>
        /// Загружает элемены из базы данных в приложение
        /// </summary>
        void InitContext()
        {

            lock (lokingdb) {
                using ( Context.Context context = new Context.Context())
                {
                    Manies = context.Manies.AsNoTracking().ToList();
                    Manies.Remove(Manies[0]);
                    Manies.Reverse();
                }
            }

            Device.BeginInvokeOnMainThread(() => {
                listArhive.ItemsSource = Manies;
            });

        }

        /// <summary>
        /// Выбор элемента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void listArhive_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           if(e.CurrentSelection.Count != 0 )
            {
                await Navigation.PushAsync(new OpenArhive(e.CurrentSelection[0] as Model.Many));
                var f = sender as CollectionView;
                f.SelectedItem = null;
            }
                
        }
    }
}