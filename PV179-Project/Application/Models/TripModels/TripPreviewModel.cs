using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Models.TripModels
{
    public class TripPreviewModel : BaseModel
    {
        public string Title       { get; set; }
        public DateTime StartDate { get; set; }
    }
}
