using System;
using SoReserva.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoReserva.Services;

namespace TesteSeleniumSoReserva
{
    internal class t1Fixture : IDisposable
    {

        public t1Fixture()
        {
            #region tentativa obsoleta
            //var ctx = new SoReservaContext();
            //OBS 01: como não consigo resolver de forma direta essa dependência (i)
            //vou impementar DI no branch FixtureEInterface
            #endregion


            var bs = new BookingService(ctx);
        }
        
        public void Dispose() { }


    }
}
