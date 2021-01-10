namespace BusinessLayer.DataTransferObjects
{
    public class UserTripDto
    {
        public UserDto User { get; set; }

        public TripDto Trip { get; set; }
    }
}
