using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DariTn.Models.Entities
{
    public class CreditFormula
    {
        public int id { get; set; }

        public int duration { get; set; }

	    public double interestRate { get; set; }

		public Bank bank { get; set; }

        public string afficher()
        {
            return "{duration: " + duration + "\n interest rate: " + interestRate + "}";
        }


    }
}