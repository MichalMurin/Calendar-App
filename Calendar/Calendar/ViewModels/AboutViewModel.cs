using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Calendar.ViewModels
{
    /// <summary>
    /// Trieda na zobrazenie karty "o aplikácii"
    /// </summary>
    public class AboutViewModel : BaseViewModel
    {
        /// <summary>
        /// Príkaz na otvorenie webového odkazu
        /// </summary>
        public ICommand OpenWebCommand { get; }

        /// <summary>
        /// Konštruktor, ktorý priradí karte titulok a vytvorí príkaz na otvorenie webového odkazu
        /// </summary>
        public AboutViewModel()
        {
            Title = "O aplikácii";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://vzdelavanie.uniza.sk/vzdelavanie/planinfo.php?kod=274671"));
        }
        
    }
}