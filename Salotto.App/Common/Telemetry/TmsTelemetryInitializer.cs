///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//


using System;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;

namespace Salotto.App.Common.Telemetry
{
    public class TmsTelemetryInitializer : ITelemetryInitializer
    {
        /// <summary>
        /// Name of the header to add to AppInsights
        /// </summary>
        private const string HeaderName = "Tms-Key";

        /// <summary>
        /// Internal ref to the HTTP context
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public TmsTelemetryInitializer(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        /// <summary>
        /// Adds the actual header to the telemetry response
        /// </summary>
        /// <param name="telemetry"></param>
        public void Initialize(ITelemetry telemetry)
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null)
                return;

            if (context.Request.Headers.TryGetValue(HeaderName, out var value))
            {
                telemetry.Context.GlobalProperties[HeaderName] = value.ToString();
            }
        }
    }
}


