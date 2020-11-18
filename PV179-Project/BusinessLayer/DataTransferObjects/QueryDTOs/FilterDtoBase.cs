namespace BusinessLayer.DataTransferObjects.QueryDTOs
{
    public class FilterDtoBase
    {
        public int PageSize { get; set; }
        public int? RequestedPage { get; set; }
        public string SortAccordingTo { get; set; }
        public bool UseAscendingOrder { get; set; }
    }
}
