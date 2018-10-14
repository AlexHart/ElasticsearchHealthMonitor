using ElasticsearchHealthMonitor.Domain.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ElasticsearchHealthMonitor.Domain.Indices
{
    public class IndicesHealthStatus : IHealthCheck
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
        /// Indices in the cluster.
        /// </summary>
        public IEnumerable<IndexInformation> Indices { get; }


        /// <summary>
        /// Load the list of indices and their status.
        /// </summary>
        /// <param name="checkSuccessful"></param>
        /// <param name="statusCode"></param>
        /// <param name="indices"></param>
        public IndicesHealthStatus(bool checkSuccessful, HttpStatusCode statusCode, IEnumerable<IndexInformation> indices)
        {
            CheckSuccessful = checkSuccessful;
            StatusCode = statusCode;
            Indices = indices;
        }

        /// <summary>
        /// Constructor for when everything fails.
        /// </summary>
        /// <param name="checkException"></param>
        public IndicesHealthStatus(Exception checkException)
        {
            CheckSuccessful = false;
            CheckException = checkException;
        }

    }
}
