using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DariTn.Models.Entities
{
    public class InsuranceAgency
    {
        public InsuranceAgency(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        [JsonConstructor]
        public InsuranceAgency()
        {
        }

        public InsuranceAgency(int id, string name, ICollection<Pack> packs) : this(id, name)
        {
            this.packs = packs;
        }

        public int id { get; set; }
        public string name { get; set; }

        public ICollection<Pack> packs { get; set; }
    }
}