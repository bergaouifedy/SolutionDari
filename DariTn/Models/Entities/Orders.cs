using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DariTn.Models.Entities
{
    public class Orders
    {
		public int id { get; set; }
		public string @ref { get; set; }
		public float totalPrice { get; set; }
		public string deliveryStatus { get; set; }
		public Boolean paymentStatus { get; set; }
		public Boolean adminStatus { get; set; }
		[ForeignKey("delivery")]
		public int deliveryid { get; set; }
		public virtual Delivery delivery { get; set; }
		[ForeignKey("User")]
		public int userid { get; set; }
		public virtual User User { get; set; }
		
	}
}