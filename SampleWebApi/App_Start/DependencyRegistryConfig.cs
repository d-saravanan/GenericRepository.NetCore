using Autofac;
//using Autofac.Extras.DynamicProxy;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using GenericRepository.EntityFramework.SampleCore;
using GenericRepository.EntityFramework.SampleCore.Entities;
using GenericRepository.EntityFramework.SampleCore.Services;
using GenericRepository.EntityFramework.SampleWebApi.Intercetors;
using GenericService.ServiceLogger;
using Microsoft.Extensions.DependencyInjection;

namespace GenericRepository.EntityFramework.SampleWebApi.App_Start
{
    public class DependencyRegistryConfig
    {
        public static AutofacServiceProvider RegisterDependencies(IContainer appContainer, IServiceCollection services, IMapper mapper)
        {
            var builder = new ContainerBuilder();

            // Register Cross cutting concerns. Make this first always 
            RegisterXCuttingConcerns(builder);

            // Register REST Api Controllers
            RegisterApiControllers(builder);

            // Register IEntitiesContext
            RegisterContexts(builder);

            // Register repositories
            RegisterRepositories(builder);

            // Register Services
            RegisterServices(builder);

            // Register the interceptors
            RegisterInterceptors(builder);

            // Register IMappingEngine
            RegisterMappers(mapper, builder);

            builder.Populate(services);

            appContainer = builder.Build();

            return new AutofacServiceProvider(appContainer);
        }

        private static void RegisterInterceptors(ContainerBuilder builder)
        {
            //builder.RegisterType<MethodInterceptors>().As<Castle.DynamicProxy.IInterceptor>().InstancePerLifetimeScope();
            //builder.Register(_ => new MethodInterceptors(new PerfLogger()));
        }

        private static void RegisterXCuttingConcerns(ContainerBuilder builder)
        {
            builder.RegisterType<PerfLogger>().As<ILogger>();
        }

        private static void RegisterApiControllers(ContainerBuilder builder)
        {
            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
        }

        private static void RegisterMappers(IMapper mapper, ContainerBuilder builder)
        {
            builder.Register(_ => mapper).As<IMapper>().SingleInstance();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<CountryService>()
                            .As<GenericService.Services.GenericService<Country, int>>().InstancePerRequest();
            //.EnableClassInterceptors()
            //.InterceptedBy(typeof(MethodInterceptors));
        }

        private static void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<EntityRepository<Country>>()
                               .As<IEntityRepository<Country, int>>().InstancePerRequest();
            builder.RegisterType<EntityRepository<Resort>>()
                   .As<IEntityRepository<Resort>>().InstancePerRequest();
            builder.RegisterType<EntityRepository<Hotel>>()
                   .As<IEntityRepository<Hotel>>().InstancePerRequest();
        }

        private static void RegisterContexts(ContainerBuilder builder)
        {
            var tempConfig = new Microsoft.EntityFrameworkCore.DbContextOptions<AccommodationEntities>();
            builder.Register(_ => new AccommodationEntities(tempConfig))
                               .As<IEntitiesContext>().InstancePerRequest();
        }
    }
}