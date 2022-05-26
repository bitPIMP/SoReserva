using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoReserva.Models
{
    public class Booking
    {
        public int id { get; set; }
        [Display(Name = "Name of Vehicle")]
        public string NameOfVehicle { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime Beginning { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Returning { get; set; }
        public Booking()
        {

        }
    }
}
