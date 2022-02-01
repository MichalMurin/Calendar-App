using Calendar.ViewModels;

namespace Calendar.Views
{
    public partial class SearchPage
    {
        public SearchPage()
        {
            InitializeComponent();
            BindingContext = new SearchViewModel();
        }
    }
}