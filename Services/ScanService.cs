using System;
using System.IO;
using System.Threading.Tasks;
using Test.Models;

namespace Test.Services
{
    public class ScanService
    {
        private readonly ThreatLogService _threatLogService;
        private readonly DatabaseService _databaseService;
        private readonly FileHashService _hashService;

        public ScanService(ThreatLogService threatLogService)
        {
            _threatLogService = threatLogService;
            _databaseService = new DatabaseService();
            _hashService = new FileHashService();
        }
        public bool ScanFile(string filePath, string scanType)
        {
            try
            {
                if (!File.Exists(filePath))
                    return false;

                // 🔐 HASH CHECK
                string hash = _hashService.ComputeSHA256(filePath);

                if (MalwareDatabase.KnownMalwareHashes.Contains(hash))
                {
                    _threatLogService.LogThreat(filePath, "Hash Match");
                    return true;
                }

                // ⚠️ HEURISTIC CHECK
                if (filePath.EndsWith(".bat", StringComparison.OrdinalIgnoreCase) ||
                    filePath.EndsWith(".ps1", StringComparison.OrdinalIgnoreCase))
                {
                    _threatLogService.LogThreat(filePath, "Suspicious Script");
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
        public async Task<ScanResult> RunQuickScanAsync()
        {
            int threatCount = 0;

            string downloads = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "Downloads"
            );

            string[] foldersToScan =
            {
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                downloads
            };

            foreach (var folder in foldersToScan)
            {
                if (!Directory.Exists(folder))
                    continue;

                foreach (var file in Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories))
                {
                    await Task.Delay(2);

                    try
                    {
                        // 🔐 HASH CHECK
                        string hash = _hashService.ComputeSHA256(file);

                        if (MalwareDatabase.KnownMalwareHashes.Contains(hash))
                        {
                            threatCount++;
                            _threatLogService.LogThreat(file, "Hash Match");
                            continue;
                        }

                        // ⚠️ HEURISTIC CHECK
                        if (file.EndsWith(".bat", StringComparison.OrdinalIgnoreCase) ||
                            file.EndsWith(".ps1", StringComparison.OrdinalIgnoreCase))
                        {
                            threatCount++;
                            _threatLogService.LogThreat(file, "Suspicious Script");
                        }
                    }
                    catch
                    {
                        // Skip inaccessible files
                    }
                }
            }

            var result = new ScanResult
            {
                IsThreatFound = threatCount > 0,
                ThreatCount = threatCount,
                CompletedAt = DateTime.Now
            };

            // 💾 Save to database
            _databaseService.SaveScan(threatCount, result.IsThreatFound ? "Threats Found" : "Clean");

            return result;
        }
    }
}