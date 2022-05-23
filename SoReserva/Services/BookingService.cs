using SoReserva.Data;
using SoReserva.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoReserva.Services
{
    public class BookingService
    {
        private readonly SoReservaContext _context;

        public BookingService(SoReservaContext context)
        {
            _context = context;
        }

        // vou retornar uma lista com todos agendamentos do meu banco de dados

        public List<Booking> FindAll() 
        {
            return _context.Booking.ToList();
        }
    }
}
