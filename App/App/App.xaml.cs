using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App
{
    public partial class App : Application
    {
        public static bool isAutorizated = false;
        public App ()
        {
            InitializeComponent();
            if (isAutorizated)
                MainPage = new MainPage();
            else
                MainPage = new NavigationPage(new MyPage());
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}

