using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DariTn.Models.Entities
{
    public class User
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string phoneNumber { get; set; }
        public string postalCode { get; set; }
        public object role { get; set; }
        public object searchHistory { get; set; }
        public List<object> asset { get; set; }
        public List<object> complaint { get; set; }
        public List<object> boughtassets { get; set; }
    }
}