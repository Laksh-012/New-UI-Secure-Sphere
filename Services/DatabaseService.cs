using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using Test.Models;

namespace Test.Services
{
    public class DatabaseService
    {
        private readonly string _dbPath;

        public DatabaseService()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            _dbPath = Path.Combine(folder, "securesphere.db");

            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using var connection = new SqliteConnection($"Data Source={_dbPath}");
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
            CREATE TABLE IF NOT EXISTS ScanHistory (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                ScanDate TEXT,
                ThreatsFound INTEGER,
                Result TEXT
            );
            ";

            command.ExecuteNonQuery();
        }

        public void SaveScan(int threats, string result)
        {
            using var connection = new SqliteConnection($"Data Source={_dbPath}");
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
            INSERT INTO ScanHistory (ScanDate, ThreatsFound, Result)
            VALUES ($date, $threats, $result);
            ";

            command.Parameters.AddWithValue("$date", DateTime.Now.ToString("g"));
            command.Parameters.AddWithValue("$threats", threats);
            command.Parameters.AddWithValue("$result", result);

            command.ExecuteNonQuery();
        }

        public List<ScanRecord> GetScanHistory()
        {
            var list = new List<ScanRecord>();

            using var connection = new SqliteConnection($"Data Source={_dbPath}");
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM ScanHistory ORDER BY Id DESC";

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new ScanRecord
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    ScanDate = reader["ScanDate"].ToString() ?? "",
                    ThreatsFound = Convert.ToInt32(reader["ThreatsFound"]),
                    Result = reader["Result"].ToString() ?? ""
                });
            }

            return list;
        }
    }
}