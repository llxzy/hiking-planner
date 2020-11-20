
using System;
using System.Collections.Generic;
using System.Text;

using DataAccessLayer.Enums;

namespace BusinessLayer.DataTransferObjects
{
    class LocationSearchInfoDto
    {
        public string Name { get; set; }
        public LocationType Type { get; set; }
    }
}
