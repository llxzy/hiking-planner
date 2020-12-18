using BusinessLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Models.TripModels
{
    public class TripListDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public UserDto Author { get; set; }

        public List<TripLocationDto> TripLocations { get; set; }
        public DateTime StartDate { get; set; }

        public bool Done { get; set; }


    }
}
