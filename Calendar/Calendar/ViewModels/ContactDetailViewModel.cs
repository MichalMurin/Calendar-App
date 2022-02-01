using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace Calendar.ViewModels
{
    /// <summary>
    /// Trieda na zobrazenie detailu kontaktu
    /// </summary>
    [QueryProperty(nameof(ContactId), nameof(ContactId))]
    public class ContactDetailViewModel : BaseViewModel
    {
        /// <summary>
        /// Príkaz na odstránenie kontaktu
        /// </summary>
        public Command DeleteContactCommand { get; }

        /// <summary>
        /// Identifikátor kontaktu predaný z predchádzajúcej triedy
        /// </summary>
        private string _contactId;

        /// <summary>
        /// Meno kontaktu
        /// </summary>
        private string _name;

        /// <summary>
        /// Dátum menín 
        /// </summary>
        private string _dateOfNameDay;

        /// <summary>
        /// Identifikátor zvoleného kontaktu
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Konštruktor na inicializovanie príkazu
        /// </summary>
        public ContactDetailViewModel()
        {
            DeleteContactCommand = new Command(OnDeleteContact);
        }

        /// <summary>
        /// Property mena
        /// </summary>
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        /// <summary>
        /// Property dátumu menín
        /// </summary>
        public string DateOfNameDay
        {
            get => _dateOfNameDay;

            set => SetProperty(ref _dateOfNameDay, value);
        }

        /// <summary>
        /// Property identifikačného reťazca predaného predchádzajúcej triedy
        /// </summary>
        public string ContactId
        {
            get => _contactId;
            set
            {
                _contactId = value;
                LoadItemId(value);
            }
        }

        /// <summary>
        /// Načítanie konrétneho kontaktu podľa ID
        /// </summary>
        /// <param name="itemId">identifikačný reťazec hľadaného kontaktu</param>
        private async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Name = item.Name;
                DateOfNameDay = item.DateOfNameDay;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Contact");
            }
        }

        /// <summary>
        /// Vymazanie kontaktu
        /// </summary>
        private async void OnDeleteContact()
        {
            await DataStore.DeleteItemAsync(ContactId);
            await Shell.Current.GoToAsync("..");
        }
    }
}
