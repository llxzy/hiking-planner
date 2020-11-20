using BusinessLayer.DataTransferObjects.QueryDTOs;
using DataAccessLayer.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.DataTransferObjects.Filters
{
    public class LocationFilterDto : FilterDtoBase
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Added { get; set; }
    }
}
