using System.Collections.Generic;
using DataAccessLayer.Enums;

namespace BusinessLayer.DataTransferObjects

{
    public class LocationDto : BaseDto
    {
        public string                Name             { get; set; }
        public LocationType          Type             { get; set; }
        public double                Lat              { get; set; }
        public double                Long             { get; set; }
        public bool                  PermanentlyAdded { get; set; }
        public int                   VisitCount       { get; set; }
        public List<TripLocationDto> Trips            { get; set; }
    }
}
