using System;
using AutoMapper;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using DataAccessLayer.DataClasses;
using DataAccessLayer.Enums;
using Infrastructure.Query;

namespace BusinessLayer.QueryObjects
{
    public class ChallengeQueryObject : QueryObjectBase<Challenge, ChallengeDto, ChallengeFilterDto, IQuery<Challenge>>
    {
        public ChallengeQueryObject(IQuery<Challenge> query) : base( query)
        {
        }
        
        public override IQuery<Challenge> ApplyFilter(IQuery<Challenge> query, ChallengeFilterDto filter)
        {
            query = string.IsNullOrWhiteSpace(filter.UserId)
                ? query
                : ((ChallengeQuery) query).FilterByUserId(int.Parse(filter.UserId));
            
            query = string.IsNullOrWhiteSpace(filter.Type)
                ? query
                : ((ChallengeQuery) query).FilterByChallengeType(Enum.Parse<ChallengeType>(filter.Type));
            
            query = string.IsNullOrWhiteSpace(filter.Finished)
                ? query
                : ((ChallengeQuery) query).FilterByFinished(bool.Parse(filter.Finished));
            
            return query;
        }
    }
}