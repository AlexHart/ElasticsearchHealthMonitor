using ElasticsearchHealthMonitor.Domain.Cluster;
using ElasticsearchHealthMonitor.Domain.Common;
using System;
using System.Net;

namespace ElasticsearchHealthMonitor
{
    public class ClusterHealthStatus : IHealthCheck
    {

        /// <summary>
        /// In case there is an exception, if not it will be null.
        /// </summary>
        public Exception CheckException { get; }

        /// <summary>
        /// Was the Http call successful.
        /// </summary>
        public bool CheckSuccessful { get; }

        /// <summary>
        /// Status code of the http call.
        /// </summary>
        public HttpStatusCode StatusCode { get; }
        /// <summary>
        /// Cluster information.
        /// </summary>
        public ClusterInformation ClusterInformation { get; }

        /// <summary>
        /// Load the cluster info together with the http call information.
        /// </summary>
        /// <param name="checkSuccessful"></param>
        /// <param name="statusCode"></param>
        /// <param name="clusterInformation"></param>
        public ClusterHealthStatus(bool checkSuccessful, HttpStatusCode statusCode, ClusterInformation clusterInformation)
        {
            CheckSuccessful = checkSuccessful;
            StatusCode = statusCode;
            ClusterInformation = clusterInformation;
        }

        /// <summary>
        /// Constructor for when everything fails.
        /// </summary>
        /// <param name="checkException"></param>
        public ClusterHealthStatus(Exception checkException)
        {
            CheckException = checkException;
        }

    }

}
