namespace BusinessLayer.DataTransferObjects
{
    public class UserTripDto : BaseDto
    {
        public UserDto User { get; set; }

        public TripDto Trip { get; set; }
    }
}
