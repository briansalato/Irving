using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Irving.Web.Models
{
    public class Car : DbModel
    {
        [Required]
        public string Name { get; set; }
        public int? Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public DateTime? Bought { get; set; }
        public int? InitialMileage { get; set; }
        public int? CurrentMileage { get; set; }
        public IList<Event> Events { get; set; }

        public Car()
        {
            Events = new List<Event>();
        }
    }
}