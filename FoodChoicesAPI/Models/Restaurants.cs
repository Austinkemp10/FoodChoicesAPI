using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodChoicesAPI.Models
{
    public class Restaurants
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string County { get; set; }
        public Coordinate Location { get; set; }
        public string ImageName { get; set; }

    }
}
