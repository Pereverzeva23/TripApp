using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripApp.Models
{
    public class Buildings
    {
        public int id { get; set; }
        [Required]
        public string number { get; set; }

        public Buildings()
        {

        }

        public Buildings(int id, string number)
        {
            this.id = id;
            this.number = number;
        }
    }
}
