using System;
using Calendar.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Services
{
    /// <summary>
    /// Trieda predstavujúca zoznam kontaktov
    /// </summary>
    public class MockDataStore : IDataStore<ContactModel>
    {
        /// <summary>
        /// Zoznam kontaktov tzpu List
        /// </summary>
        private readonly List<ContactModel> _items;

        /// <summary>
        /// Kalendár na zisťovanie, či sa v kalendári nachádza meno ktoré sa pokúšame pridať
        /// </summary>
        public readonly CalendarOfNameDays CalendarCheck;

        /// <summary>
        /// Konštruktor, v ktorom sa vytvorí zoznam a pridá sa prvý kontakt
        /// </summary>
        public MockDataStore()
        {
            CalendarCheck = new CalendarOfNameDays();
            _items = new List<ContactModel>()
            {
                new ContactModel { Id = Guid.NewGuid().ToString(), Name = "Alojz Príklad"},
            };
        }

        /// <inheritdoc cref="IDataStore{T}"/>
        public async Task<bool> AddItemAsync(ContactModel item)
        {
            if (CalendarCheck.IsNameInCalendar(item.Name) && !IsSameNameInDatabse(item.Name))
            {
                _items.Add(item);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        /// <summary>
        /// Metóda, ktorá overí výskyt rovnakého mena v databáze
        /// </summary>
        /// <param name="name">Overované meno</param>
        /// <returns>true ak sa meno už v databáze nachádza</returns>
        private bool IsSameNameInDatabse(string name)
        {
            var names = _items.Select(contact => contact.Name);
            if (names.Contains(name))
                return true;
            return false;
        }

        /// <inheritdoc cref="IDataStore{T}"/>
        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = _items.FirstOrDefault(arg => arg.Id == id);
            _items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        /// <inheritdoc cref="IDataStore{T}"/>
        public async Task<ContactModel> GetItemAsync(string id)
        {
            return await Task.FromResult(_items.FirstOrDefault(s => s.Id == id));
        }

        /// <inheritdoc cref="IDataStore{T}"/>
        public async Task<IEnumerable<ContactModel>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(_items);
        }
    }
}