using System.Collections.Generic;
using BusinessLayer.DataTransferObjects;
using DataAccessLayer.Enums;

namespace BusinessLayer.Services.Interfaces
{
    public interface IChallengeService
    {
        bool SetAsFinished(int challengeId);

        List<ChallengeDto> ListAll(int userId);

        List<ChallengeDto> ListSortedByType(int userId, ChallengeType type);

        List<ChallengeDto> ListFinished(int userId, bool finished);

    }
}