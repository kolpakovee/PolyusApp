using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace App
{
    public partial class HelpPage : ContentPage
    {
        public HelpPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        async void Switch_Toggled(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            if (sw.IsToggled == false)
            {
                (double, double) location = await MainPage.GetLocation(sender, new EventArgs());
                await DisplayAlert("Уведомление", $"Ваша смена завершена!\n", "OK");
            }
            else
            {
                await Navigation.PushModalAsync(new MainPage());
            }
        }
    }
}

