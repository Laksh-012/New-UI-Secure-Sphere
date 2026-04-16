namespace Test.Services
{
    public class WebSecurityService
    {
        public bool WebsiteProtection { get; set; } = true;
        public bool PhishingProtection { get; set; } = true;
        public bool NsfwFiltering { get; set; }
        public bool PasswordProtection { get; set; } = true;
    }
}
