using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Irving.Web.Helpers;
using System.ComponentModel.DataAnnotations;
using Irving.Web.Validation;
using FluentValidation.Mvc;
using FluentValidation.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Irving.Web.Models
{
    [Validator(typeof(AssetValidator))]
    public class Asset : DbModel
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual Asset Parent { get; set; }
        public virtual List<Note> Notes { get; set; }
        public virtual List<Asset> Children { get; set; }
    }
}