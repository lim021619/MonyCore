using Microsoft.EntityFrameworkCore;
using MonyCore.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MonyCore
{
    public partial class MainPage : ContentPage
    {
        object lokerinitdata = new object();

        private int allManyUser = 0;
        private int incom = 0;
        private int consumption = 0;

        string rowconsumption = string.Empty;
        string rowincom = string.Empty;
        string rowAllManyUser = string.Empty;

        List<Model.Incom> incoms = new List<Model.Incom>();
        List<Model.Consumption> consumptions = new List<Model.Consumption>();   

        public List<Logic.ViewData> ViewDatas { get; set; }

        /// <summary>
        /// Графа доходов
        /// </summary>
        public int Incom { get => incom; set
            {
                incom = value;
                Rowincom = incom.ToString("c");
                
            }
        }

        /// <summary>
        /// Графа расходов
        /// </summary>
        public int Consumption { get => consumption; set
            {
                consumption = value;
                RowConsumption = consumption.ToString("c");
                
            }
        }
        /// <summary>
        /// Графа финансов на текущий момент.
        /// </summary>
        public int AllManyUser
        {
            get => allManyUser; set
            {
                lock(lokerinitdata)
                {
                    if (value < 0)
                    {
                        allManyUser = value;
                        int sqr = (allManyUser) * (allManyUser);
                        double i = (allManyUser * allManyUser) / Math.Sqrt(sqr);
                        RowAllManyUser = $"-{ i.ToString("c")}";

                    }
                    else
                    {
                        allManyUser = value;
                        RowAllManyUser = allManyUser.ToString("c");
                    }
                }
                
            }
        }
        /// <summary>
        /// Строка представляющая распологаемые финансы
        /// </summary>
        public string RowAllManyUser { get { return rowAllManyUser; } set {

                rowAllManyUser = value;
                OnPropertyChanged("RowAllManyUser");

            } }
        /// <summary>
        /// Строка представляющая доходы
        /// </summary>
        public string Rowincom { get { return rowincom; } set {

                rowincom = value;
                OnPropertyChanged("Rowincom");

            } }
        /// <summary>
        /// Строка предствляющаяя расходы
        /// </summary>
        public string RowConsumption { get { return rowconsumption; } set {

                rowconsumption = value;
                OnPropertyChanged("RowConsumption");
                

            } }

        public List<string> historyContent { get; set; }

        public List<string> historyDate { get; set; }

        public MainPage()
        {
            InitializeComponent();
            historyContent = new List<string>();
            historyContent = new List<string>();
            ViewDatas = new List<Logic.ViewData>();
            rootcont.BindingContext = this;
            AllManyNow.SetBinding(Label.TextProperty, "RowAllManyUser");
            ConsumptionText.SetBinding(Label.TextProperty, "RowConsumption");
            IncomText.SetBinding(Label.TextProperty, "Rowincom");
            RowAllManyUser = 0.ToString("c");
            RowConsumption = 0.ToString("c");
            Rowincom = 0.ToString("c");
            ToolbarItem arhiv =new ToolbarItem();
            arhiv.Text = "Архив";
            arhiv.Clicked += Button_Clicked;
            ToolbarItems.Add(arhiv);
            Title = "Главная";

            
        }
        /// <summary>
        /// Асинхронная инициализация данных о финансах
        /// </summary>
        async Task InitDataAsync()
        {
            await Task.Run(() => InitData());
        }
        /// <summary>
        /// Инициализация данных о финансах
        /// </summary>
        void InitData()
        {
            
                using (Context.Context context = new Context.Context())
                {
                    Model.Many many = context.Manies.
                        AsNoTracking().
                        Include(i => i.Incoms).
                        Include(i => i.Consumptions).
                        FirstOrDefault(m => m.id == 1);
                    bool one = false;
                    bool two = false;
                    decimal result = 0;

                    if (many == null)
                    {
                        Model.Many manyNew = new Model.Many();
                        context.Manies.Add(manyNew);
                        context.SaveChanges();

                    }
                    else
                    {
                        incoms = many.Incoms;
                        consumptions = many.Consumptions;

                        if (many.AllMany != 0)
                        {
                         Consumption = ((int)many.AllMany);

                            one = true;
                        }
                        if (many.AllManyConsumption != 0)
                        {
                         Incom = ((int)many.AllManyConsumption);

                            two = true;
                        }
                        if (one && two)
                        {
                            result = many.AllMany - many.AllManyConsumption;
                            AllManyUser = ((int)result);
                        }
                        else if (one)
                        {
                            AllManyUser = ((int)many.AllMany);
                        }
                    }

                }
            
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await InitDataAsync(); 
            ViewDatas = await InitRecentHistoryAsync();

            RecentOperations.BindingContext = this;
            RecentOperations.ItemsSource = ViewDatas;
           

        }

        async Task<List<Logic.ViewData>> InitRecentHistoryAsync()
        {
           return await Task.Run(() => InitRecentHistory());
        }

        List<Logic.ViewData> InitRecentHistory()
        {
            List<Logic.ViewData> result = new List<Logic.ViewData>();
            lock (lokerinitdata)
            {
               
                List<Interfases.IFinObjecte> finObjectes = new List<Interfases.IFinObjecte>();

                for (int i = 0; i < 30; i++)
                {
                   
                    if (i <= incoms.Count() - 1) finObjectes.Add(incoms[i]);
                    if (i <= consumptions.Count()-1) finObjectes.Add(consumptions[i]);
                }

                finObjectes.Reverse();

                FillList(finObjectes, ref result);
            }

           

            return result;
            
        }


        void FillList(List<Interfases.IFinObjecte> a, ref List<Logic.ViewData> b)
        {
            foreach (var item in a)
            {
                b.Add(item.GetDataView());
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
        /// <summary>
        /// Открытие окна о расходах
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void NewCon_Clicked(object sender, EventArgs e) => await Navigation.PushModalAsync(new AddCon());
        /// <summary>
        /// Открытие окна о доходах
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void NewInc_Clicked(object sender, EventArgs e) => await Navigation.PushModalAsync(new AddInc());

        private void NewCon_Clicked_1(object sender, EventArgs e)
        {
            addArhiveAsync();
        }

        async void addArhiveAsync()
        {
            await Task.Run(() => addArhive());
        }

        void addArhive()
        {
            lock (lokerinitdata)
            {
                Model.Many many = new Model.Many();

                using (Context.Context context = new Context.Context())
                {
                     many = context.Manies.Include(i => i.Incoms).Include(i => i.Consumptions).FirstOrDefault(i => i.id == 1);
                    Model.Many many2 = new Model.Many();
                    many2.AllMany = many.AllMany;
                    many2.DateCreate = many.DateCreate;
                    many2.NumberInArhive = context.Manies.Count() + 1;
                    many2.AllManyConsumption = many.AllManyConsumption;
                    many2.CountCounsuption = many.CountCounsuption;
                    many2.CountIncom = many.CountIncom;

                    foreach (var item in incoms)
                    {
                        var i = new Model.Incom {Data = item.Data, 
                        Many = many2,
                        Summ = item.Summ,
                        Time = item.Time,

                        };
                        many2.Incoms.Add(i);
                    }
                    foreach (var item in consumptions)
                    {
                        var i = new Model.Consumption
                        {
                            Data = item.Data,
                            Many = many2,
                            Summ = item.Summ,
                            Time = item.Time,

                        };

                        many2.Consumptions.Add(i);
                    }

                    context.Manies.Add(many2);

                    many.AllManyConsumption =
                     many.AllMany =
                      many.AllMany =
                       many.CountCounsuption =
                        many.CountIncom = 0;
                   

                    many.DateCreate = DateTime.Now.ToString();
                    many.DateArhive = String.Empty;
                    many.Incoms.Clear();
                    many.Consumptions.Clear();

                    
                    context.SaveChanges();
                }

            }

            AllManyUser = 0;
            Incom = 0;
            Consumption = 0;
            Device.BeginInvokeOnMainThread(() => {
                RecentOperations.ItemsSource = null;
            });
            ViewDatas.Clear();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListArhive());
        }
    }
}
