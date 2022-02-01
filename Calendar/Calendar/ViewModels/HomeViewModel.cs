using System;
using Calendar.Services;

namespace Calendar.ViewModels
{
    /// <summary>
    /// Trieda na zobrazenie domovskej stránky
    /// </summary>
    public class HomeViewModel : BaseViewModel
    {
        /// <summary>
        /// Konštruktor na inicializovanie titulku triedy
        /// </summary>
        public HomeViewModel()
        {
            Title = "Domov";
        }

        /// <summary>
        /// Meno dnešného meninového osávenca
        /// </summary>
        public string TodaysName
        {
            get
            {
                CalendarOfNameDays calendar = new CalendarOfNameDays();
                return calendar.GetName(DateTime.Now);
            }
        }
    }
}
