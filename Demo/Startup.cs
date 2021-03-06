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
        private static void HandleMapTest1(IApplicationBuilder app) //Method for map1
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("\nMap Test 1");
            });
        }

        private static void HandleMapTest2(IApplicationBuilder app) //Method for map2
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("\nMap Test 2");
            });
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

            app.UseRouting(); //This middleware is used to route requests.   

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
                    //await context.Response.WriteAsync("\nHello Home Page!");
                    if (env.IsEnvironment("Develop"))
                    {
                        await context.Response.WriteAsync("\nCustom Environment Created");
                    }

                    else if (env.IsDevelopment())  // code to be executed in development environment 
                    {
                        await context.Response.WriteAsync("\nDevelopment Environment");
                    }

                    else if (env.IsStaging()) // code to be executed in staging environment 
                    {
                        await context.Response.WriteAsync("\nStaging Environment");
                    }

                  else if (env.IsProduction())  // code to be executed in production environment 
                    {
                        await context.Response.WriteAsync("\nProduction Environment");
                    }
                });
            });
            app.Map("/map1", HandleMapTest1);

            app.Map("/map2", HandleMapTest2);

            app.Run(async context =>
            {
                await context.Response.WriteAsync("\nPage not Existed.");
            });
            app.Run(async context =>
            {
                await context.Response.WriteAsync("\nThis is never going to run."); //will never be executed    
            });
        }
    }
}
