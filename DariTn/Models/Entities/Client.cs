using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DariTn.Models.Entities
{
    public class Client : User
    {
        public Client(string firstName, string lastName, string email, string login, string password, string phoneNumber, string postalCode, string user_type, ICollection<Credit> credits) : base(firstName, lastName, email, login, password, phoneNumber, postalCode, user_type)
        {
            this.refClient = "C";
            this.Credits = credits;
        }

        public string refClient { get; set; }
        public ICollection<Credit> Credits { get; set; }
    }
}