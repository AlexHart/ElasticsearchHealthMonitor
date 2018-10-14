# Elasticsearch Health Monitor
Very simple and naive library to check an Elasticsearch cluster status.

It's just to be able to check the status without the need of importing extra nuget dependencies. 
That's why it's just a http call to the cluster url health end point.

It does return this data:

```csharp

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

```

And also information about the http call status.

When checking indices you get:

```csharp

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

```
