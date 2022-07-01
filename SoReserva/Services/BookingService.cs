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
        #region Das Antigas
        /*
        public bool PodeCriarReserva(DateTime d_inicio, DateTime d_devolucao)
        {
            //OK, funcionando
            if (d_devolucao < d_inicio) return false;// a data de de inicio tem que ser menor que a devolução. Não pode ser igual pq precio de 1 dia pra higienização após a devolução

            // OK, funcionando
            if (d_inicio < DateTime.Today) return false;// não é possível criar reservas no passado 


            var ClosestReturningDate = _context.Booking.Where(x => x.Returning < d_inicio)
                                        .OrderByDescending(x => x.Returning)
                                        .Select(x => x.Returning)
                                        .ToList()
                                        .FirstOrDefault();

            if (d_inicio < ClosestReturningDate) return false; // a data de início da locação não pode estar dentro do período da outra locação
            DateTime CriticalReturningDate = ClosestReturningDate.AddDays(1);
            

            var ClosestlBeginningDate = _context.Booking.Where(x => x.Beginning > d_devolucao)
                            .OrderBy(x => x.Beginning)
                            .Select(x => x.Beginning)
                            .ToList()
                            .FirstOrDefault();
            //if (d_devolucao > ClosestlBeginningDate) return false; // a data dedevolução da locação não pode estar dentro do período de outra locação

            DateTime CriticalBeginningDate = d_devolucao.AddDays(1);
            // preciso mudar o jeito de descrever essa variável porque do jeito que está dou a ideia que a lógica de formação dela é análoga a da CriticalReturningDate ?


            if ((ClosestReturningDate == DateTime.MinValue && ClosestlBeginningDate == DateTime.MinValue)) return true;//ok, se não tiver nada marcado pode criar uma reserva qualquer 


            if ((d_inicio > CriticalReturningDate) && (CriticalBeginningDate < ClosestlBeginningDate)) return true;
            // Se a "data de início do aluguel solicitado" é maior que a "data de devolução do aluguel imediatamente anterior", a "data de início do aluguel solicitado" é autorizada e resta-nos averiguar se a data de devolução é´possível
            // CriticalBeginningDate é a data de devolução do aluguel solicitado + 1 dia. Esse valor tem que ser menor que o início do aluguel imediatamente posterior.                    

            return false;
        }
        */
        #endregion

        #region Das Antiga Primóridos

        /*
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

        // se não existe nenhuma reserva pode fazer qualquer reserva
        // deixa a primeira reserva ser criada porque não existe nenhuma!
        // o DateTime.MinValue mata a necessidade de usar o string pra detectar nulos. 
        // o DateTime.MinValue, nesse contexto, funciona como o null
        // Se não tiver nenhuma reserva pode fazer. OK
        //if ((CriticalReturningDate == DateTime.MinValue && CriticalBeginningDate == DateTime.MinValue)) return true; // o null == a primeira data possível no DateTime?


        ////Se o dia de início for maior que a última devolução + 1 dia e a data de devolução é menor que o próxima dia de início -1
        //if ((d_inicio >= CriticalReturningDate.AddDays(1)) && (d_devolucao <= CriticalBeginningDate.AddDays(-1))) return true;
        //// parece que esse comando está resolvendo as restrições de +-1 dia mesmo quando uma dessas variáveis coincide com o dia de hoje.  

        //if ((DateTime.Today >= CriticalReturningDate.AddDays(1)) && (d_devolucao <= CriticalBeginningDate.AddDays(-1))) return true;

        ////if ((CriticalReturningDate == d_inicio) || (CriticalBeginningDate == d_devolucao)) return false;

        //return false;
        //}
        #endregion
    }
}
