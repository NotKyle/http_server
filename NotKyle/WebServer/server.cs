using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace NotKyle.WebServer
{
    public static class Server
    {
        private static HttpListener listener;
        private static int maxConnections = 20;
        private static Semaphore sem = new Semaphore(maxConnections, maxConnections);

        public static void Start(int port = 80)
        {
            List<IPAddress> localhostIPs = GetLocalIPs();
            listener = InitialiseListener(localhostIPs, port);
            ConfigLoader.ConfigLoader loader = LoadConfig();

            StartHost(loader);
        }

        private static void StartHost(ConfigLoader.ConfigLoader configLoader)
        {

            object config = configLoader.ParseConfig(configLoader.GetConfig());

            var configOpts = (Dictionary<string, object>)config;

            // Print the general config
            foreach (var opt in configOpts)
            {
                Console.WriteLine(opt.Key + ": " + opt.Value);
            }

            ConfigTemplate.ConfigTemplate cfg = new ConfigTemplate.ConfigTemplate();
            Logger.Logger.Log($"Starting server on {cfg.Host}:{cfg.Port}");

            cfg.Config(
                (string)configOpts["name"],
                (int)configOpts["port"],
                (string)configOpts["host"],
                (string)configOpts["protocol"],
                (string)configOpts["version"],
                (string)configOpts["env"],
                (int)configOpts["maxConnections"],
                (int)configOpts["timeout"],
                (bool)configOpts["keepAlive"]
            );

            if (cfg.VerifyConfig())
            {
                Start(listener);
            }
            else
            {
                Logger.Logger.Log("Failed to start server, invalid config.");

                foreach (string error in cfg.Errors)
                {
                    Logger.Logger.Log(error);
                }
            }
        }

        private static ConfigLoader.ConfigLoader LoadConfig()
        {
            ConfigLoader.ConfigLoader configLoader = new ConfigLoader.ConfigLoader();
            ConfigLoader.ConfigLoader config = configLoader.LoadConfig();

            return configLoader;
        }

        public static void Stop()
        {
            listener.Stop();
        }

        private static void Start(HttpListener listener)
        {
            listener.Start();
            Task.Run(() => RunServer(listener));
        }

        private static async Task RunServer(HttpListener listener)
        {
            while (true)
            {
                sem.WaitOne();
                await StartConnectionListener(listener);
            }
        }

        private static async Task StartConnectionListener(HttpListener listener)
        {
            HttpListenerContext context = await listener.GetContextAsync();
            sem.Release();

            string response = "<html><body><h1>Hello, World!</h1></body></html>";

            byte[] encoded = Encoding.UTF8.GetBytes(response);
            context.Response.ContentLength64 = encoded.Length;
            await context.Response.OutputStream.WriteAsync(encoded, 0, encoded.Length);
            context.Response.Close();
        }

        private static List<IPAddress> GetLocalIPs()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            return host.AddressList.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork).ToList();
        }

        private static HttpListener InitialiseListener(List<IPAddress> localhostIPs, int port = 80)
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add($"http://localhost:{port}/");

            foreach (IPAddress ip in localhostIPs)
            {
                listener.Prefixes.Add($"http://{ip}:{port}/");
            }

            return listener;
        }
    }
}

