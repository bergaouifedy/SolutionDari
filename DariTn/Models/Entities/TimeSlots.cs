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
        [Range(typeof(TimeSpan), "00:00", "23:59")]
        public TimeSpan hdebut { get; set; }
        [Range(typeof(TimeSpan), "00:00", "23:59")]
        public TimeSpan hfin { get; set; }
        public string title { get; set; }
        [ForeignKey("asset")]
        public int idasset { get; set; }
        public AssetAdv asset { get; set; }

        public virtual IEnumerable<Appointment> Appointments { get; set; }
    }
}