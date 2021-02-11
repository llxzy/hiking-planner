using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Models.ReviewModels
{
    public class ReviewCreateModel
    {
        public int TripId { get; set; }
        public string Text { get; set; }
    }
}
