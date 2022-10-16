using System;
using Xamarin.Forms;

namespace App
{
    public class Request
    {
        public string FIO { get; set; }
        public string Adress { get; set; }
        public Image Urgency { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string UrgencyStatus { get; set; }
        string Number { get; set; }
        public string ID { get;set; }
        public Color color { get; set; }

        public Request(string name, string surname, string patronymic, string adress, string urgency, DateTime begin,
            DateTime end, string number, string id)
        {
            FIO = name + " " + patronymic + " " + surname;
            Adress = adress;
            Urgency = new Image();
            UrgencyStatus = urgency;
            Number = number;
            BeginDate = begin;
            EndDate = end;
            ID = id;
            color = Color.White;
            // switch (urgency) { }
            if (urgency == "Совсем не срочно")
                Urgency.Source = "image4";
            else if (urgency == "Не очень срочно")
                Urgency.Source = "image2";
            else if (urgency == "Срочно")
                Urgency.Source = "image1";
            else
                Urgency.Source = "highpriority";
        }

        public string GetInfo()
        {
            string result = "ИНФОРМАЦИЯ О ЗАЯВКЕ" + "\n";
            result += $"ЗАКАЗЧИК:  {FIO}\nТЕЛЕФОН:  {Number}\nАДРЕС:  {Adress}\nСРОЧНОСТЬ:  {UrgencyStatus}\nДАТА НАЧАЛА:  {BeginDate.ToShortDateString()}\n" +
                $"ДАТА ОКОНЧАНИЯ:  {EndDate.ToShortDateString()}\n";
            return result;
        }
    }
}

