using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NSEasyBuy.BLL;
using NSEasyBuy.Repository.Data;
using NSEasyBuy.Service;
using NSEasyBuy.Service.ProductService;
using NSEasyBuyAPI;

namespace NSEasyBuy
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
            services.Configure<ConnectionString>(Configuration.GetSection("ConnectionString"));
            services.AddTransient<IProductService,ProductService>();
            services.AddTransient<ProductManager>();
            services.AddTransient<DataAccessManager>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.Use(async (context, next) =>
            {
                logger.LogInformation("First Request Incoming");
                await next();
                logger.LogInformation("First Response Outgoing");
            });
           
            ////  app.Run(async (context) => { await context.Response.BodyWriter.WriteAsync(System.Text.Encoding.UTF8.GetBytes(System.Diagnostics.Process.GetCurrentProcess().ProcessName)); }) ;
           // app.Run(async (context) => { await context.Response.BodyWriter.WriteAsync(System.Text.Encoding.UTF8.GetBytes(Configuration["Test"])); }) ;

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
