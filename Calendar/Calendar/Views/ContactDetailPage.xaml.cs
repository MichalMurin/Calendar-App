using Calendar.ViewModels;

namespace Calendar.Views
{
    public partial class ContactDetailPage
    {
        public ContactDetailPage()
        {
            InitializeComponent();
            BindingContext = new ContactDetailViewModel();
        }
    }
}