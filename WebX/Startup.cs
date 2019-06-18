using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebX.BLL;
using WebX.COMMON;
using WebX.DbAccess;
using WebX.DbAccess.Interface;

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

            var redisConn = Configuration["ConnectionString:Redis:Connection"];
            var redisInstanceName = Configuration["ConnectionString:Redis:InstanceName"];
            //Session 过期时长分钟
            var sessionOutTime = Configuration.GetValue<int>("ContectionString:SessionTimeOut", 30);
            var redis = StackExchange.Redis.ConnectionMultiplexer.Connect(redisConn);
            //services.AddDataProtection().PersistKeysToRedis(redis, "DataProtection-Test-Keys");
            services.AddDistributedRedisCache(option =>
            {
                //redis 连接字符串
                option.Configuration = redisConn; //多个redis服务器：options.Configuration="地址1:端口,地址2:端口";
                //redis 实例名
                option.InstanceName = redisInstanceName;
            });

            //services.AddDistributedMemoryCache(); 使用内存缓存
            services.AddSession(options=>
            {

                options.Cookie.Name = ".AdventureWorks.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(sessionOutTime); 
                options.Cookie.HttpOnly = true; // 设置在浏览器中不能通过js获取cookie的值
            });

            #region 数据库依赖注入（连接）
            var connectionString = Configuration["ConnectionString:MySqlConnection"];
            services.AddDbContext<MySqlContext>(options => options.UseMySQL(connectionString));
            //注册数据库操作类
            services.AddScoped<IAccount,AccountBLL>();

            services.Configure<DBsetting>(Configuration.GetSection("ConnectionString"));
            //services.AddOptions().Configure<DBsetting>(Configuration);
            #endregion
            
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
            app.UseStaticFiles();// 静态文件支持
            app.UseCookiePolicy();
            app.UseSession();
            

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                        name: "Myareas",
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
