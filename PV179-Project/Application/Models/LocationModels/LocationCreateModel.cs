using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Enums;
using System.Threading.Tasks;

namespace Application.Models.LocationModels
{
    public class LocationCreateModel
    {
        public string Name       { get; set; }
        public LocationType Type { get; set; }
        public double Lat        { get; set; }
        public double Long       { get; set; }
    }
}
