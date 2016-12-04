using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using GenericRepository.EntityFramework.SampleCore.Entities;
using Microsoft.EntityFrameworkCore;
using GenericRepository.EntityFramework.SampleCore;
using Autofac;
using GenericRepository.EntityFramework.SampleWebApi.App_Start;
using AutoMapper;

namespace SampleWebApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IContainer applicationContainer { get; private set; }
        public IConfigurationRoot Configuration { get; }
        public static IMapper _mapper;

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddEntityFrameworkSqlServer();
            services.AddDbContext<AccommodationEntities>(options => options.UseSqlServer(Configuration.GetConnectionString("AccomodationEntities")));
            RegisterMappings();
            return DependencyRegistryConfig.RegisterDependencies(applicationContainer,services, _mapper);
        }

        public static void RegisterMappings()
        {
            _mapper = MapperConfig.RegisterMappings();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
