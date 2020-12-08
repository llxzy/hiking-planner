using BusinessLayer.DataTransferObjects.QueryDTOs;

namespace BusinessLayer.DataTransferObjects.Filters
{
    public class TripFilterDto : FilterDtoBase
    {
        public string StartDate { get; set; }

        public string Done { get; set; }
        
        public string AuthorId { get; set; }
        
        public string Title { get; set; }
    }
}