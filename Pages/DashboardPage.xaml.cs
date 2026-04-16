using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using Test.Services;

namespace Test.Pages
{
    public sealed partial class DashboardPage : Page
    {
        private readonly DatabaseService _db = new DatabaseService();
        private readonly ScanService _scanService = AppServices.ScanService;

        public DashboardPage()
        {
            this.InitializeComponent();
            LoadHistory();

            // Initial UI state
            StatusText.Text = "Active";
            StatusText.Foreground = new SolidColorBrush(Microsoft.UI.Colors.LimeGreen);
        }

        // 🔹 Load scan history
        private void LoadHistory()
        {
            try
            {
                var history = _db.GetScanHistory();
                HistoryList.ItemsSource = history;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("History Error: " + ex.Message);
            }
        }

        // 🔹 Quick Scan Button
        private async void QuickScan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 🔥 STEP 1: Show scanning state
                StatusText.Text = "Scanning...";
                StatusText.Foreground = new SolidColorBrush(Microsoft.UI.Colors.Yellow);

                // Optional: reset threats count during scan
                ThreatsCount.Text = "0";

                // 🔥 RUN SCAN
                var result = await _scanService.RunQuickScanAsync();

                // 🔥 STEP 2: Update UI after scan
                ThreatsCount.Text = result.ThreatCount.ToString();
                LastScanText.Text = result.CompletedAt.ToString("g");

                if (result.IsThreatFound)
                {
                    StatusText.Text = "Threats Removed";
                    StatusText.Foreground = new SolidColorBrush(Microsoft.UI.Colors.Red);
                }
                else
                {
                    StatusText.Text = "Protected";
                    StatusText.Foreground = new SolidColorBrush(Microsoft.UI.Colors.LimeGreen);
                }

                // 🔥 Reload history
                LoadHistory();
            }
            catch (Exception ex)
            {
                StatusText.Text = "Error";
                StatusText.Foreground = new SolidColorBrush(Microsoft.UI.Colors.Red);

                System.Diagnostics.Debug.WriteLine("Scan Error: " + ex.Message);
            }
        }
    }
}