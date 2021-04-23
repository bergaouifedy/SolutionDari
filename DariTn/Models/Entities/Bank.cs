using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DariTn.Models.Entities
{
    public class Bank
    {
        [JsonConstructor]
        public Bank()
        {

        }
        public Bank(int id, string name, ICollection<CreditFormula> creditFormulas)
        {
            this.id = id;
            this.name = name;
            CreditFormulas = creditFormulas;
        }

        public Bank(string name)
        {
            this.name = name;
        }

        public int id { get; set; }
        public string name { get; set; }

        public ICollection<CreditFormula> CreditFormulas { get; set; }
    }
}