using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.QueryObjects;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.DataClasses;
using DataAccessLayer.Enums;
using Infrastructure;
using Infrastructure.Query;

namespace BusinessLayer.Services.Implementations
{
    public class ChallengeService :
        CrudQueryServiceBase<Challenge, ChallengeDto, ChallengeFilterDto>,
        IChallengeService
    {
        public ChallengeService(IRepository<Challenge> repository, 
            QueryObjectBase<Challenge, ChallengeDto, ChallengeFilterDto, IQuery<Challenge>> qob) : base(repository, qob)
        {
        }

        public bool SetAsFinished(int challengeId)
        {
            var challenge = GetAsync(challengeId).Result;
            challenge.Finished = true;
            Update(challenge);
            return true;
        }

        public List<ChallengeDto> ListAll(int userId)
        {
            var result = QueryObject.ExecuteQuery(new ChallengeFilterDto()
            {
                UserId = userId.ToString()
            });
            return result.Items.ToList();
        }

        public List<ChallengeDto> ListSortedByType(int userId, ChallengeType type)
        {
            var filter = new ChallengeFilterDto()
            {
                UserId = userId.ToString(),
                Type = type.ToString()
            };
            filter.SortAccordingTo = nameof(filter.Type);
            var result = QueryObject.ExecuteQuery(filter);
            return result.Items.ToList();
        }

        public List<ChallengeDto> ListFinished(int userId, bool finished)
        {
            var result = QueryObject.ExecuteQuery(new ChallengeFilterDto()
            {
                UserId = userId.ToString(),
                Finished = finished.ToString()
            });
            return result.Items.ToList();
        }
    }
}