using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Test.Services;
using System;
using Windows.Storage;

namespace Test.Pages
{
    public sealed partial class ProtectionPage : Page
    {
        private readonly ApplicationDataContainer _settings = ApplicationData.Current.LocalSettings;

        public ProtectionPage()
        {
            this.InitializeComponent();
            LoadToggleStates();
        }

        // 🔄 LOAD SAVED STATES
        private void LoadToggleStates()
        {
            VirusToggle.IsOn = (bool?)_settings.Values["VirusProtection"] ?? false;
            RansomwareToggle.IsOn = (bool?)_settings.Values["RansomwareProtection"] ?? false;
            FileProtectionToggle.IsOn = (bool?)_settings.Values["FileProtection"] ?? false;

            // 🔥 Apply logic immediately after loading
            ApplyProtectionStates();
        }

        // 🚀 APPLY STATES (VERY IMPORTANT)
        private void ApplyProtectionStates()
        {
            if (VirusToggle.IsOn)
            {
                AppServices.RealTimeProtection.StartMonitoring(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            }

            if (RansomwareToggle.IsOn)
            {
                AppServices.RealTimeProtection.StartMonitoring(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            }

            if (FileProtectionToggle.IsOn)
            {
                string downloads = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";

                AppServices.RealTimeProtection.StartMonitoring(downloads);
            }
        }

        // 🛡️ VIRUS PROTECTION
        private void VirusToggle_Toggled(object sender, RoutedEventArgs e)
        {
            _settings.Values["VirusProtection"] = VirusToggle.IsOn;

            if (VirusToggle.IsOn)
            {
                AppServices.RealTimeProtection.StartMonitoring(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            }
            else
            {
                AppServices.RealTimeProtection.StopMonitoring();
                ApplyProtectionStates(); // restart others if ON
            }
        }

        // 🔒 RANSOMWARE PROTECTION
        private void RansomwareToggle_Toggled(object sender, RoutedEventArgs e)
        {
            _settings.Values["RansomwareProtection"] = RansomwareToggle.IsOn;

            if (RansomwareToggle.IsOn)
            {
                AppServices.RealTimeProtection.StartMonitoring(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            }
            else
            {
                AppServices.RealTimeProtection.StopMonitoring();
                ApplyProtectionStates();
            }
        }

        // 📁 FILE PROTECTION
        private void FileProtectionToggle_Toggled(object sender, RoutedEventArgs e)
        {
            _settings.Values["FileProtection"] = FileProtectionToggle.IsOn;

            string downloads = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";

            if (FileProtectionToggle.IsOn)
            {
                AppServices.RealTimeProtection.StartMonitoring(downloads);
            }
            else
            {
                AppServices.RealTimeProtection.StopMonitoring();
                ApplyProtectionStates();
            }
        }
    }
}