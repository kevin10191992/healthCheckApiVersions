using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthChekExtensions
{
    public class GenericoHealthCheck : IHealthCheck
    {
        private readonly IConfiguration configuration;

        public GenericoHealthCheck(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Se puede validar lo que quieras y crear multiples clases que puedan valdiar lo que tu quieras
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            bool Parse = true;
            bool ServicioOk = bool.TryParse(configuration["ServicioOK"], out Parse);
            if (!ServicioOk || !Parse)
            {
                return HealthCheckResult.Unhealthy();
            }
            else
            {
                return HealthCheckResult.Healthy();
            }
        }
    }
}