﻿using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MonyCore
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
         
            MainPage = new Model.HelloPage();

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
