using Test.Models;
using System;
using System.Collections.ObjectModel;

namespace Test.Services
{
    public class ThreatLogService
    {
        public ObservableCollection<ThreatModel> Threats { get; }
            = new ObservableCollection<ThreatModel>();

        public void LogThreat(string filePath, string scanType)
        {
            Threats.Add(new ThreatModel
            {
                FileName = System.IO.Path.GetFileName(filePath),
                FilePath = filePath,
                DetectedAt = DateTime.Now,
                ScanType = scanType
            });
        }
    }
}

