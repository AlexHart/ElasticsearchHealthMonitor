# ElasticsearchHealthMonitor
Very simple and naive library to check an Elasticsearch cluster status.

It's just to be able to check the status without the need of importing extra nuget dependencies. 
That's why it's just a http call to the cluster url health end point.

It does return this data:

```csharp

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

```
