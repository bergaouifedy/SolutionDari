using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DariTn.Models
{
    public class InsuranceEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Pack")]
        public string SelectedPackId { get; set; }
        public IEnumerable<SelectListItem> Packs { get; set; }
    }
}