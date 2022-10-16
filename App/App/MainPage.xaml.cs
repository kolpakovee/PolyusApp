using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Diagnostics;
using System.Net.Http;
using System.Drawing;
using System.Threading;
using Point = Xamarin.Forms.Point;

namespace App
{
    public partial class MainPage : ContentPage
    {
        public IList<Request> Requests { get; set; }
        public Dictionary<string, Request> RequestsDictionary { get; set; }
        private static readonly HttpClient client = new HttpClient();
        private readonly string url = "";
        public MainPage()
        {
            InitializeComponent();
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            SendCoordinatesToServer();
            await Navigation.PopModalAsync();
        }

        public async static Task<(double,double)> GetLocation(System.Object sender, System.EventArgs e)
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Low,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
                }
                if (location == null)
                {
                    return (0, 0);
                }
                else
                {
                    return (location.Latitude, location.Longitude);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Something is wrong: {ex.Message}");
                return (0, 0);
            }
        }

        protected override void OnAppearing()
        {
            DateTime begin = new DateTime(2022, 5, 29);
            DateTime end = new DateTime(2022, 6, 10);
            Requests = new List<Request>();
            Requests.Add(new Request("Egor", "Kolpakov","Evgenyevich", "Moscow, Tverskaya, 33", "Срочно", begin, end, "+7(987) 777 777", "257"));
            Requests.Add(new Request("Ivan", "Ivanov", "Ivanovich", "Moscow, Tverskaya, 2/4", "Чрезвычайная ситуация", begin, end, "+7(987) 777 777", "642"));
            Requests.Add(new Request("Max", "Petrov", "Ruslanovich", "Moscow, Sadovaya, 1", "Совсем не срочно", begin, end, "+7(987) 777 777", "925"));
            RequestsDictionary = new Dictionary<string, Request>();
            RequestsDictionary.Add(Requests[0].ID, Requests[0]);
            RequestsDictionary.Add(Requests[1].ID, Requests[1]);
            RequestsDictionary.Add(Requests[2].ID, Requests[2]);
            BindingContext = this;
            base.OnAppearing();
        }

        async void OnSelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            Request r = (Request)e.CurrentSelection[0];
            bool b = await DisplayAlert($"Заявка #{r.ID}", $"Вы хотите выполнить заявку?\n\n{r.GetInfo()}", "Да", "Нет");
            if (b)
            {
                foreach(var el in Requests)
                {
                    if (el.ID == r.ID)
                        el.color = Xamarin.Forms.Color.LightGreen; 
                }
                
                //Dictionary<string, string> dict = new Dictionary<string, string>()
                //{
                //    { location.Item1.ToString(), location.Item2.ToString()}
                //};
                //FormUrlEncodedContent form = new FormUrlEncodedContent(dict);

                //HttpResponseMessage response = await client.PostAsync(url, form); // send data to server

                //string result = await response.Content.ReadAsStringAsync(); // считывание результата
            }
        }

        async void RefreshView_Refreshing(System.Object sender, System.EventArgs e)
        {
            await Task.Delay(3000);
            Debug.WriteLine("Updated!");
            refreshView.IsRefreshing = false;
        }

        async void SendCoordinatesToServer()
        {
            (double, double) location = await MainPage.GetLocation(new Object(), new EventArgs());
            await DisplayAlert("Уведомление", $"Отправил ваши координаты {location.Item1} {location.Item2} на сервер", "ОК");
        }


    }
}

