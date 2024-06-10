using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TripApp.Models
{
    public class TripRoutes
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        public DateTime date { get; set; }

        //Foreign Key
        [Required]
        public int AddressToId { get; set; }
        [Required]
        public int AddressFromId { get; set; }

        //Navigation property
        //[Required]
        //public Addresses AddressTo { get; set; }
        //[Required]
        //public Addresses AddressFrom { get; set; }

        public TripRoutes()
        {

        }

        public TripRoutes(int id, string name, DateTime date, int AddressBuildingToId, int AddressBuildingFromId)
        {
            this.id = id;
            this.name = name;
            this.date = date;
            this.AddressToId = AddressBuildingToId;
            this.AddressFromId = AddressBuildingFromId;
        }
    }
}
