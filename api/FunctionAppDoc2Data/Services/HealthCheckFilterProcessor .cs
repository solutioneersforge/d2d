using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace FunctionAppDoc2Data.Services;

public class HealthCheckFilterProcessor : ITelemetryProcessor
{
    private readonly ITelemetryProcessor _next;

    public HealthCheckFilterProcessor(ITelemetryProcessor next)
    {
        _next = next;
    }

    public void Process(ITelemetry item)
    {
        // Skip health check requests entirely
        if (IsHealthCheckRequest(item))
            return;

        _next.Process(item);
    }

    private bool IsHealthCheckRequest(ITelemetry item)
    {
        return item is RequestTelemetry request &&
               request.Url.AbsolutePath.Contains("health");
    }
}
