namespace Test.Models
{
    public class ScanRecord
    {
        public int Id { get; set; }
        public string ScanDate { get; set; } = "";
        public int ThreatsFound { get; set; }
        public string Result { get; set; } = "";
    }
}