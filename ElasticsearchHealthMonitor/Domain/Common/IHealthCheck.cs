using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ElasticsearchHealthMonitor.Domain.Common
{
    public interface IHealthCheck
    {

        /// <summary>
        /// Was the Http call successful.
        /// </summary>
        bool CheckSuccessful { get; }

        /// <summary>
        /// Status code of the http call.
        /// </summary>
        HttpStatusCode StatusCode { get; }

        /// <summary>
        /// In case there is an exception, if not it will be null.
        /// </summary>
        Exception CheckException { get; }

    }
}
