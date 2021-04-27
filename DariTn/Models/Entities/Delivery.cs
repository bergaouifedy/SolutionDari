using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DariTn.Models.Entities
{
    public class Delivery
    {
		public int id { get; set; }
		[Required(ErrorMessage = "You must enter the First Name")]
		public string firstName { get; set; }
		[Required(ErrorMessage = "You must enter the Last Name")]
		public string lastName { get; set; }
		[Required(ErrorMessage = "You must enter the PhoneNumber")]
		[Range(10000000,99999999, ErrorMessage = "The PhoneNumber must have 8 digits")]
		public int phoneNumber { get; set; }
		[Compare("Email", ErrorMessage = "Invalid email")]
		[Required(ErrorMessage = "You must enter the Email")]
		public string email { get; set; }
		[Required(ErrorMessage = "You must enter the Address")]
		public string address { get; set; }
		[Required(ErrorMessage = "You must enter the PostalCode")]
		[Range(1000, 9999, ErrorMessage = "The PostalCode must have 4 digits")]
		public int postalCode { get; set; }
	}
}