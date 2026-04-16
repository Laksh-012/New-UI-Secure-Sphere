namespace Test.Services
{
    public class FileProtectionService
    {
        public bool VirusProtectionEnabled { get; private set; } = true;
        public bool RansomwareProtectionEnabled { get; private set; } = true;
        public bool FileProtectionEnabled { get; private set; } = true;

        public void ToggleVirusProtection(bool enabled)
            => VirusProtectionEnabled = enabled;

        public void ToggleRansomwareProtection(bool enabled)
            => RansomwareProtectionEnabled = enabled;

        public void ToggleFileProtection(bool enabled)
            => FileProtectionEnabled = enabled;
    }
}
