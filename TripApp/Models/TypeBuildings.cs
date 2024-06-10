using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripApp.Models
{
    public class TypeBuildings
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }

        public TypeBuildings()
        {

        }

        public TypeBuildings(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
