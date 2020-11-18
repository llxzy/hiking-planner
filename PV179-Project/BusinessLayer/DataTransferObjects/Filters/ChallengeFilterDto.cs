using System;
using BusinessLayer.DataTransferObjects.QueryDTOs;
using DataAccessLayer.Enums;

namespace BusinessLayer.DataTransferObjects.Filters
{
    public class ChallengeFilterDto : FilterDtoBase
    {
        public int UserId { get; set; }
        public ChallengeType? Type { get; set; }
        public bool Finished { get; set; }
        public DateTime StartDate { get; set; }
        
    }
}