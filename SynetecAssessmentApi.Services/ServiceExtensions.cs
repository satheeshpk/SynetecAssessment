using Microsoft.Extensions.DependencyInjection;
using SynetecAssessmentApi.Core.Services;
using SynetecAssessmentApi.Core;

namespace SynetecAssessmentApi.Services
{
    /// <summary>
    /// Service extensions repsonsible for registering synetec services.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Adds the synetec services.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public static void AddSynetecServices(this IServiceCollection services)
        {
            // add automapper objects
            services.AddAutoMapper(typeof(EmployeeMapper));

            // add synetec services
            services.AddTransient<IBonusCalculator, BonusCalculator>();
            services.AddTransient<IEmployeeService, EmployeeService>();
        }
    }
}