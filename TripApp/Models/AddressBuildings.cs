using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripApp.Models
{
    public class AddressBuildings
    {
        public int id {  get; set; }
        [Required]
        public float x { get; set; }
        [Required]
        public float y { get; set; }

        //Foreign Key
        [Required]
        public int AddressId { get; set; }
        [Required]
        public int BuildingId { get; set; }

        public AddressBuildings()
        {
            
        }

        public AddressBuildings(int id, float x_coordinate, float y_coordinate, int AddressId, int BuildingId)
        {
            this.id = id;
            this.x = x_coordinate;
            this.y = y_coordinate;
            this.AddressId = AddressId;
            this.BuildingId = BuildingId;
        }
    }
}
