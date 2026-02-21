using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Management;

namespace Test.Panel
{
    public sealed partial class PerformancePage : Page
    {
        public PerformancePage()
        {
            this.InitializeComponent();
            LoadSystemInfo();
        } 
        private void LoadSystemInfo()
        {
            CPUTextBlock.Text = GetCPU();
            RAMTextBlock.Text = GetRAM();
            GPUTextBlock.Text = GetGPU();
            OSTextBlock.Text = GetOS();
        }

        public string GetCPU()
        {
            var searcher = new ManagementObjectSearcher("select * from Win32_Processor");

            foreach (var item in searcher.Get())
                return item["Name"].ToString();

            return "Unknown CPU";
        }

        public string GetRAM()
        {
            var searcher = new ManagementObjectSearcher("select * from Win32_ComputerSystem");

            foreach (var item in searcher.Get())
            {
                double ram = System.Math.Round(
                    System.Convert.ToDouble(item["TotalPhysicalMemory"]) / (1024 * 1024 * 1024), 2);

                return ram + " GB";
            }

            return "Unknown RAM";
        }

        public string GetGPU()
        {
            var searcher = new ManagementObjectSearcher("select * from Win32_VideoController");

            foreach (var item in searcher.Get())
                return item["Name"].ToString();

            return "Unknown GPU";
        }

        public string GetOS()
        {
            var searcher = new ManagementObjectSearcher("select * from Win32_OperatingSystem");

            foreach (var item in searcher.Get())
                return item["Caption"].ToString();

            return "Unknown OS";
        }
    }
}