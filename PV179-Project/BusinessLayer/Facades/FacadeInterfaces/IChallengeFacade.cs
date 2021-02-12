using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using DataAccessLayer.Enums;

namespace BusinessLayer.Facades.FacadeInterfaces
{
    public interface IChallengeFacade : IDisposable
    {
        Task<ChallengeDto> GetAsync(int id);

        Task Update(ChallengeDto challengeDto);

        Task DeleteAsync(int challengeId);

        Task FinishChallengeAsync(int challengeId);

        List<ChallengeDto> ListAllUsersChallenges(int userId);

        List<ChallengeDto> ListSortedByType(int userId, ChallengeType type);

        List<ChallengeDto> ListFinishedChallenges(int userId);

        Task CreateAsync(int count, int userId, ChallengeType type);



    }
}