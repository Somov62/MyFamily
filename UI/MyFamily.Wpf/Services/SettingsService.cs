using Newtonsoft.Json;
using System;
using System.IO;

namespace MyFamily.Wpf.Services
{
    internal static class SettingsService
    {
        private static readonly string _pathToFile = Path.Combine(Environment.CurrentDirectory, "appsettings.json");

        public static Configuration Configuration { get; } = Load();

        public static void SaveChanges()
        {
            File.WriteAllText(_pathToFile, JsonConvert.SerializeObject(Configuration));
        }

        private static Configuration Load()
        {
            if (!File.Exists(_pathToFile))
                File.Create(_pathToFile).Close();
            Configuration config = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(_pathToFile))!;
            config ??= new Configuration()
            {
                
            };
            return config;
        }
    }

    internal class Configuration
    {

        public Models.Family? Family { get; set; }

    }
}
