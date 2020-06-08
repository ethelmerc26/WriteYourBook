using Book.MVVM;
using Book.MVVM.Views;
using Book.Utils;
using System.Windows;

namespace Book
{
    public partial class App : Application
    {
        public App()
        {
            Navigation.MainWindow = new MainWindow { Content = new Chapters_View(), Visibility = Visibility.Visible };
        }
    }
}
