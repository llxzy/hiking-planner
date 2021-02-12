using System;

namespace Application.Models.TripModels
{
    public class TripCreateModel
    {
        public string   Title        { get; set; }
        public string   Description  { get; set; }
        public DateTime StartDate    { get; set; }
        public bool     Done         { get; set; }
        public string   Participants { get; set; }
    }
}
