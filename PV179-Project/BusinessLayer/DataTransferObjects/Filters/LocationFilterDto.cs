using BusinessLayer.DataTransferObjects.QueryDTOs;

namespace BusinessLayer.DataTransferObjects.Filters
{
    public class LocationFilterDto : FilterDtoBase
    {
        public string Name             { get; set; }
        public string Type             { get; set; }
        public string PermanentlyAdded { get; set; }
        public string VisitCount       { get; set; }
        public string Lat              { get; set; }
        public string Long             { get; set; }
    }
}
