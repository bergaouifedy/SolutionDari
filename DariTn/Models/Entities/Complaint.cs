using DariTn.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DariTN.Models.Entities
{
    public class Complaint
    {

        public int id { get; set; }
        public string @ref { get; set; }
        public string description { get; set; }
        public bool status { get; set; }
        public DateTime creationDate { get; set; }
        [ForeignKey("AssetAdv")]
        public int assetid { get; set; }
        public AssetAdv asset { get; set; }
        [ForeignKey("User")]
        public int userid { get; set; }
        public virtual User User { get; set; }
    }
}