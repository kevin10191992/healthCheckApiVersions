using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthChekExtensions
{
    public static class HealthCheckBuilderExtension
    {
        public static IHealthChecksBuilder AddGenericoHealthCheck(this IHealthChecksBuilder builder, IConfiguration configuration)
        {
            return builder.AddCheck("GenericoHealthCheck", new GenericoHealthCheck(configuration), HealthStatus.Unhealthy);
        }

    }
}
