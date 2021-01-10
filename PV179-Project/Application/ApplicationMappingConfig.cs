using Application.Models;
using Application.Models.ChallengeModels;
using Application.Models.LocationModels;
using Application.Models.TripLocationModels;
using Application.Models.TripModels;
using Application.Models.UserModels;
using AutoMapper;
using BusinessLayer.DataTransferObjects;
using DataAccessLayer.DataClasses;

namespace Application
{
    public class ApplicationMappingConfig
    {
        public static void ConfigureMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<UserDto, UserModel>().ReverseMap();
            config.CreateMap<TripDto, TripModel>().ReverseMap();
            config.CreateMap<LocationDto, LocationModel>().ReverseMap();
            config.CreateMap<UserTripDto, UserTripModel>().ReverseMap();
            config.CreateMap<ChallengeDto, ChallengeModel>().ReverseMap();
            config.CreateMap<TripLocationDto, TripLocationModel>().ReverseMap();
            config.CreateMap<UserRegistrationDto, UserCreateModel>().ReverseMap();
        }
    }
}