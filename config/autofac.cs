using Autofac;
using FoodApp.Api.Data.Repository;

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

        }
    }

    }

