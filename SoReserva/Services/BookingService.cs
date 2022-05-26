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

        public DateTime FindById(DateTime? d_inicio, DateTime? d_devolucao)
        {
            return _context.Booking.Where(x => x.Beginning == d_inicio).Select(x => x.Beginning).SingleOrDefault();
        }

        public bool PodeCriarReserva(DateTime d_inicio, DateTime d_devolucao)
        {

            var CriticalReturningDate = _context.Booking.Where(x => x.Returning < d_inicio)
                                        .OrderByDescending(x => x.Returning)
                                        .Select(x => x.Returning)
                                        .ToList()
                                        .FirstOrDefault();

            string CriticalReturningDateString = CriticalReturningDate.ToString();// aqui está chegando só o Returning Date pq eu fiz o .Select(x => x.Returning)
            //string CriticalReturningDateString = CriticalReturningDate.Returning.ToString();
            //

            // os aluguéis são definidos por a data de retirada e a data de devolução.
            // Estou tentando inserir o período de locação entre outros dois já existentes. 
            // A data de retirada tem que ser maior que data de devolução imediatamente anterior
            // A data de devolução tem que ser menor que a data de retirada imediatamente posterior
            // Essa linha de comando faz 3 coisas:
            // 1) agrupa as datas de devolução que são menores que a data de início da locação solicitada
            // 2) Ordena do maior para o menor: a maior data é a que mais se aproxima do início da locação solicitada
            // 3) Isolo a data de devolução que ocorre antes do início da locação solicitada

            var CriticalBeginningDate = _context.Booking.Where(x => x.Beginning > d_devolucao).OrderBy(x => x.Beginning).Select(x=> x.Beginning).FirstOrDefault();
            string CriticalBeginningDateString = CriticalBeginningDate.ToString();

            //string CriticalBeginningDateString = CriticalBeginningDate.Beginning?.ToString(); //Operator ? cannot applied to operand of type DateTime 


            // any significa perguntar se o predicado é atendido! 

            // Vejo o tamanho da Lista. Se ela for vazia então estou criando a primeira reserva
            /* if Lista.Count >0 return true
             else faz  DateTime.Compare(Returning, d_inicio) && 
             
             
             
             
             */

            if (!(String.IsNullOrEmpty(CriticalReturningDateString) || String.IsNullOrEmpty(CriticalBeginningDateString))) return true; // o null == a primeira data possível no DateTime?
            // deixa a primeira reserva ser criada porque não existe nenhuma!                        

            if ((CriticalReturningDate == d_inicio) || (CriticalBeginningDate == d_devolucao)) return false;

            return false;

            


           



        }
    }
}
