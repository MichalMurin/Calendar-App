using System;
using System.Diagnostics;
using Calendar.Models;
using System.Linq;
using System.Threading;
using Calendar.ViewModels;
using Xamarin.Essentials;
namespace Calendar.Services
{
    /// <summary>
    /// Trieda na nčítanie kontaktov z telefónu
    /// </summary>
    public class LoadContacts: BaseViewModel
    {
        /// <summary>
        /// Metóda, ktorá načíta kontakty z telefónu a uloží ich do zoznamu
        /// Zdroj: https://docs.microsoft.com/en-us/xamarin/essentials/contacts?context=xamarin%2Fxamarin-forms&tabs=android
        /// </summary>
        public async void LoadAllContactsAsync()
        {
            try
            {
                var cancellationToken = default(CancellationToken);
                var contacts = await Contacts.GetAllAsync(cancellationToken);
                if (contacts == null)
                    return;
                var contactsList = contacts.ToList();

                foreach (var contact in contactsList)
                {
                    ContactModel newContact = new ContactModel()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = contact.GivenName,
                        };
                    await DataStore.AddItemAsync(newContact);
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to load contacts.");
            }
        }
    }
}
