namespace BusinessLayer.DataTransferObjects
{
    public class UserTripDto
    {
        public int UserId { get; set; }
        public UserDto User { get; set; }
        public int TripId { get; set; }

        public TripDto Trip { get; set; }
    }
}
