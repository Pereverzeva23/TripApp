using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TripApp.Models
{
    public class Events
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        public string description { get; set; }
        [Required]
        public double cost { get; set; }

        public DateTime dateStart { get; set; }
        public DateTime dateEnd { get; set; }
        public TimeSpan timeStart { get; set; }
        public TimeSpan timeEnd { get; set; }

        //Foreign Key
        [Required]
        public int AddressBuildingId { get; set; }

        //Navigation property
        //public Addresses Address { get; set; }

        public Events()
        {

        }

        public Events(int id, string name, string description, double cost, int AddressId,
            DateTime dateStart, DateTime dateEnd, TimeSpan timeStart, TimeSpan timeEnd)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.cost = cost;
            this.dateStart = dateStart;
            this.dateEnd = dateEnd;
            this.timeStart = timeStart;
            this.timeEnd = timeEnd;
            this.AddressBuildingId = AddressId;
        }
    }
}
