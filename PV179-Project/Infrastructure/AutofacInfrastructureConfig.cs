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
            builder.RegisterType<UnitOfWork.UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerDependency();
            
            builder.RegisterType<UnitOfWorkProvider>()
                .As<IUnitOfWorkProvider>()
                .InstancePerDependency();
           
            // should be fine for generics
            builder.RegisterGeneric(typeof(GenericRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerDependency();
            
            // trying generic for query, alternative is
            builder.RegisterGeneric(typeof(QueryBase<>))
                .As(typeof(IQuery<>))
                .InstancePerDependency();


            // todo delete comments before pushing final
            // queries
            // for now not added, possible to add registerype<specific query> as self()?
            // they are directly converted in queryobj base so who knows

            /*
            builder.RegisterType<ChallengeQuery>()
                .AsSelf()
                .InstancePerDependency();
            */

            // from cviko ukazka
            builder.RegisterType<DatabaseContext>()
                .AsSelf()
                .InstancePerDependency();

        }
    }
}