using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Irving.Web.Models
{
    public class Note : DbModel
    {
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Text { get; set; }

        public DateTime? Date { get; set; }

        [Required]
        public int AssetId { get; set; }

        [ForeignKey("AssetId")]
        public Asset Asset { get; set; }
    }
}