using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Irving.Web.Models
{
    public class Event : DbModel
    {
        public DateTime? LastOccurance { get; set; }
        public DateTime? NextOccurance { get; set; }
        [Required]
        public Recurrence Recurrence { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}