namespace BusinessLayer.DataTransferObjects
{
    public class UserTripDto : BaseDto
    {
        public UserDto UserInfoDTO { get; set; }

        public TripDto TripDTO { get; set; }
    }
}
