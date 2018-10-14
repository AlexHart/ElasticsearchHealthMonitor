using ElasticsearchHealthMonitor.Domain.Indices;
using System.Threading.Tasks;

namespace ElasticsearchHealthMonitor
{
    public interface IHealthChecker
    {
        Task<IndicesHealthStatus> CheckIndicesAsync();
        Task<ClusterHealthStatus> CheckClusterHealthAsync();
    }
}