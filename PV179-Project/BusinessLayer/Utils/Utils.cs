using System;

namespace BusinessLayer.Utils
{
    public static class Utils
    {
        public static DateTime ParseDate(string dateString)
        {
            // format DD/MM/YYYY
            var dateSplit = dateString.Split('/');
            return new DateTime(int.Parse(dateSplit[2]), int.Parse(dateSplit[1]), int.Parse(dateSplit[0]));
        }
    }
}