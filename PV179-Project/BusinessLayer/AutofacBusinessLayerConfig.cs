using Autofac;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.QueryObjects;
using DataAccessLayer.DataClasses;
using Infrastructure;
using Infrastructure.Query;
using System.Linq;
using System.Reflection;
using BusinessLayer.DataTransferObjects.QueryDTOs;

namespace BusinessLayer
{
    public class AutofacBusinessLayerConfig : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
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
                .As<QueryObjectBase<User, UserDto, UserFilterDto, IQuery<User>>>()
                .InstancePerDependency();
            
            builder.RegisterType<ReviewQueryObject>()
                .As<QueryObjectBase<Review, ReviewDto, ReviewFilterDto, IQuery<Review>>>()
                .InstancePerDependency();
            
            builder.RegisterType<TripQueryObject>()
                .As<QueryObjectBase<Trip, TripDto, TripFilterDto, IQuery<Trip>>>()
                .InstancePerDependency();
            
            builder.RegisterType<LocationQueryObject>()
                .As<QueryObjectBase<Location, LocationDto, LocationFilterDto, IQuery<Location>>>()
                .InstancePerDependency();
            
            builder.RegisterType<ChallengeQueryObject>()
                .As<QueryObjectBase<Challenge, ChallengeDto, ChallengeFilterDto, IQuery<Challenge>>>()
                .InstancePerDependency();

            builder.RegisterType<UserQuery>()
                .As<IQuery<User>>()
                .InstancePerDependency();
            
            builder.RegisterType<ReviewQuery>()
                .As<IQuery<Review>>()
                .InstancePerDependency();
            
            builder.RegisterType<ChallengeQuery>()
                .As<IQuery<Challenge>>()
                .InstancePerDependency();
            
            builder.RegisterType<LocationQuery>()
                .As<IQuery<Location>>()
                .InstancePerDependency();
            
            builder.RegisterType<TripQuery>()
                .As<IQuery<Trip>>()
                .InstancePerDependency();
            
            builder.RegisterType<TripLocationQuery>()
                .As<IQuery<TripLocation>>()
                .InstancePerDependency();
            
            builder.RegisterType<TripLocationQueryObject>()
                .As<QueryObjectBase<TripLocation, TripLocationDto, FilterDtoBase, IQuery<TripLocation>>>()
                .InstancePerDependency();
            
            builder.RegisterType<UserReviewVoteQuery>()
                .As<IQuery<UserReviewVote>>()
                .InstancePerDependency();

            builder.RegisterType<UserReviewVoteQueryObject>()
                .As<QueryObjectBase<UserReviewVote, UserReviewVoteDto, FilterDtoBase, IQuery<UserReviewVote>>>()
                .InstancePerDependency();
            
            builder.RegisterType<UserTripQuery>()
                .As<IQuery<UserTrip>>()
                .InstancePerDependency();

            builder.RegisterType<UserTripQueryObject>()
                .As<QueryObjectBase<UserTrip, UserTripDto, FilterDtoBase, IQuery<UserTrip>>>()
                .InstancePerDependency();
        }
    }
}