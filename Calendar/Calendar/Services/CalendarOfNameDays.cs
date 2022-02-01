using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace Calendar.Services
{
    /// <summary>
    /// Trieda slúžiaca na načítanie zozanmu mien a hľadanie v tomto zozname
    /// </summary>
    public class CalendarOfNameDays
    {
        /// <summary>
        /// Konštanta - meno súboru v cache pamäti kde sa uloží súbor s menami
        /// </summary>
        private const string FileNameInCache = "CacheCalendar.json";

        /// <summary>
        /// Správa o nenájdení mena v kalendári
        /// </summary>
        private const string NameNotFoundMessage = "Meno nebolo nájdené";

        /// <summary>
        /// Správa o zadaní neplatného dátumu
        /// </summary>
        private const string WrongDateMessage = "Nesprávne zadaný dátum";

        /// <summary>
        /// Zoznam mien a dní v jednom roku
        /// </summary>
        private Dictionary<int, Month> Year { get; set; }

        /// <summary>
        /// Konštruktor, ktorý sa pokúša načítať súbor a následne ho uložiť do cach pamäte, volaním metódy SaveCalendarFromOutsideToCache()
        /// </summary>
        /// <param name="fileNameAssets">názov súboru v priečinku Assets</param>
        public CalendarOfNameDays(string fileNameAssets)
        {
            SaveCalendarFromOutsideToCache(fileNameAssets, FileNameInCache);
        }

        /// <summary>
        /// Konštruktor, ktorý načíta zoznam mien a dní a volá metódu na deserializovanie súboru do atribútu Year
        /// </summary>
        public CalendarOfNameDays()
        {
            Year = new Dictionary<int, Month>();
            LoadCalendarFromCache(FileNameInCache);
        }

        /// <summary>
        /// Metóda ktorá načíta súbor z cache pamäte a následne ho deserializuje do atribútu Year
        /// </summary>
        /// <param name="sourceFileName">Názov súboru v cache pamäti</param>
        private void LoadCalendarFromCache(string sourceFileName)
        {
            var path = Path.Combine(FileSystem.CacheDirectory, sourceFileName);
            var text = File.ReadAllText(path);
            Year = JsonConvert.DeserializeObject<Dictionary<int, Month>>(text);
        }

        /// <summary>
        /// Metóda ktorá načíta súbor z priečinku Assets a uloží ho do cache pamäte
        /// Zdroj: https://docs.microsoft.com/en-us/xamarin/essentials/file-system-helpers?context=xamarin%2Fandroid&tabs=android
        /// </summary>
        /// <param name="sourceFileName">Názov súboru v priečinku Assets</param>
        /// <param name="targetFileName">Názov priečinku, ktorý sa vytvorí v cache pamäti</param>
        private async void SaveCalendarFromOutsideToCache(string sourceFileName, string targetFileName)
        {
            string text;
            using (var stream = await FileSystem.OpenAppPackageFileAsync(sourceFileName))
            {
                using (var reader = new StreamReader(stream))
                {
                    text = await reader.ReadToEndAsync();
                }
            }
            var path = Path.Combine(FileSystem.CacheDirectory, targetFileName);
            File.WriteAllText(path,text);
        }

        /// <summary>
        /// Metóda, ktorá vráti dátum menín zadaného mena
        /// </summary>
        /// <param name="name">Meno pre ktoré hľadáme dátum menín</param>
        /// <returns>Dátum menín</returns>
        public string GetDateOfNameDay(string name)
        {
            var names = name.Split(' ');
            foreach (var tempname in names)
            {
                var cleanName = CleanString(tempname);
                foreach (var month in Year)
                {
                    foreach (var day in month.Value.OneMonth)
                    {
                        var possibleValues = day.Value.Split(',');
                        foreach (var value in possibleValues)
                        {
                            var cleanValue = CleanString(value);
                            if (cleanValue.Equals(cleanName))
                            {
                                return $"{day.Key}.{month.Key + 1}.";
                            }
                        }
                    }
                }
            }
            return NameNotFoundMessage;
        }

        /// <summary>
        /// Metóda, ktorá vráti meno prislúchajúce k zadanému dátumu
        /// </summary>
        /// <param name="date">Dátum pre ktorý hľadáme meno</param>
        /// <returns>Meninové meno</returns>
        public string GetName(DateTime date)
        {
            int day = date.Day;
            int month = date.Month;
            var tempMonth = Year[month - 1].OneMonth;
            var result = tempMonth[day];
            if (result != null)
            {
                return result;
            }
            return WrongDateMessage;
        }

        /// <summary>
        /// Metóda, ktorá zistí, či sa meno nachádza v kalendári
        /// </summary>
        /// <param name="name">hľadané meno</param>
        /// <returns>True ak sa meno nachádza v kalendári</returns>
        public bool IsNameInCalendar(string name)
        {
            if (GetDateOfNameDay(name) == NameNotFoundMessage)
                return false;
            return true;
        }

        /// <summary>
        /// Metóda ktorá vracia očistený reťazec od diakritiky a v malých písmenách
        /// </summary>
        /// <param name="text">Reťazec, ktorý chceme očistiť</param>
        /// <returns>Očistený reťazec</returns>
        private static string CleanString(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;
            text = text.ToLower().Trim();
            text = text.Normalize(NormalizationForm.FormD);
            var chars = text.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            return new string(chars).Normalize(NormalizationForm.FormC);
        }
    }
}
