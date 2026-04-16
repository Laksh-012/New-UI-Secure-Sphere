using System;

namespace Test.Models
{
    public class ThreatModel
    {
        public string FileName { get; set; } = "";
        public string FilePath { get; set; } = "";
        public DateTime DetectedAt { get; set; }
        public string ScanType { get; set; } = ""; // Quick Scan / Real-Time
    }
}
