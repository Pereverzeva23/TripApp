using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TripApp.Models
{
    public class Sights
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        public string description { get; set; }
        [Required]
        public TimeSpan house_workTo { get; set; }
        [Required]
        public TimeSpan house_workFrom { get; set; }
        [Required]
        public float rating { get; set; }

        //Foreign Key
        [Required]
        public int AddressBuildingId { get; set; }

        //Navigation property
        //public Addresses Address { get; set; }

        public Sights()
        {

        }

        public Sights(int id, string name, TimeSpan workTo, TimeSpan workFrom, float rating)
        {
            this.id = id;
            this.name = name;
            this.house_workTo = workTo;
            this.house_workFrom = workFrom;
            this.rating = rating;
        }

        public Sights(int id, string name, string description, TimeSpan workTo, TimeSpan workFrom, float rating, int addressBuildingId)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.house_workTo = workTo;
            this.house_workFrom = workFrom;
            this.rating = rating;
            this.AddressBuildingId = addressBuildingId;
        }
    }
}
