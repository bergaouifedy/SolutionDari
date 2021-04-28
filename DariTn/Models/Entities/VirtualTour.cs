using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DariTn.Models.Entities
{
    public class VirtualTour
    {
        [Key, ForeignKey("asset")]
        public int id { get; set; }
        public string link { get; set; }
        public bool status { get; set; }
        public int assetid { get; set; }
        public virtual AssetAdv asset { get; set; }
    }
}