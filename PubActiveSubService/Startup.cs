using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Swashbuckle.AspNetCore.Swagger;

namespace PubActiveSubService {
    public class Startup {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(swaggerGenOptions => {
                swaggerGenOptions.SwaggerDoc("v1", new Info {
                    Title = $"{nameof(PubActiveSubService)}",
                    Version = "v1",
                    Description = "Publish to Subscribers via Web Apis Host as a Http Proxy to many other Web Apis Hosts",
                    TermsOfService = "https://example.com/terms",

                    Contact = new Contact {
                        Name = "Stewart Scott Rogers",
                        Email = "Stewart.Rogers@POBox.com",
                        Url = "https://github.com/StewartScottRogers",
                    },

                    License = new License {
                        Name = "Free and open",
                        Url = "https://github.com/StewartScottRogers",
                    }
                });
            });

            services.AddIntegrationProcessors();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseSwagger();
            app.UseSwaggerUI(swagerUiOptions => {
                swagerUiOptions.SwaggerEndpoint("/swagger/v1/swagger.json", $"{nameof(PubActiveSubService)} v1");
                swagerUiOptions.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
