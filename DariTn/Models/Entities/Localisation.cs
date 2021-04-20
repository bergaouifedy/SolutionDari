using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DariTn.Models.Entities
{
    public class Localisation
    {
        public int id { get; set; }
        public int latitude { get; set; }
        public int longtitude { get; set; }


        public int? AssetID { get; set; }
        public virtual AssetAdv asset { get; set; }
    }
}