using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MonitoringLogs.Models
{
    public class FailureLog
    {
        public FailureLog()
        {
        }

        public FailureLog(string application, string source, string code, string message, string level, Dictionary<string, object> properties)
        {
            Application = application;
            Source = source;
            Code = code;
            Message = message;
            Level = level;
            Properties = properties;
        }

        public string CorrelationalId { get; set; } = Guid.NewGuid().ToString();
        public string TimeStamp { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffz");
        public string Application { get; set; }
        public string Source { get; set; }
        public string Code { get; set; } = null;
        public string Message { get; set; }
        public string Level { get; set; }
        public Dictionary<string, object> Properties { get; set; }

        public string SetLog(HttpContext context, string app, string source,
                string level, string message, Dictionary<string, object> parameters = null)
        {
            DefineHttpItems(context, app, source, level, message, parameters);

            var log = new FailureLog
            {
                Application = app,
                Source = source,
                Level = level,
                Code = context.Response?.StatusCode.ToString(),
                Message = message,
                Properties = parameters
            };

            var newLog = JsonSerializer.Serialize(log);

            return newLog;
        }
        public void DefineHttpItems(HttpContext context, string app, string source, 
                string level, string message, Dictionary<string, object> parameters = null)
        {
            context.Items["Application"] = app;
            context.Items["Source"] = source;
            context.Items["Level"] = level;
            context.Items["Message"] = message;
            context.Items["Properties"] = new Dictionary<string, object>()
            {
                {"Properties", parameters }
            };
        }

    }
}
