using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DariTn.Models.Entities
{
    public class Delivery
    {
		public int id { get; set; }
		public string firstName { get; set; }
		public string lastName { get; set; }
		public int phoneNumber { get; set; }
		public string email { get; set; }
		public string address { get; set; }
		public int postalCode { get; set; }
	}
}