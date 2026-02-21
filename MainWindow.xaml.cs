using Microsoft.UI.Xaml;
using Test.Panel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Test
{

    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainFrame.Navigate(typeof(SecurityPage));
        }

        private void Performance_click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(PerformancePage));

        }
        private void Security_click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(SecurityPage));

        }
    }
}
