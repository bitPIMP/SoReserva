using SoReserva.Interface;
using SoReserva.Services;
using SoReserva.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.Fixture
{
    public class BSFixture
    {
        
        private IBookingService _bookingServicefx { get; set; }
        //Setup

        
        public BSFixture(IBookingService bs)
        {
            _bookingServicefx = bs;
        }
        //Tear Down
        // não implementei
       
        
    }
}
