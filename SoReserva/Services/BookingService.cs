using SoReserva.Data;
using SoReserva.Interface;
using SoReserva.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoReserva.Services
{
    public class BookingService : IBookingService 
    {
        private readonly SoReservaContext _context;

        // acho que eu preciso injetar essa dependência usando interface
        public BookingService(SoReservaContext context)
        {
            _context = context;
        }       


        public List<Booking> FindAll()
        {
            return _context.Booking.ToList();
        }

        public DateTime FindById(DateTime? d_inicio, DateTime? d_devolucao)
        {
            return _context.Booking.Where(x => x.Beginning == d_inicio).Select(x => x.Beginning).SingleOrDefault();
        }

        public bool PodeCriarReserva(DateTime d_inicio, DateTime d_devolucao)
        {
            //OK, funcionando
            if (d_devolucao < d_inicio) return false;// a data de de inicio tem que ser menor que a devolução. Não pode ser igual pq precio de 1 dia pra higienização após a devolução

            // OK, funcionando
            if (d_inicio < DateTime.Today) return false;// não é possível criar reservas no passado 


            // d2 = ClosestReturningDate
            var ClosestReturningDate = _context.Booking.Where(x => x.Returning < d_inicio)
                                        .OrderByDescending(x => x.Returning)
                                        .Select(x => x.Returning)
                                        .ToList()
                                        .FirstOrDefault();

            // d5 = CriticalReturningDate
            DateTime CriticalReturningDate = ClosestReturningDate.AddDays(1);


            // d3 = ClosestlBeginningDate
            var ClosestlBeginningDate = _context.Booking.Where(x => x.Beginning > d_devolucao)
                            .OrderBy(x => x.Beginning)
                            .Select(x => x.Beginning)
                            .ToList()
                            .FirstOrDefault();

            if (ClosestlBeginningDate == DateTime.MinValue) ClosestlBeginningDate = DateTime.MaxValue;
           // se não encontro nenhuma data de aluguel após o período solicitado tenho que fazer um análogo a null 
           // e esse null fica mais interessante se ele puder ficar maior que os outros valores. Na vdd ele se parece mais com o infinito positivo.
            //if (d_devolucao > ClosestlBeginningDate) return false; // a data dedevolução da locação não pode estar dentro do período de outra locação

                // d6 CriticalBeginningDate
                DateTime CriticalBeginningDate = d_devolucao.AddDays(1);

            //if ((ClosestReturningDate == DateTime.MinValue && ClosestlBeginningDate == DateTime.MinValue)) return true;//ok, se não tiver nada marcado pode criar uma reserva qualquer 

            
            if ((DateTime.Compare(ClosestlBeginningDate, CriticalBeginningDate) == 1) 
                && ((DateTime.Compare(CriticalBeginningDate, d_inicio) == 1)) 
                && (DateTime.Compare(d_inicio, CriticalReturningDate) == 1)) 
                return true;  
            
            return false;
        }       
    }
}
