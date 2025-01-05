using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace NotKyle.ConfigLoader
{
    public class ConfigLoader
    {
        private string configPath = "config.json";
        private object config;

        public ConfigLoader LoadConfig()
        {
            string json = File.ReadAllText(configPath);
            object config = ParseConfig(json);

            this.config = config;

            Logger.Logger.Log("Config [" + configPath + "] loaded successfully");

            return this;
        }

        public object ParseConfig(string json)
        {
            return JsonSerializer.Deserialize<Dictionary<string, object>>(json);
        }

        public string GetConfig()
        {
            return JsonSerializer.Serialize(config);
        }
    }
}
