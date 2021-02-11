namespace API.Models
{
    public class ReviewShowModel
    {
        public TripShowModel ReviewedTrip { get; set; }
        public UserShowModel Author       { get; set; }
        public string        Text         { get; set; }
    }
}
