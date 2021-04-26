using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DariTn.Models.Entities
{
    public class Insurance
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public float Price { get; set; }
        public BoughtAsset Boughtasset { get; set; }
        public Pack Pack { get; set; }
    }
}