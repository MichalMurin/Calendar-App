using Calendar.Views;
using Xamarin.Forms;

namespace Calendar
{
    public partial class AppShell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ContactDetailPage), typeof(ContactDetailPage));
            Routing.RegisterRoute(nameof(NewContactPage), typeof(NewContactPage));
        }
    }
}
