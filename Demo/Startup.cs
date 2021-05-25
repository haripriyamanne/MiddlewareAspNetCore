using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Hello First Middle Ware");
                await next();
                await context.Response.WriteAsync("\nMy First Middle Ware Response");
            });

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("\nHello Second Middle Ware");
                await next();
                await context.Response.WriteAsync("\nMy Second Middle Ware Response");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("\nHello World!");
                });
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.Map("/home", async context =>
                {
                    await context.Response.WriteAsync("\nHello Home Page!");
                });
            });
        }
    }
}
