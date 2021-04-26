using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DariTn.Models.Entities
{
    public class TimeSlots
    {
        [Key]
        public int id { get; set; }
        [DataType(DataType.Date)]
        public DateTime date { get; set; }
        public TimeSpan hdebut { get; set; }
        public TimeSpan hfin { get; set; }
        public string title { get; set; }
        [ForeignKey("asset")]
        public int idasset { get; set; }
        public AssetAdv asset { get; set; }
    }
}