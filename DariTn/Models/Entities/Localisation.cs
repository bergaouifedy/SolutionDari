using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DariTn.Models.Entities
{
    public class Localisation
    {
        [Key, ForeignKey("asset")]
        public int id { get; set; }
        [DataType(DataType.Currency)]
        public double latitude { get; set; }
        [DataType(DataType.Currency)]
        [Range(-179.999999999999999999999999999, 180.000000000000000000000)]
        public double longtitude { get; set; }
        public int? assetid { get; set; }
        public virtual AssetAdv asset { get; set; }
    }
}