using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoReserva.Models
{
    public class Booking
    {
        public int id { get; set; }
        public string NameOfVehicle { get; set; }
        public DateTime Beginning { get; set; }
        public DateTime Returning { get; set; }
        public Booking()
        {

        }
    }
}
