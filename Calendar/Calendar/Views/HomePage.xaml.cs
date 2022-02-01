using Calendar.ViewModels;
using Xamarin.Forms;

namespace Calendar.Views
{
    public partial class HomePage
    {
        public HomePage()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel();
        }
    }
}