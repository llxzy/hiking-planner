using Autofac;
using DataAccessLayer;
using Infrastructure.Query;
using Infrastructure.UnitOfWork;

namespace Infrastructure
{
    public class AutofacInfrastructureConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseContext>()
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterType<UnitOfWorkProvider>()
                .As<IUnitOfWorkProvider>()
                .InstancePerLifetimeScope();
           
            builder.RegisterGeneric(typeof(GenericRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerDependency();
            
            builder.RegisterGeneric(typeof(QueryBase<>))
                .As(typeof(IQuery<>))
                .InstancePerDependency();

            builder.RegisterType<UserQuery>()
                .AsSelf()
                .InstancePerDependency();
            
            builder.RegisterType<ReviewQuery>()
                .AsSelf()
                .InstancePerDependency();
            
            builder.RegisterType<TripQuery>()
                .AsSelf()
                .InstancePerDependency();
            
            builder.RegisterType<LocationQuery>()
                .AsSelf()
                .InstancePerDependency();
            
            builder.RegisterType<ChallengeQuery>()
                .AsSelf()
                .InstancePerDependency();
        }
    }
}
