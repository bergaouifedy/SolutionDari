using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DariTn.Models.Entities
{
    public class BoughtAsset
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }

        AssetAdv Asset { get; set; }

        Client Client { get; set; }
    }
}