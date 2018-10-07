using System;
using System.Net;

namespace ElasticsearchHealthMonitor
{
    public class HealthStatus
    {

        public bool CheckSuccessful { get; }

        public HttpStatusCode StatusCode { get; }

        public string ClusterName { get; }

        public string ClusterStatus { get; }

        public string RawClusterHealth { get; }

        public Exception CheckException { get; }

        /// <summary>
        /// Load the status of the http call to the elasticsearch health endpoint.
        /// </summary>
        /// <param name="checkSuccessful"></param>
        /// <param name="statusCode"></param>
        /// <param name="clusterName"></param>
        /// <param name="clusterStatus"></param>
        /// <param name="rawClusterHealth"></param>
        public HealthStatus(bool checkSuccessful, HttpStatusCode statusCode, string clusterName, string clusterStatus, 
            string rawClusterHealth)
        {
            CheckSuccessful = checkSuccessful;
            StatusCode = statusCode;
            ClusterName = clusterName;
            ClusterStatus = clusterStatus;
            RawClusterHealth = rawClusterHealth;
            CheckException = null;
        }

        /// <summary>
        /// Constructor for when everything fails.
        /// </summary>
        /// <param name="checkException"></param>
        public HealthStatus(Exception checkException)
        {
            CheckException = checkException;
        }

    }

}
