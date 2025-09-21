using MonitoringLogs.Models;
using System.Threading.Tasks;

namespace MonitoringLogs.Services
{
    public interface IAzureService
    {
        Task SendLogAsync(FailureLog logEntry);
        Task SendLogToEventHubAsync(FailureLog logEntry);
        Task SendLogToBlobStorageAsync(FailureLog logEntry);
    }
}
