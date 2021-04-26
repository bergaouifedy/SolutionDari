using System;
using System.Collections.Generic;
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
        public string @ref { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public string addedDate { get; set; }
        public double price { get; set; }
        public bool availability { get; set; }
        public bool status { get; set; }
        public string image { get; set; }

    }
}