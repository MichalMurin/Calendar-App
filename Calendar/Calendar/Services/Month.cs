using System.Collections.Generic;

namespace Calendar.Services
{
    /// <summary>
    /// Trieda reprezentujúca jeden mesiac v roku
    /// </summary>
    public class Month
    {
        /// <summary>
        /// Zoznam dní a k nim priradených mien jedného mesiaca
        /// </summary>
        public Dictionary<int, string> OneMonth { get; set; }
    }
}
