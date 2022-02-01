using System.Collections.Generic;
using System.Threading.Tasks;

namespace Calendar.Services
{
    /// <summary>
    /// Generický interface pre dátové úložisko objektov T
    /// </summary>
    /// <typeparam name="T">Generikum</typeparam>
    public interface IDataStore<T>
    {
        /// <summary>
        /// Pridanie položky do úložiska
        /// </summary>
        /// <param name="item">pridávaná položka</param>
        /// <returns>true ak sa podarilo vložiť položku</returns>
        Task<bool> AddItemAsync(T item);

        /// <summary>
        /// Odstránenie položky
        /// </summary>
        /// <param name="id">Identifikačný reťazec položky</param>
        /// <returns>true ak sa podarilo vymazať položku</returns>
        Task<bool> DeleteItemAsync(string id);

        /// <summary>
        /// Metóda na získanie konkrétnej položky
        /// </summary>
        /// <param name="id">Identifikačný reťazec položky</param>
        /// <returns>Konrétnu položku</returns>
        Task<T> GetItemAsync(string id);

        /// <summary>
        /// Metóda na získanie celého zoznamu
        /// </summary>
        /// <param name="forceRefresh">Informácia, či sa obnoví cieľová stránka</param>
        /// <returns>Celý zoznam</returns>
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
