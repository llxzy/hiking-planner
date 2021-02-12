using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Models.TripLocationModels;
using DataAccessLayer.Enums;

namespace Application.Models.LocationModels
{
    public class LocationModel : BaseModel
    {
        public string                  Name             { get; set; }
        public LocationType            Type             { get; set; }
        public double                  Lat              { get; set; }
        public double                  Long             { get; set; }
        public bool                    PermanentlyAdded { get; set; }
        public int                     VisitCount       { get; set; }
        public List<TripLocationModel> Trips            { get; set; }
    }
}
