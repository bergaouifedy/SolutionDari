using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DariTn.Models.Entities
{
    public class Pack
    {
        [JsonConstructor]
        public Pack()
        {

        }

        public Pack(int id, string name, PackageType type, float garagePrice, float poolPrice, float appartementPrice, float housePrice, float roomPrice, float squaremeterPrice, float belongingRatio, string description, InsuranceAgency insuranceAgency)
        {
            this.id = id;
            this.name = name;
            this.type = type;
            this.garagePrice = garagePrice;
            this.poolPrice = poolPrice;
            this.appartementPrice = appartementPrice;
            this.housePrice = housePrice;
            this.roomPrice = roomPrice;
            this.squaremeterPrice = squaremeterPrice;
            this.belongingRatio = belongingRatio;
            this.description = description;
            this.insuranceAgency = insuranceAgency;
        }

        public Pack(string name, PackageType type, float garagePrice, float poolPrice, float appartementPrice, float housePrice, float roomPrice, float squaremeterPrice, float belongingRatio, string description)
        {
            this.name = name;
            this.type = type;
            this.garagePrice = garagePrice;
            this.poolPrice = poolPrice;
            this.appartementPrice = appartementPrice;
            this.housePrice = housePrice;
            this.roomPrice = roomPrice;
            this.squaremeterPrice = squaremeterPrice;
            this.belongingRatio = belongingRatio;
            this.description = description;
        }

        public int id { get; set; }

		public string name { get; set; }

		public enum PackageType 
		{
			MultirisqueshabitationSecondaire, MultirisqueshabitationPrincipale, Responsabilitcivile, Vieprivee
		}

        public PackageType type { get; set; }

		public float garagePrice { get; set; }

		public float poolPrice { get; set; }

		public float appartementPrice { get; set; }

		public float housePrice { get; set; }

		public float roomPrice { get; set; }

		public float squaremeterPrice { get; set; }

		public float belongingRatio { get; set; }

		public string description { get; set; }

        public string afficher()
        {
            return "GarageP: " + garagePrice + "\n PoolP: " + poolPrice + "\n PoolP: " + poolPrice + "\n AppartementP: " + appartementPrice + "\n type:" + type;
        }

		InsuranceAgency insuranceAgency { get; set; }
	}
}