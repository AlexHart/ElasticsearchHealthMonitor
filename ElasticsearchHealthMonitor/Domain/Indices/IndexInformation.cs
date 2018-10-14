using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticsearchHealthMonitor
{

    public class IndexInformation
    {

        public string Health { get; }
        public IndexStatus Status { get; }
        public string IndexName { get; }
        public string Uuid { get; }
        public int PrimaryShards { get; }
        public int ReplicatedShards { get; }
        public int DocumentsCount { get; }
        public int DocumentsDeleted { get; }
        public string StoreSize { get; }
        public string StoreSizePrimary { get; }

        public IndexInformation(string health, IndexStatus status, string indexName, string uuid,
            int primaryShards, int replicatedShards, int documentsCount, int documentsDeleted,
            string storeSize, string storeSizePrimary)
        {
            Health = health;
            Status = status;
            IndexName = indexName;
            Uuid = uuid;
            PrimaryShards = primaryShards;
            ReplicatedShards = replicatedShards;
            DocumentsCount = documentsCount;
            DocumentsDeleted = documentsDeleted;
            StoreSize = storeSize;
            StoreSizePrimary = storeSizePrimary;
        }

    }
}
