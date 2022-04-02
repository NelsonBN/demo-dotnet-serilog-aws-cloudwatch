﻿using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Demo.WebAPI.Setups;

internal static class LoggerSetup
{
    public static LoggerConfiguration Configure(this HostBuilderContext context, LoggerConfiguration loggerConfiguration, IServiceProvider serviceProvider)
    {
        loggerConfiguration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(serviceProvider);

        return loggerConfiguration;
    }


    public static IApplicationBuilder UseSerilog(this IApplicationBuilder app)
    {
        app.UseSerilogRequestLogging(c => c.EnrichDiagnosticContext = _enrichFromRequest);
        app.UseMiddleware<LoggerMiddleware>();

        return app;
    }


    private static void _enrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
        => diagnosticContext.Set("CorrelationId", httpContext.GetCorrelationId());

    internal static string GetCorrelationId(this HttpContext httpContext)
        => Activity.Current?.Id ?? httpContext?.TraceIdentifier;
}
