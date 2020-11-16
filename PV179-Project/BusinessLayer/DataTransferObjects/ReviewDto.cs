namespace BusinessLayer.DataTransferObjects
{
    public class ReviewDto
    {
        public TripDto ReviewedTrip { get; set; }

        public UserDto Author { get; set; }

        public string Text { get; set; }
    }
}
