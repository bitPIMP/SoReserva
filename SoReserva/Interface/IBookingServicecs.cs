using SoReserva.Models;
using System;
using System.Collections.Generic;

namespace SoReserva.Interface
{
    public interface IBookingServices
    {
        private readonly SoReservaContext _context;

        // acho que eu preciso injetar essa dependência usando interface
        public BookingService(SoReservaContext context)
        {
            _context = context;
        }

        // vou retornar uma lista com todos agendamentos do meu banco de dados

        public List<Booking> FindAll();
       

        public DateTime FindById(DateTime? d_inicio, DateTime? d_devolucao);
        

        bool PodeCriarReserva(DateTime d_inicio, DateTime d_devolucao);
       
       

    }
}
