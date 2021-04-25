using DariTN.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public virtual Localisation localisation { get; set; }
        [ForeignKey("User")]
        public int userid { get; set; }
        public virtual User User { get; set; }
        // public List<object> creneaux { get; set; }
        public virtual List<Media> media { get; set; }
        // public List<object> guarantees { get; set; }
    }
}