namespace Test.Services
{
    public static class AppServices
    {
        public static DatabaseService Database { get; } = new DatabaseService();

        public static ScanService ScanService { get; } =
            new ScanService(new ThreatLogService());

        public static RealTimeProtectionService RealTimeProtection { get; } =
            new RealTimeProtectionService();
    }
}