using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DariTn.Models.Entities
{
    public class GuaranteeDocument
    {
        public int Id { get; set; }
        public byte[] ImageCin { get; set; }
        public byte[] EngagementProof { get; set; }
        public byte[] PayProof { get; set; }
        public byte[] ImageJustifPay { get; set; }
    }
}