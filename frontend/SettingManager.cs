using System.IO;
using System.Text.Json;

namespace frontend
{
    public static class SettingsManager
    {
        private static readonly string SettingsPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "YourAppName",
            "settings.json");

        public static string EverythingPath { get; set; }

        static SettingsManager()
        {
            Load();
        }

        public static void Load()
        {
            if (File.Exists(SettingsPath))
            {
                var json = File.ReadAllText(SettingsPath);
                var data = JsonSerializer.Deserialize<SettingsData>(json);
                EverythingPath = data?.EverythingPath;
            }
        }

        public static void Save()
        {
            var dir = Path.GetDirectoryName(SettingsPath);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            var data = new SettingsData { EverythingPath = EverythingPath };
            var json = JsonSerializer.Serialize(data);
            File.WriteAllText(SettingsPath, json);
        }

        private class SettingsData
        {
            public string EverythingPath { get; set; }
        }
    }

}