using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Test.Pages;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Test.Panel
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SecurityPage : Page
    {
        public SecurityPage()
        {
            this.InitializeComponent();
            MainFrame.Navigate(typeof(DashboardPage));

        }
        private void Dashboard_click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(DashboardPage));
        }
        private void Devices_click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(DevicesPage));
        }
        private void Protection_click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(ProtectionPage));
        }
        private void WebSecurity_click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(WebSecurityPage));
        }
        private void Settings_click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(SettingsPage));
        }
    }
}
