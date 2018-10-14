using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticsearchHealthMonitor.Domain.Cluster
{
    public class ClusterInformation
    {
        public ClusterInformation(string clusterName, string clusterStatus, string rawClusterHealth, int nodesTotal, int nodesDataTotal, int shards, double activeShardsPercent)
        {
            ClusterName = clusterName;
            ClusterStatus = clusterStatus;
            RawClusterHealth = rawClusterHealth;
            NodesTotal = nodesTotal;
            NodesDataTotal = nodesDataTotal;
            Shards = shards;
            ActiveShardsPercent = activeShardsPercent;
        }

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
        /// Number of nodes.
        /// </summary>
        public int NodesTotal { get; }

        /// <summary>
        /// Number of data nodes.
        /// </summary>
        public int NodesDataTotal { get; }

        /// <summary>
        /// Number of total shards in the cluster.
        /// </summary>
        public int Shards { get; }

        /// <summary>
        /// Percentage of active percents.
        /// </summary>
        public double ActiveShardsPercent { get; }

    }
}
