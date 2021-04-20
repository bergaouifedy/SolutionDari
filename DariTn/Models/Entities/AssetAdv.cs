using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DariTn.Models.Entities
{
    public class AssetAdv
    {
        public int id { get; set; }
        public string @ref { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public DateTime addedDate { get; set; }
        public double price { get; set; }
        public double surface { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postalCode { get; set; }
        public int nbrRooms { get; set; }
        public int nbrComplaints { get; set; }
        public int nbrFloor { get; set; }
        public int floor { get; set; }
        public int nbrBathrooms { get; set; }
        public bool furnished { get; set; }
        public bool garage { get; set; }
        public bool parking { get; set; }
        public bool pool { get; set; }
        public string category { get; set; }
        public object rentType { get; set; }
        public string availability { get; set; }
        public bool status { get; set; }
        public int capacity { get; set; }
        //  public List<object> rv { get; set; }
        // public object localisation { get; set; }
        // public List<object> creneaux { get; set; }
        // public List<object> media { get; set; }
        // public List<object> guarantees { get; set; }
    }
}