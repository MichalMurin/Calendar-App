using Calendar.ViewModels;

namespace Calendar.Views
{
    public partial class NewContactPage 
    {
        public NewContactPage()
        {
            InitializeComponent();
            BindingContext = new NewContactViewModel();
        }
    }
}