using Calendar.Models;
using System;
using Calendar.Services;
using Xamarin.Forms;

namespace Calendar.ViewModels
{
    /// <summary>
    /// Trieda na zobrazenie okna pre pridanie kontaktu
    /// </summary>
    public class NewContactViewModel : BaseViewModel
    {
        /// <summary>
        /// Kalendír, na hodnototenie valídneho vstupu
        /// </summary>
        private readonly CalendarOfNameDays CalendarCheck;

        /// <summary>
        /// Meno kontaktu
        /// </summary>
        private string _name;

        /// <summary>
        /// Property mena
        /// </summary>
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        /// <summary>
        /// Príkaz na uloženie kontaktu
        /// </summary>
        public Command SaveCommand { get; }

        /// <summary>
        /// Príkaz na zrušenie akcie
        /// </summary>
        public Command CancelCommand { get; }

        /// <summary>
        /// Konštruktor, ktorý iniciaizuje príkazy a priradí udalosť
        /// </summary>
        public NewContactViewModel()
        {
            CalendarCheck = new CalendarOfNameDays();
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
        }

        /// <summary>
        /// Metoda na zistenie valídneho vstupu
        /// </summary>
        /// <returns>true ak nie je zadaný vstup prázdny a meno sa nachádza v kalendári</returns>
        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(_name) && CalendarCheck.IsNameInCalendar(_name);
        }

        /// <summary>
        /// Návrat do predchádzajúceho okna pri zrušení akcie
        /// </summary>
        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        /// <summary>
        /// Pridanie kontaktu do databázy
        /// </summary>
        private async void OnSave()
        {
            ContactModel newContact = new ContactModel()
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
            };
            await DataStore.AddItemAsync(newContact);
            await Shell.Current.GoToAsync("..");
        }
    }
}
