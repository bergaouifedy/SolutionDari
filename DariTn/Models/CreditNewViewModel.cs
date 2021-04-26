using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DariTn.Models
{
    public class CreditNewViewModel
    {
        [Required]
        [Display(Name = "Initial Amount")]
        public float InitialAmount { get; set; }
        [Required]
        [Display(Name = "Formula")]
        public string SelectedFormulaId { get; set; }
        public IEnumerable<SelectListItem> Formulas { get; set; }
    }
}