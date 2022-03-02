using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Cronofly.Services;
using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cronofly
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
            services.AddControllersWithViews();

            services.AddAWSService<IAmazonDynamoDB>(Configuration.GetAWSOptions("AWS:DynamoDB"));
            services.AddSingleton<IDynamoDBContext, DynamoDBContext>();

            services.AddSingleton<ILinkRedirectionService, LinkRedirectionService>();
            services.AddSingleton<ILinkShorteningService, LinkShorteningService>();
            services.AddSingleton<ILinkSaver, LinkSaver>();
            services.AddSingleton<ILinkGetter, LinkGetter>();

            services.Configure<DbConfig>(Configuration.GetSection("DB"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
