using BusinessLayer.DataTransferObjects.QueryDTOs;

namespace BusinessLayer.DataTransferObjects.Filters
{
    public class ReviewFilterDto : FilterDtoBase
    {
        public string ReviewedTripId { get; set; }
        public string AuthorId       { get; set; }
        public string Flagged        { get; set; }
        public string Upvotes        { get; set; }
        public string Downvotes      { get; set; }
    }
}