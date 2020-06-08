using System.Threading.Tasks;
using System.Windows;

namespace Book.MVVM.Views
{
    public partial class Alarm_View : Window
    {
        public Alarm_View(Window window, string message)
        {
            InitializeComponent();
            AlarmMessage.Text = message;
            var pageLocation = window.PointToScreen(new Point(0, 0));
            Left = pageLocation.X + window.ActualWidth - 2 - Width;
            Top = pageLocation.Y + window.ActualHeight - 2 - Height;

            Application.Current.Dispatcher.InvokeAsync(async () =>
            {
                Show();
                await Task.Delay(3000);
                Close();
            });
        }
    }
}