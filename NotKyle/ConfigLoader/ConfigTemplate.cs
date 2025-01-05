using System.Collections.Generic;

namespace NotKyle.ConfigTemplate
{
    public class ConfigTemplate
    {
        /**
      *"name": "NotKyle Frontend",
      *"port": 80,
      *"host": "notkyle.org",
      *"protocol": "http",
      *"version": "1.0.0",
      *"env": "production",
      *"maxConnections": 1000,
      *"timeout": 5000,
      *"keepAlive": true
        */

        public string Name { get; set; }
        public int Port { get; set; }
        public string Host { get; set; }
        public string Protocol { get; set; }
        public string Version { get; set; }
        public string Env { get; set; }
        public int MaxConnections { get; set; }
        public int Timeout { get; set; }
        public bool KeepAlive { get; set; }

        // Store config errors
        public string Error { get; set; }
        public List<string> Errors { get; } = new List<string>();

        public void Config()
        {
            Name = "*";
            Port = 80;
            Host = "*";
            Protocol = "http";
            Version = "1.0.0";
            Env = "production";
            MaxConnections = 1000;
            Timeout = 5000;
            KeepAlive = true;
        }

        public void Config(string name,
                int port = 80,
                string host = "*",
                string protocol = "http",
                string version = "1.0.0",
                string env = "production",
                int maxConnections = 1000,
                int timeout = 5000,
                bool keepAlive = true
            )
        {
            Name = name;
            Port = port;
            Host = host;
            Protocol = protocol;
            Version = version;
            Env = env;
            MaxConnections = maxConnections;
            Timeout = timeout;
            KeepAlive = keepAlive;
        }

        public override string ToString()
        {
            return $"Name: {Name}\nPort: {Port}\nHost: {Host}\nProtocol: {Protocol}\nVersion: {Version}\nEnv: {Env}\nMaxConnections: {MaxConnections}\nTimeout: {Timeout}\nKeepAlive: {KeepAlive}";
        }

        public static ConfigTemplate FromJson(string json)
        {
            return System.Text.Json.JsonSerializer.Deserialize<ConfigTemplate>(json);
        }

        public string ToJson()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }

        public bool VerifyConfig()
        {
            // Compare our loaded config toa make sure all the values are correct 
            if (Name == null || Name == "")
            {
                Errors.Add("Name is required");
            }

            if (Port < 1 || Port > 65535)
            {
                Errors.Add("Port must be between 1 and 65535");
            }

            if (Host == null || Host == "")
            {
                Errors.Add("Host is required");
            }

            if (Protocol == null || Protocol == "")
            {
                Errors.Add("Protocol is required");
            }

            if (Version == null || Version == "")
            {
                Errors.Add("Version is required");
            }

            if (Env == null || Env == "")
            {
                Errors.Add("Env is required");
            }

            if (MaxConnections < 1)
            {
                Errors.Add("MaxConnections must be greater than 0");
            }

            if (Timeout < 1)
            {
                Errors.Add("Timeout must be greater than 0");
            }

            return Errors.Count == 0;
        }
    }
}
