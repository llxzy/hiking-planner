using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using DataAccessLayer.Enums;

namespace BusinessLayer.Facades.FacadeInterfaces
{
    public interface IChallengeFacade : IDisposable
    {
        Task Create(int count, int userId, ChallengeType type);

        Task<ChallengeDto> GetAsync(int id);

        Task Update(ChallengeDto challengeDto);

        Task Delete(int challengeId);

        Task FinishChallenge(int challengeId);

        List<ChallengeDto> ListAllUsersChallenges(int userId);

        List<ChallengeDto> ListSortedByType(int userId, ChallengeType type);

        List<ChallengeDto> ListFinishedChallenges(int userId);

        Task Create(int count, UserDto user, ChallengeType type);



    }
}