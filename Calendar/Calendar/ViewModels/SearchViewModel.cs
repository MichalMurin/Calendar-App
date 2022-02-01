using System;
using System.Linq;
using Calendar.Services;
using Xamarin.Forms;

namespace Calendar.ViewModels
{
    /// <summary>
    /// Trieda na zobrazovanie karty vyhľadávania
    /// </summary>
    public class SearchViewModel : BaseViewModel
    {
        /// <summary>
        /// Správa o neuspešnom hľadaní
        /// </summary>
        private const string ErorMessage = "Nesprávne zadaná hodnota";

        /// <summary>
        /// Zadaný reťazec
        /// </summary>
        private string _value;

        /// <summary>
        /// Nájdený reťazec
        /// </summary>
        private string _result;

        /// <summary>
        /// Kalendár na vyhľadávanie 
        /// </summary>
        private readonly CalendarOfNameDays _calendar;

        /// <summary>
        /// Príkaz na hľadanie v zozname
        /// </summary>
        public Command SearchCommand { get; }

        /// <summary>
        /// Konštruktor, ktorý inicializuje atribúty a priradí triede titulok
        /// </summary>
        public SearchViewModel()
        {
            Title = "Hľadaj";
            _calendar = new CalendarOfNameDays();
            SearchCommand = new Command(OnSearch, ValidateSearch);
            PropertyChanged += (_, __) => SearchCommand.ChangeCanExecute();
        }

        /// <summary>
        /// Kontrola zadaného vstupu
        /// </summary>
        /// <returns>true ak zadaný vstup nieje prázdny</returns>
        private bool ValidateSearch()
        {
            return !string.IsNullOrWhiteSpace(_value);
        }

        /// <summary>
        /// Property zadaného reťazca
        /// </summary>
        public string Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        /// <summary>
        /// Property hľadaného reťazca
        /// </summary>
        public string Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }

        /// <summary>
        /// Metóda, ktorá vyhľadá v kalendári výsledok
        /// </summary>
        private void OnSearch()
        {
            if (GetTypeOfSearch() == 0)
            {
                var numbers = Value.Split('.');
                if (numbers.Count() > 2)
                {
                    int.TryParse(numbers[0], out var day);
                    int.TryParse(numbers[1], out var month);
                    if (day > 0 && month > 0)
                    {
                        var date = new DateTime(2000, month, day);
                        Result = _calendar.GetName(date);
                    }
                    else
                    {
                        Result = ErorMessage;
                    }
                }
                else
                {
                    Result = ErorMessage;
                }
            }
            else
            {
                Result = _calendar.GetDateOfNameDay(Value);
            }
        }

        /// <summary>
        /// Metóda určí či hľadáme dátum alebo meno
        /// </summary>
        /// <returns>0 ak hľadáme meno, 1 ak hľadáme dátum</returns>
        private int GetTypeOfSearch()
        {
            bool isNumber = int.TryParse(Value[0].ToString(), out _);
            if (isNumber)
            {
                return 0;
            }
            return 1;
        }
    }
}
