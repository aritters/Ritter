using Aritter.Infra.IoC.Containers;
using Aritter.Security.Infra.Ioc.Containers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Integration.AspNetCore;
using SimpleInjector.Integration.AspNetCore.Mvc;

namespace Aritter.API
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public ISimpleInjectorServiceContainer ServiceContainer { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            ServiceContainer = new SimpleInjectorServiceContainer();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc(config =>
            {
                config.Filters.Add(new Aritter.API.Seedwork.Filters.ErrorFilterAttribute());
            });

            // Add functionality to inject IOptions<T>
            services.AddOptions();

            // Add our Config object so it can be injected
            //services.Configure<MySettings>(Configuration.GetSection("MySettings"));

            // *If* you need access to generic IConfiguration this is **required**
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IServiceContainer>(ServiceContainer);
            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(ServiceContainer.Container as Container));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseSimpleInjectorAspNetRequestScoping(ServiceContainer.Container);

            ServiceContainer.Configure(app.ApplicationServices, container =>
            {
                container.Options.DefaultScopedLifestyle = new AspNetRequestLifestyle();
                container.Register<IConfiguration>(() => { return Configuration; }, Lifestyle.Scoped);
            });

            ServiceContainer.Container.RegisterMvcControllers(app);
            ServiceContainer.Container.Verify();

            app.UseCors(builder => builder.AllowAnyOrigin());
            app.UseMvc();
        }
    }
}
