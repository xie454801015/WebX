using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebX.COMMON;
using WebX.DbCONT;

namespace WebX
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {   
            //空项目要写接入appsettings.json文件
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //var connectionString = Configuration.GetConnectionString("MySqlConnection");
            var connectionString = Configuration["ConnectionString:MySqlConnection"];
            services.AddDbContext<MySqlContext>(options => options.UseMySQL(connectionString));

            services.Configure<DBsetting>(Configuration.GetSection("ConnectionString"));
            //services.AddOptions().Configure<DBsetting>(Configuration);

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");// 使用异常处理器
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseStaticFiles();// 静态文件支持
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                        name: "Myarea",
                        template: "{area:exists}/{controller=Articles}/{action=Index}/{id?}"
                    );

                routes.MapRoute(
                        name: "default",
                        template: "{Controller=Home}/{action=Index}/{id?}"
                    );
            }
            );

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
