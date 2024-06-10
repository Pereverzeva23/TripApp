using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripApp.Models
{
    public class Addresses
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        /*
        [Required]
        public double x { get; set; }
        [Required]
        public double y { get; set; }
        [Required]
        public string name_street { get; set; }
        [Required]
        public string house_street { get; set; }

        //Foreign Key
        [Required]
        public int TypeBuildingId { get; set; }

        //Navigation property
        public TypeBuildings TypeBuilding { get; set; }
        */

        public Addresses()
        {

        }

        public Addresses(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        /*
        public Addresses(int id, double x, double y, string name_street, string house_street, int TypeBuildingId)
        {
            this.id = id;
            this.x = x;
            this.y = y;
            this.name_street = name_street;
            this.house_street = house_street;
            this.TypeBuildingId = TypeBuildingId;
        }
        */
    }
}
