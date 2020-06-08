using Book.MVVM.ViewModels;
using System.Windows.Controls;

namespace Book.MVVM.Views
{
    public partial class PartRedact_View : Page
    {
        public PartRedact_View(object parameter)
        {
            InitializeComponent();
            DataContext = new PartRedact_ViewModel(parameter);
        }
    }
}
