using System;

namespace Test.Models
{
    public class ScanResult
    {
        public bool IsThreatFound { get; set; }
        public int ThreatCount { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}