using SoReserva.Models;
using System;
using System.Collections.Generic;

namespace SoReserva.Interface
{
    public interface IBookingService
    {


        public List<Booking> FindAll();

        public DateTime FindById(DateTime? d_inicio, DateTime? d_devolucao);

        public bool PodeCriarReserva(DateTime d_inicio, DateTime d_devolucao);       

    }
}
