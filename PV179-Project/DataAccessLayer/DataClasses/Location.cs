using System;
using System.ComponentModel.DataAnnotations;
using DataAccessLayer.Enums;

namespace DataAccessLayer
{
    public class Location
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } 
        public LocationType Type { get; set; }
        //public Tuple<double, double> Coords { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
    }
}