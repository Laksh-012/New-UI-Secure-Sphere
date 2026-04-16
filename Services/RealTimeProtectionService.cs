using System;
using System.Collections.Generic;
using System.IO;
using Test.Services;

namespace Test.Services
{
    public class RealTimeProtectionService
    {
        private readonly ScanService _scanService = AppServices.ScanService;

        private List<FileSystemWatcher> _watchers = new();

        // ✅ START MONITORING (MULTIPLE FOLDERS)
        public void StartMonitoring(string path)
        {
            if (!Directory.Exists(path))
                return;

            var watcher = new FileSystemWatcher(path)
            {
                IncludeSubdirectories = true,
                EnableRaisingEvents = true
            };

            watcher.Created += OnFileChanged;
            watcher.Changed += OnFileChanged;
            watcher.Renamed += OnFileRenamed;

            _watchers.Add(watcher);
        }

        // 🛑 STOP ALL MONITORING
        public void StopMonitoring()
        {
            foreach (var watcher in _watchers)
            {
                watcher.EnableRaisingEvents = false;
                watcher.Dispose();
            }

            _watchers.Clear();
        }

        // 📂 FILE CREATED / MODIFIED
        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            if (e.FullPath.EndsWith(".tmp", StringComparison.OrdinalIgnoreCase))
                return;

            try
            {
                bool infected = _scanService.ScanFile(e.FullPath, "Real-Time Protection");

                if (infected)
                {
                    Console.WriteLine($"⚠️ Threat detected: {e.FullPath}");
                }
            }
            catch
            {
                // ignore errors
            }
        }

        // 🔁 FILE RENAMED
        private void OnFileRenamed(object sender, RenamedEventArgs e)
        {
            try
            {
                bool infected = _scanService.ScanFile(e.FullPath, "Real-Time Protection");

                if (infected)
                {
                    Console.WriteLine($"⚠️ Threat detected after rename: {e.FullPath}");
                }
            }
            catch
            {
                // ignore errors
            }
        }
    }
}