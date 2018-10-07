using System;
using System.Net;

namespace ElasticsearchHealthMonitor
{
    public class HealthStatus
    {

        /// <summary>
        /// Was the Http call successful.
        /// </summary>
        public bool CheckSuccessful { get; }

        /// <summary>
        /// Status code of the http call.
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Name of the cluster.
        /// </summary>
        public string ClusterName { get; }

        /// <summary>
        /// Status of the cluster. (green, orange, red)
        /// </summary>
        public string ClusterStatus { get; }

        /// <summary>
        /// String returned by the elastic health endpoint.
        /// </summary>
        public string RawClusterHealth { get; }

        /// <summary>
        /// In case there is an exception, if not it will be null.
        /// </summary>
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
