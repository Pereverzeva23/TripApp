using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripApp.Models
{
    public class Route
    {
        public string Name { get; set; }
        public string PointFrom { get; set; }
        public string PointTo { get; set; }
        public DateTime Date { get; set; }
        public Route(string name, string pointFrom, string pointTo, DateTime date)
        {
            Name = name;
            PointFrom = pointFrom;
            PointTo = pointTo;
            Date = date;
        }
    }
}
