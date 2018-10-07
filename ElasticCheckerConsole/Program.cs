using System;
using System.Threading;
using System.Threading.Tasks;
using ElasticsearchHealthMonitor;

namespace ElasticCheckerConsole
{
    class Program
    {

        static void Main(string[] args)
        {
            string elasticClusterUrl = Environment.GetEnvironmentVariable("ELASTIC_CLUSTER");

            var elasticUri = new Uri(elasticClusterUrl);

            var connectionInfo = new ElasticsearchConnectionInfo(elasticUri);

            var checker = new HealthChecker(connectionInfo);

            var status = Task.Run(async () => 
                await checker.CheckStatusAsync()
            ).GetAwaiter().GetResult();

            // Check the status every 5 seconds.
            while (true)
            {
                if (status.CheckSuccessful)
                {
                    //Console.WriteLine(status.RawClusterHealth);
                    Console.WriteLine($"{DateTime.Now}\t{status.ClusterName}\t{status.ClusterStatus}");
                }
                else
                {
                    Console.WriteLine(status.CheckException.Message);
                }

                Thread.Sleep(5000);
            }
        }

    }
}
