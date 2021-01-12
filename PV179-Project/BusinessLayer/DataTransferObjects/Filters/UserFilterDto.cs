using BusinessLayer.DataTransferObjects.QueryDTOs;

namespace BusinessLayer.DataTransferObjects.Filters
{
    public class UserFilterDto : FilterDtoBase
    {
        public string Name        { get; set; }
        public string MailAddress { get; set; }
    }
}
