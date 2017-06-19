using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FirstAspNetCore_Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using FirstAspNetCore_Dao;

namespace FirstAspNetCore_API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ////// Add framework services.
            //services.AddMvc();

            // Add framework services.
            var unitOfWork = new UnitOfWork($"{Configuration["ConnectionStrings:AutoSpy"]}");
            services.AddTransient<IUnitOfWork>(uow => unitOfWork);

            //var appSettings = Configuration.GetSection("AppSettings");
            //services.Configure<AppSettings>(appSettings);

            services.AddMvc();

            services.AddLogging();

            //services.AddSwaggerGen(sw =>
            //{
            //    sw.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "AutoSpy", Version = "v1" });
            //});
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Authorization",
            //    policy => policy.Requirements.Add(new AuthorizationRequirement(unitOfWork, appSettings)));
            //});
            //services.AddSingleton<IAuthorizationHandler, AuthorizationHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseDeveloperExceptionPage();
            app.UseMvc();
            ////if (env.IsDevelopment())
            ////{
            ////    app.UseDeveloperExceptionPage();
            ////}
            ////Add middleware
            //app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            //app.UseMvcWithDefaultRoute();

            //app.UseSwagger();
            //app.UseSwaggerUI(sw =>
            //{
            //    sw.SwaggerEndpoint("/swagger/v1/swagger.json", "AutoSpy");
            //});
        }
    }
}
