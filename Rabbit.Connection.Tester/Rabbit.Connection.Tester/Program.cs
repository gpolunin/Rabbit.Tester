using System;
using System.Configuration;
using System.Diagnostics;
using log4net;
using log4net.Config;
using RabbitMQ.Client;

namespace Rabbit.Connection.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            //BasicConfigurator.Configure();
            XmlConfigurator.Configure();
            var log = LogManager.GetLogger(typeof(Program));
            log.Info("Starting applicaction");
            log.Info("Creating rabbit mq connection factory");

            var rabbitUri = new Uri(ConfigurationManager.AppSettings["RabbitUri"], UriKind.RelativeOrAbsolute);

            var factory = new ConnectionFactory
            {
                HostName = rabbitUri.Host,
                Port = rabbitUri.Port,
                //SocketReadTimeout = 5,
                //ContinuationTimeout = new TimeSpan(0, 0, 5),
                RequestedConnectionTimeout = 15000,
                NetworkRecoveryInterval =
                    new TimeSpan(0, 0, 30),
                AutomaticRecoveryEnabled = true,
                UseBackgroundThreadsForIO = true
            };

            var stopwatch = Stopwatch.StartNew();

            try
            {
                log.Info($"Trying to connect to rabbit mq: {rabbitUri}");
                var conn = factory.CreateConnection();
                log.Info($"Created connection to rabbit mq: {rabbitUri}");
                conn.Dispose();

                var ch = conn.CreateModel();

                

               
            }
            catch (Exception e)
            {
                log.Error($"Failed to create rabbit mq connection: {rabbitUri}", e);
            }

            stopwatch.Stop();
            log.Info($"Was waiting for response for {stopwatch.Elapsed.Seconds} seconds.");
            log.Info("Application ended");
            Console.ReadLine();
        }
    }
}
