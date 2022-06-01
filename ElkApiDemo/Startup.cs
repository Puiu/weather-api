using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System;

namespace ElkApiDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ElkApiDemo", Version = "v1" });
                c.IncludeXmlComments("ElkApiDemo.xml");
            });
            services.AddHealthChecks();

            services.AddOpenTelemetryTracing(
                        (builder) => builder
                                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("ElkApiDemo")) //name visible in APM server
                                    .AddAspNetCoreInstrumentation(
                                        (options) => options.Filter =
                                            (httpContext) =>
                                            {
                                                // only collect telemetry about HTTP GET requests
                                                return httpContext.Request.Method.Equals("GET");
                                            })
                                    .AddConsoleExporter()
                                    .AddOtlpExporter(opt =>
                                    {
                                        opt.Endpoint = new System.Uri("http://apmserver:8200/");
                                        opt.ExportProcessorType = OpenTelemetry.ExportProcessorType.Simple;
                                        opt.Headers = "";
                                    })
                        );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "ElkApiDemo v1"); c.DisplayRequestDuration(); });

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
