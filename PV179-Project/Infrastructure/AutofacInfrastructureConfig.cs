using System;
using Autofac;
using DataAccessLayer;
using DataAccessLayer.DataClasses;
using Infrastructure.Query;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AutofacInfrastructureConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseContext>()
                .AsSelf() //necessary?
                .InstancePerDependency();

            //unnecessary
            //builder.RegisterType<UnitOfWork.UnitOfWork>()
            //    .As<IUnitOfWork>()
            //    .SingleInstance();
                //.InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWorkProvider>()
                .As<IUnitOfWorkProvider>()
                //.SingleInstance();
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