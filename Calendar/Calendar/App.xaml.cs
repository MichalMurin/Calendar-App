using Calendar.Services;
using Xamarin.Forms;

namespace Calendar
{
    public partial class App 
    {
        public App()
        {
            InitializeComponent();
            DependencyService.Register<MockDataStore>();
            var unused = new CalendarOfNameDays("calendar.json");
            MainPage = new AppShell();
        }
        protected override void OnStart()
        {
        }
        protected override void OnSleep()
        {
        }
        protected override void OnResume()
        {
        }
    }
}
