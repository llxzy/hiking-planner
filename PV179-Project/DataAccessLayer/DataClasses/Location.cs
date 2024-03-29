﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAccessLayer.Enums;

namespace DataAccessLayer.DataClasses
{
    public class Location : BaseEntity
    {
        [MaxLength(100)]
        public string                            Name             { get; set; } 
        public LocationType                      Type             { get; set; }
        public double                            Lat              { get; set; }
        public double                            Long             { get; set; }
        public int                               VisitCount       { get; set; }
        public bool                              PermanentlyAdded { get; set; }
        public virtual ICollection<TripLocation> Trips            { get; set; }
    }
}
