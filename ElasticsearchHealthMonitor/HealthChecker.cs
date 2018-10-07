using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace ElasticsearchHealthMonitor
{
    /// <summary>
    /// Utility class to check the health of an elasticsearch cluster.
    /// </summary>
    public class HealthChecker
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

        /// <summary>
        /// Check the cluster status.
        /// </summary>
        /// <returns></returns>
        public async Task<HealthStatus> CheckStatusAsync()
        {
            HealthStatus result;

            try
            {
                // If there is a user specified add it to the basic authorization.
                if (!string.IsNullOrEmpty(connectionInfo.UserName) && !string.IsNullOrEmpty(connectionInfo.Password))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                            Convert.ToBase64String(
                                System.Text.UTF8Encoding.UTF8.GetBytes($"{connectionInfo.UserName}:{connectionInfo.Password}")));
                }

                var healthUri = new Uri(connectionInfo.Uri.ToString() + "_cat/health");

                var response = await httpClient.GetAsync(healthUri);

                // Get the content and split the neccesary parts.
                var content = await response.Content.ReadAsStringAsync();
                var contentSplits = content.Split(null);
                var clusterName = contentSplits[2];
                var clusterStatus = contentSplits[3];

                result = new HealthStatus(response.IsSuccessStatusCode, response.StatusCode, clusterName, clusterStatus, content);

            }
            catch (Exception ex)
            {
                result = new HealthStatus(ex);
            }
            
            return result;
        }

    }

}
