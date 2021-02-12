using System;

namespace Application.Models.TripModels
{
    public class TripPreviewModel : BaseModel
    {
        public string   Title     { get; set; }
        public DateTime StartDate { get; set; }
    }
}
