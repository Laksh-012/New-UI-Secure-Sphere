using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Test.Services
{
    public class FileHashService
    {
        public string ComputeSHA256(string filePath)
        {
            using var stream = File.OpenRead(filePath);
            using var sha = SHA256.Create();

            byte[] hash = sha.ComputeHash(stream);

            StringBuilder sb = new StringBuilder();
            foreach (byte b in hash)
                sb.Append(b.ToString("x2"));

            return sb.ToString();
        }
    }
}