using BusinessLayer.DataTransferObjects.QueryDTOs;

namespace BusinessLayer.DataTransferObjects.Filters
{
    public class ReviewFilterDto : FilterDtoBase
    {
        public string ReviewedTripId { get; set; }
        public string AuthorId       { get; set; }
    }
}
