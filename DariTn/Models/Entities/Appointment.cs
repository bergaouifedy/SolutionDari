using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DariTn.Models.Entities
{
    public class Appointment
    {
        [Key]
        public int id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string place { get; set; }

        [ForeignKey("client")]
        public User client { get; set; }

        [ForeignKey("timeSlots")]
        public int? creneauid { get; set; }
        public virtual TimeSlots timeSlots { get; set; }
    }
}