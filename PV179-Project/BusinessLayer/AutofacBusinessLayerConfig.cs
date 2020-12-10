using Autofac;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.QueryObjects;
using DataAccessLayer.DataClasses;
using Infrastructure;
using Infrastructure.Query;
using System.Linq;
using System.Reflection;

namespace BusinessLayer
{
    public class AutofacBusinessLayerConfig : Autofac.Module
    {
        //public static IContainer Configure()
        protected override void Load(ContainerBuilder builder)
        {
            //var builder = new ContainerBuilder();
            builder.RegisterModule(new AutofacInfrastructureConfig());

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Namespace == "BusinessLayer.QueryObjects").AsSelf()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Namespace == "BusinessLayer.Services.Implementations")
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name))
                .InstancePerDependency();
            
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Namespace == "BusinessLayer.Facades.FacadeImplementations")
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name))
                .InstancePerDependency();

            builder.RegisterGeneric(typeof(QueryObjectBase<,,,>))
                .As(typeof(IQueryObjectBase<,,>))
                .InstancePerDependency();

            builder.RegisterType<UserQueryObject>()
                //.AsSelf().InstancePerDependency();
                .As<QueryObjectBase<User, UserDto, UserFilterDto, IQuery<User>>>()
                .InstancePerDependency();
            
            builder.RegisterType<ReviewQueryObject>()
                .AsSelf()
                .InstancePerDependency();
            
            builder.RegisterType<TripQueryObject>()
                .AsSelf()
                .InstancePerDependency();
            
            builder.RegisterType<LocationQueryObject>()
                .AsSelf()
                .InstancePerDependency();
            
            builder.RegisterType<ChallengeQueryObject>()
                .AsSelf()
                .InstancePerDependency();
            
            // register generic doesn't work with crud query :( 
            // but ^^ should work?

            // filters? dtos? probably don't need to, just a reminder to add it here if stuck 
            //return builder.Build();
        }
    }
}