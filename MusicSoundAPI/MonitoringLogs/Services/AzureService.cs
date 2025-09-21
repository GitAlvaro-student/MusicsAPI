using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MonitoringLogs.Models;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MonitoringLogs.Services
{
    public class AzureService : IAzureService
    {
        private readonly EventHubProducerClient _eventHubClient;
        private readonly BlobContainerClient _blobContainerClient;
        private readonly ILogger<AzureService> _logger;

        public AzureService(IConfiguration configuration, ILogger<AzureService> logger)
        {
            _logger = logger;

            var eventHubConnectionString = configuration["Azure:EventHub:ConnectionString"];
            var eventHubName = configuration["Azure:EventHub:HubName"];
            _eventHubClient = new EventHubProducerClient(eventHubConnectionString, eventHubName);

            var storageConnectionString = configuration["Azure:Storage:ConnectionString"];
            var containerName = configuration["Azure:Storage:ContainerName"];
            _blobContainerClient = new BlobContainerClient(storageConnectionString, containerName);
        }

        public async Task SendLogAsync(FailureLog logEntry)
        {
            var tasks = new[]
            {
            SendLogToEventHubAsync(logEntry),
            SendLogToBlobStorageAsync(logEntry)
        };

            await Task.WhenAll(tasks);
        }

        public async Task SendLogToEventHubAsync(FailureLog logEntry)
        {
            try
            {
                var json = JsonSerializer.Serialize(logEntry);
                var eventData = new EventData(Encoding.UTF8.GetBytes(json));

                // Adicionar propriedades para roteamento
                eventData.Properties.Add("source", logEntry.Source);
                eventData.Properties.Add("level", logEntry.Level);

                var eventBatch = await _eventHubClient.CreateBatchAsync();
                eventBatch.TryAdd(eventData);

                await _eventHubClient.SendAsync(eventBatch);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao enviar log para EventHub");
            }
        }

        public async Task SendLogToBlobStorageAsync(FailureLog logEntry)
        {
            try
            {
                var json = JsonSerializer.Serialize(logEntry);
                var blobName = $"logs/{DateTime.UtcNow:yyyy/MM/dd}/{Guid.NewGuid()}.json";

                var blobClient = _blobContainerClient.GetBlobClient(blobName);
                await blobClient.UploadAsync(
                    new MemoryStream(Encoding.UTF8.GetBytes(json)),
                    overwrite: true
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao enviar log para Blob Storage");
            }
        }
    }
}
