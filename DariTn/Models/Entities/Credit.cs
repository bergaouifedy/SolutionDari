using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DariTn.Models.Entities
{
    public class Credit
    {
        public int id { get; set; }
		public float initialamount { get; set; }
		public float finalamount { get; set; }
		public float monthly { get; set; }
		public CreditFormula creditformula { get; set; }
		public Client client { get; set; }
	}
}