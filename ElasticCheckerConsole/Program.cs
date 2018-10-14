using System;
using System.Threading;
using System.Threading.Tasks;
using ElasticsearchHealthMonitor;
using ElasticsearchHealthMonitor.Services;

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

            // Check the status constantly.
            while (true)
            {
                // Do the check.
                var status = Task.Run(async () =>
                    await checker.CheckClusterHealthAsync()
                ).GetAwaiter().GetResult();

                if (status.CheckSuccessful)
                {
                    //Console.WriteLine(status.RawClusterHealth);
                    Console.WriteLine($"{DateTime.Now}\t{status.ClusterInformation.ClusterName}\t{status.ClusterInformation.ClusterStatus}");
                }
                else
                {
                    Console.WriteLine(status.CheckException.Message);
                }

                var indicesStatus = Task
                    .Run(async () => await checker.CheckIndicesAsync())
                    .GetAwaiter().GetResult();

                if (indicesStatus.CheckSuccessful)
                {
                    foreach(var index in indicesStatus.Indices)
                    {
                        Console.WriteLine($"{index.IndexName} - {index.Health} - {index.Status.ToString()} - {index.PrimaryShards} - {index.StoreSize}");
                    }
                }
                else
                {
                    Console.WriteLine(status.CheckException.Message);
                }

                Console.WriteLine();

                // Sleep 5 seconds between checks.
                Thread.Sleep(5000);
            }
        }

    }
}
