using Calendar.Services;

namespace Calendar.Models
{
    /// <summary>
    /// Trieda predstavujúca model kontaktu, s príslušnými atribútami
    /// </summary>
    public class ContactModel
    {
        /// <summary>
        /// jedinečný identifikátor kontaktu
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Meno kontaktu
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Dátum menín kontaktu
        /// </summary>
        public string DateOfNameDay => ($"{new CalendarOfNameDays().GetDateOfNameDay(Name)}");
    }
}