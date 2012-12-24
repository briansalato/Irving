using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Irving.Web.ViewModels
{
    public class CreateAssetViewModel
    {
        [Required]
        public string Name { get; set; }
    }

    public class ShowAssetViewModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public List<NoteViewModel> Notes { get; set; }
    }

    public class EditAssetViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Id { get; set; }
    }

    public class RelatedAssetViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class NoteViewModel
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public int AssetId { get; set; }
        public DateTime? Date { get; set; }
    }
}