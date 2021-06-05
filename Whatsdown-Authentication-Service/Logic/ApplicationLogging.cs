using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whatsdown_Authentication_Service.Logic
{
    public static class ApplicationLogging
    {
        public static ILoggerFactory LogFactory { get; } = LoggerFactory.Create(builder => { 
            builder.ClearProviders();

        // Clear Microsoft's default providers (like eventlogs and others)
        builder.AddConsole(options =>
        {
            options.IncludeScopes = true;
            options.TimestampFormat = "hh:mm:ss ";
        });

    });
public static ILogger<T> CreateLogger<T>() => LogFactory.CreateLogger<T>();
}
}
