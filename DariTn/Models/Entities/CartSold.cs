using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DariTn.Models.Entities
{
    public class CartSold
    {
        public int id { get; set; }
        [ForeignKey("User")]
        public int userid { get; set; }
        public virtual User User { get; set; }
        [ForeignKey("furniture")]
        public int furnitureid { get; set; }
        public FurnitureAdv furniture { get; set; }
        [ForeignKey("orders")]
        public int ordersid{ get; set; }
        public Orders order { get; set; }
    }
}