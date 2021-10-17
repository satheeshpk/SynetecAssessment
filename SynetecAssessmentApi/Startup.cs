using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SynetecAssessmentApi.Persistence;
using SynetecAssessmentApi.Services;
using SynetecAssessmentApi.Validators;

namespace SynetecAssessmentApi
{
    public class Startup
    {
        private const string APISETTINGS_SECTIONNAME = "SynetecApiSettings";
        private readonly ApiSettings _apiSettings;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            // initialise the api settings from configuration
            _apiSettings = new ApiSettings
            {
                Title = Configuration.GetSection(APISETTINGS_SECTIONNAME)["Title"],
                Version = Configuration.GetSection(APISETTINGS_SECTIONNAME)["Version"]
            };
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                // add the fluent validation for validating incoming api messages
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CalculateBonusDtoValidator>());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(_apiSettings.Version, new OpenApiInfo { Title = _apiSettings.Title, Version = _apiSettings.Version });
            });

            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: "HrDb"));

            // add the synetec services
            services.AddSynetecServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{_apiSettings.Title} {_apiSettings.Version}"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}