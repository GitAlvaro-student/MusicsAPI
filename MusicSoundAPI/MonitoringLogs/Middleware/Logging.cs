using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Abstractions;
using MonitoringLogs.Models;
using MonitoringLogs.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringLogs.Middleware
{
    public class Logging
    {
        private readonly RequestDelegate _next;
        private readonly IAzureService _azureLogService;

        public Logging(RequestDelegate next, IAzureService azureLogService)
        {
            _next = next;
            _azureLogService = azureLogService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var correlationId = Guid.NewGuid().ToString();
            context.Items["CorrelationId"] = correlationId;

            var stopwatch = Stopwatch.StartNew();

            // Log da requisição
            await LogRequestAsync(context, correlationId);

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await LogExceptionAsync(context, ex, correlationId);
                throw;
            }
            finally
            {
                stopwatch.Stop();
                await LogResponseAsync(context, correlationId, stopwatch.ElapsedMilliseconds);
            }
        }

        private async Task LogRequestAsync(HttpContext context, string correlationId)
        {
            var logEntry = new FailureLog
            {
                CorrelationalId = correlationId,
                Application = context.Items["Application"] as string,
                Source = context.Items["Source"] as string,
                Level = context.Items["Level"] as string,
                Message = $"HTTP {context.Request.Method} {context.Request.Path}",

                Properties = new Dictionary<string, object>
                {
                    ["RequestMethod"] = context.Request.Method,
                    ["RequestPath"] = context.Request.Path.Value,
                    ["RequestQuery"] = context.Request.QueryString.Value,
                    //["UserAgent"] = context.Request.Headers.UserAgent,
                    ["RemoteIpAddress"] = context.Connection.RemoteIpAddress?.ToString()
                }
            };

            await _azureLogService.SendLogAsync(logEntry);
        }

        private async Task LogResponseAsync(HttpContext context, string correlationId, long elapsed)
        {
            var logEntry = new FailureLog
            {
                CorrelationalId = correlationId,
                Application = context.Items["Application"] as string,
                Source = context.Items["Source"] as string,
                Level = context.Items["Level"] as string,
                Code = context.Response.StatusCode.ToString(),
                Message = context.Items["Message"] as string,
                Properties = context.Items["Properties"] as Dictionary<string, object>
            };

            await _azureLogService.SendLogAsync(logEntry);
        }

        private async Task LogExceptionAsync(HttpContext context, Exception ex, string correlationId)
        {
            var logEntry = new FailureLog
            {
                CorrelationalId = correlationId,
                Application = context.Items["Application"] as string,
                Source = context.Items["Source"] as string,
                Level = "Error",
                Code = context.Response.StatusCode.ToString(),
                Message = ex.Message,
                Properties = new Dictionary<string, object>
                {
                    ["ExceptionType"] = ex.GetType().Name,
                    ["StackTrace"] = ex.StackTrace,
                    ["RequestPath"] = context.Request.Path.Value
                }
            };

            await _azureLogService.SendLogAsync(logEntry);
        }
    }
}
