using Autofac;
using AutoMapper;
using FoodApp.Api.Data.Repository;
using FoodApp.Api.ViewModle.Profiles;

namespace FoodApp.Api.config
{
    public class autofac : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
           builder.RegisterGeneric(typeof(Repository<>)).
            As(typeof(IRepository<>)).
            InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(Program).Assembly).
                  Where(p => p.Name.EndsWith("Service")).
                  AsImplementedInterfaces().
                  InstancePerLifetimeScope();

            // Register AutoMapper
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RecipesProfile>(); // AutoMapper Profile
            })).AsSelf().SingleInstance();

            builder.Register(context =>
            {
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper();
            }).As<IMapper>().InstancePerLifetimeScope();
        }
    }

    }

