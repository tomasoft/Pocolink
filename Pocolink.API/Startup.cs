using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pocolink.API.Services;
using Pocolink.DAL.Client;
using Pocolink.DAL.Services;

namespace Pocolink.API
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
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });

            services.AddSingleton<IUrlRetrievalService>(_ => new UrlRetrievalService(new DataProviderService(new MongoDbClient(new KeyVaultService()),new KeyVaultService())));
            services.AddSingleton<IValidationService>(_ => new ValidationService());
            services.AddSingleton<IHashingService>(_ => new HashingService());
            services.AddSingleton<IShorteningService, ShorteningService>(_ => new ShorteningService(new HashingService(), new UrlRetrievalService(new DataProviderService(new MongoDbClient(new KeyVaultService()), new KeyVaultService())), new DataProviderService(new MongoDbClient(new KeyVaultService()), new KeyVaultService())));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
