using API.Models;
using AutoMapper;
using BusinessLayer.DataTransferObjects;
using DataAccessLayer.DataClasses;

namespace API
{
    public static class ApiMappingConfig
    {
        public static void ConfigureMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<ReviewDto, ReviewShowModel>();
            config.CreateMap<UserDto, UserShowModel>();
            config.CreateMap<TripDto, TripShowModel>();
            config.CreateMap<TripLocationDto, TripLocationShowModel>();
            config.CreateMap<LocationDto, LocationShowModel>();
            config.CreateMap<ChallengeDto, ChallengeShowModel>();
        }
    }
}