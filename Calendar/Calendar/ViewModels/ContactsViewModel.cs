using Calendar.Models;
using Calendar.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Calendar.Services;
using Xamarin.Forms;

namespace Calendar.ViewModels
{
    /// <summary>
    /// Trieda na zobrazenie kontaktov
    /// </summary>
    public class ContactsViewModel : BaseViewModel
    {
        /// <summary>
        /// Vybraný kontakt
        /// </summary>
        private ContactModel _selectedItem;

        /// <summary>
        /// Zoznam kontaktov
        /// </summary>
        public ObservableCollection<ContactModel> Contacts { get; }

        /// <summary>
        /// Príkaz na načítanie kontaktov zo zoznamu
        /// </summary>
        public Command LoadContactsCommand { get; }

        /// <summary>
        /// Príkaz na načítanie kontaktov zo zoznamu kontaktov v telefóne
        /// </summary>
        public Command LoadAllContactsCommand { get; }

        /// <summary>
        /// Príkaz na pridanie kontaktu do zoznamu
        /// </summary>
        public Command AddContactCommand { get; }

        /// <summary>
        /// Príkaz na otvorenie detailov o kontakte
        /// </summary>
        public Command<ContactModel> ContactTapped { get; }

        /// <summary>
        /// Konštruktor, ktorý inicializuje titulok a príkazy
        /// </summary>
        public ContactsViewModel()
        {
            Title = "Kontakty";
            Contacts = new ObservableCollection<ContactModel>();
            LoadContactsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ContactTapped = new Command<ContactModel>(OnContactSelected);
            AddContactCommand = new Command(OnAddContact);
            LoadAllContactsCommand = new Command(OnLoadContacts);
        }

        /// <summary>
        /// Obnovenie stránky na zobrazenie kontaktov
        /// </summary>
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            try
            {
                Contacts.Clear();
                var items = await DataStore.GetItemsAsync(true);
                items = items.OrderBy(contatct => contatct.Name);
                foreach (var item in items)
                {
                    Contacts.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Pri načítaní stránky vynuluje označný kontakt a zmení atribút IsBusy na true aby sa načítala stránka
        /// </summary>
        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        /// <summary>
        /// Načítanie kontaktov z telefónu
        /// </summary>
        private async void OnLoadContacts()
        {
            LoadContacts lc = new LoadContacts();
            await Task.Run(() => lc.LoadAllContactsAsync());
            await ExecuteLoadItemsCommand();
        }

        /// <summary>
        /// Označený kontakt
        /// </summary>
        public ContactModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnContactSelected(value);
            }
        }

        /// <summary>
        /// Pridanie kontaktu
        /// </summary>
        private async void OnAddContact()
        {
            await Shell.Current.GoToAsync(nameof(NewContactPage));
        }

        /// <summary>
        /// Zobrazenie detailu kontaktu
        /// </summary>
        /// <param name="item">Stlačený kontakt</param>
        async void OnContactSelected(ContactModel item)
        {
            if (item == null)
                return;
            
            await Shell.Current.GoToAsync($"{nameof(ContactDetailPage)}?{nameof(ContactDetailViewModel.ContactId)}={item.Id}");
        }
    }
}