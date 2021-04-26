using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DariTn.Models.Entities
{
    public class Localisation
    {
        public int id { get; set; }
        public float latitude { get; set; }
        public float longtitude { get; set; }
        [ForeignKey("asset")]
        public int? assetid { get; set; }
        public virtual AssetAdv asset { get; set; }
    }
}