using Book.MVVM.ViewModels;
using System.Windows.Controls;

namespace Book.MVVM.Views
{
    public partial class ChapterRedact_View : Page
    {
        public ChapterRedact_View(object parameter)
        {
            InitializeComponent();
            DataContext = new ChapterRedact_ViewModel(parameter);
        }
    }
}
