using DariTn.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DariTN.Models.Entities
{
    public class Media
    {
        public int id { get; set; }
        public string @ref { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public byte[] file { get; set; }
        [ForeignKey("asset")]
        public int assetadvid { get; set; }
        public AssetAdv asset { get; set; }
    }
}