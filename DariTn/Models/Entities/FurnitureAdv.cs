using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DariTn.Models.Entities
{
    public class FurnitureAdv
    {

        public int id { get; set; }
        [ForeignKey("User")]
        public int userid { get; set; }
        public virtual User User { get; set; }
        [Required(ErrorMessage = "You must enter the Reference")]
        public string @ref { get; set; }
        [Required(ErrorMessage = "You must enter the Name")]
        public string name { get; set; }
        public string description { get; set; }
        [Required(ErrorMessage = "You must enter the Type")]
        public string type { get; set; }
        public string addedDate { get; set; }
        [Required(ErrorMessage = "You must enter the Price")]
        [Range(1, 50000000, ErrorMessage = "The Price must be positive")]
        public double price { get; set; }
        public bool availability { get; set; }
        public bool status { get; set; }
        [DataType(DataType.Upload)]
        [Display(Name = "Upload File")]
        [Required(ErrorMessage = "Please choose file to upload.")]
        public string image { get; set; }

        

    }
}