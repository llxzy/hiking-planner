using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Models.TripModels;
using Application.Models.UserModels;
using BusinessLayer.DataTransferObjects;

namespace Application.Models.ReviewModels
{
    public class ReviewModel : BaseModel
    {
        public TripModel ReviewedTrip                   { get; set; }

        public UserModel Author                         { get; set; }

        public string Text                              { get; set; }

        public int UpvoteCount                          { get; set; }

        public bool Flagged                             { get; set; }

        public int DownvoteCount                        { get; set; }

        public IList<UserReviewVoteDto> UserReviewVotes { get; set; }
    }
}
