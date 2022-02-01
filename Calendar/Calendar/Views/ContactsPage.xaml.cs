using Calendar.ViewModels;

namespace Calendar.Views
{
    public partial class ContactsPage
    {
        readonly ContactsViewModel _viewModel;
        public ContactsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ContactsViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}