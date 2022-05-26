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

            //string CriticalReturningDateString = CriticalReturningDate.ToString();// aqui está chegando só o Returning Date pq eu fiz o .Select(x => x.Returning)
            #region Anotações 1

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
            #endregion
            var CriticalBeginningDate = _context.Booking.Where(x => x.Beginning > d_devolucao)
                                        .OrderBy(x => x.Beginning)
                                        .Select(x=> x.Beginning)
                                        .ToList()
                                        .FirstOrDefault();


            //string CriticalBeginningDateString = CriticalBeginningDate.ToString();

            #region Anotações 2
            //string CriticalBeginningDateString = CriticalBeginningDate.Beginning?.ToString(); //Operator ? cannot applied to operand of type DateTime 


            // any significa perguntar se o predicado é atendido! 

            // Vejo o tamanho da Lista. Se ela for vazia então estou criando a primeira reserva
            /* if Lista.Count >0 return true
             else faz  DateTime.Compare(Returning, d_inicio) && 
             */
            #endregion
            // se não existe nenhuma reserva pode fazer qualquer reserva
            // deixa a primeira reserva ser criada porque não existe nenhuma!
            // o DateTime.MinValue mata a necessidade de usar o string pra detectar nulos. 
            // o DateTime.MinValue, nesse contexto, funciona como o null
            // Se não tiver nenhuma reserva pode fazer. OK
            if ((CriticalReturningDate == DateTime.MinValue && CriticalBeginningDate == DateTime.MinValue)) return true; // o null == a primeira data possível no DateTime?


            //Se o dia de início for maior que a última devolução + 1 dia e a data de devolução é menor que o próxima dia de início -1
            if ((d_inicio >= CriticalReturningDate.AddDays(1)) && (d_devolucao <= CriticalBeginningDate.AddDays(-1))) return true;
            // parece que esse comando está resolvendo as restrições de +-1 dia mesmo quando uma dessas variáveis coincide com o dia de hoje.  

            if ((DateTime.Today >= CriticalReturningDate.AddDays(1)) && (d_devolucao <= CriticalBeginningDate.AddDays(-1))) return true;

            //if ((CriticalReturningDate == d_inicio) || (CriticalBeginningDate == d_devolucao)) return false;

            return false;

            


           



        }
    }
}
