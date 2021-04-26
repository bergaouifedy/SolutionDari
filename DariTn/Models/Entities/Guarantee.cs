using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DariTn.Models.Entities
{
    public class Guarantee
    {
        public int Id { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime expirationDate { get; set; }
        public Client client { get; set; }
        public AssetAdv asset { get; set; }
        public bool status { get; set; }
        public GuaranteeDocument guaranteeDocument { get; set; }
    }
}