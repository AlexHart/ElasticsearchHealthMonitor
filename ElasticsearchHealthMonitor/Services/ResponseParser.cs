using ElasticsearchHealthMonitor.Domain.Cluster;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace ElasticsearchHealthMonitor.Services
{
    public static class ResponseParser
    {
        private readonly static CultureInfo numberFormatCulture = new CultureInfo("en-US");

        /// <summary>
        /// Parse the raw response from the server to a clusterinformation object.
        /// </summary>
        /// <param name="raw"></param>
        /// <returns></returns>
        public static ClusterInformation ParseClusterHealth(string raw)
        {
            if (string.IsNullOrEmpty(raw))
                throw new ArgumentNullException(nameof(raw));

            var contentSplits = raw.Split(null);
            var clusterName = contentSplits[2];
            var clusterStatus = contentSplits[3];

            int nodesCount;
            int.TryParse(contentSplits[4], NumberStyles.Any, numberFormatCulture, out nodesCount);

            int nodesDataCount;
            int.TryParse(contentSplits[5], NumberStyles.Any, numberFormatCulture, out nodesDataCount);

            int shards;
            int.TryParse(contentSplits[6], NumberStyles.Any, numberFormatCulture, out shards);

            double activeShardsPercent;
            string cleanPercentageNumber = contentSplits[13].Remove(contentSplits[13].Length - 1, 1);
            double.TryParse(cleanPercentageNumber, NumberStyles.Any, numberFormatCulture, out activeShardsPercent);

            return new ClusterInformation(clusterName, clusterStatus, raw, nodesCount, nodesDataCount, shards, activeShardsPercent);
        }

        /// <summary>
        /// Parse the raw response from the server to a IndexInformation object.
        /// </summary>
        /// <param name="raw"></param>
        /// <returns></returns>
        public static IndexInformation ParseIndexInformation(string raw)
        {
            if (string.IsNullOrEmpty(raw))
                throw new ArgumentNullException(nameof(raw));

            // Remove multiple spaces that simulate tabs in the text response.
            string rawWithoutMultipleSpaces = Regex.Replace(raw, @"\s+", " ");

            var contentSplits = rawWithoutMultipleSpaces.Split(null);

            string health = contentSplits[0];
            var status = contentSplits[1] == "open" ? IndexStatus.Open : IndexStatus.Closed;
            var indexName = contentSplits[2];
            var uuid = contentSplits[3];

            int primaryShards;
            int.TryParse(contentSplits[4], NumberStyles.Any, numberFormatCulture, out primaryShards);

            int replicatedShards;
            int.TryParse(contentSplits[5], NumberStyles.Any, numberFormatCulture, out replicatedShards);

            int documentsCount;
            int.TryParse(contentSplits[6], NumberStyles.Any, numberFormatCulture, out documentsCount);

            int documentsDeleted;
            int.TryParse(contentSplits[7], NumberStyles.Any, numberFormatCulture, out documentsDeleted);

            string storeSize = contentSplits[8];
            string storeSizePrimary = contentSplits[9];

            return new IndexInformation(health, status, indexName, uuid, 
                primaryShards, replicatedShards, documentsCount, 
                documentsDeleted, storeSize, storeSizePrimary);
        }

    }
}
