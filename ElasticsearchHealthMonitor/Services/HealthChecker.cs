using ElasticsearchHealthMonitor.Domain.Indices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace ElasticsearchHealthMonitor.Services
{
    /// <summary>
    /// Utility class to check the health of an elasticsearch cluster.
    /// </summary>
    public class HealthChecker : IHealthChecker
    {
        /// <summary>
        /// Reusable httpclient.
        /// </summary>
        private static HttpClient httpClient;

        /// <summary>
        /// Information to connect to the elastic search cluster.
        /// </summary>
        private readonly ElasticsearchConnectionInfo connectionInfo;

        /// <summary>
        /// Constructor to setup the health checker. 
        /// </summary>
        /// <param name="connectionInfo"></param>
        public HealthChecker(ElasticsearchConnectionInfo connectionInfo)
        {
            this.connectionInfo = connectionInfo;

            if (httpClient == null)
                httpClient = new HttpClient();
        }

        private void AddAuthenticationToHttpClient()
        {
            // If there is a user specified add it to the basic authorization.
            if (!string.IsNullOrEmpty(connectionInfo.UserName) && !string.IsNullOrEmpty(connectionInfo.Password))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(
                            System.Text.UTF8Encoding.UTF8.GetBytes($"{connectionInfo.UserName}:{connectionInfo.Password}")));
            }
        }

        /// <summary>
        /// Check the cluster status.
        /// </summary>
        /// <returns></returns>
        public async Task<ClusterHealthStatus> CheckClusterHealthAsync()
        {
            ClusterHealthStatus result;

            try
            {
                AddAuthenticationToHttpClient();

                var healthUri = new Uri(connectionInfo.Uri.ToString() + "_cat/health");

                var response = await httpClient.GetAsync(healthUri);

                if (!response.IsSuccessStatusCode)
                {
                    //TODO: Improve the error handling.
                    result = new ClusterHealthStatus(false, response.StatusCode, null);
                }
                else
                {
                    // Get the content and split the neccesary parts.
                    var rawContent = await response.Content.ReadAsStringAsync();

                    var clusterInfo = ResponseParser.ParseClusterHealth(rawContent);

                    result = new ClusterHealthStatus(true, response.StatusCode, clusterInfo);
                }
            }
            catch (Exception ex)
            {
                result = new ClusterHealthStatus(ex);
            }
            
            return result;
        }

        /// <summary>
        /// Get all the indices information.
        /// </summary>
        /// <returns></returns>
        public async Task<IndicesHealthStatus> CheckIndicesAsync()
        {
            IndicesHealthStatus result;
            try
            {
                AddAuthenticationToHttpClient();

                var healthUri = new Uri(connectionInfo.Uri.ToString() + "_cat/indices?s=index");

                var response = await httpClient.GetAsync(healthUri);

                if (!response.IsSuccessStatusCode)
                {
                    //TODO: Improve the error handling.
                    result = new IndicesHealthStatus(false, response.StatusCode, null);
                }
                else
                {
                    // Get the content and split the neccesary parts.
                    var content = await response.Content.ReadAsStringAsync();

                    var indices = new List<IndexInformation>();
                    var lines = content.Split(new string[] { Environment.NewLine, "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var line in lines)
                    {
                        var indexInfo = ResponseParser.ParseIndexInformation(line);

                        // Exclude system indices.
                        if (!indexInfo.IndexName.StartsWith("."))
                            indices.Add(indexInfo);
                    }

                    result = new IndicesHealthStatus(true, response.StatusCode, indices);
                }
            }
            catch (Exception ex)
            {
                result = new IndicesHealthStatus(ex);
            }
            return result;
        }

    }

}
