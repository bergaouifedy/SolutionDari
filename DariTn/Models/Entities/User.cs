using DariTN.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DariTn.Models.Entities
{
    public class User
    {
        public User(string firstName, string lastName, string email, string login, string password, string phoneNumber, string postalCode, string user_type)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.login = login;
            this.password = password;
            this.phoneNumber = phoneNumber;
            this.postalCode = postalCode;
            this.user_type = user_type;
        }
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string phoneNumber { get; set; }
        public string postalCode { get; set; }
        public object role { get; set; }

        public string user_type { get; set; }
        public object searchHistory { get; set; }
        public List<Complaint> complaint { get; set; }
        public List<object> boughtassets { get; set; }
    }
}