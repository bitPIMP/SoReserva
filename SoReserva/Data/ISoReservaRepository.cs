using Microsoft.EntityFrameworkCore;

namespace SoReserva.Data
{
    public interface ISoReservaRepository
    {
        SoReservaContext SoReservaContext { get; set; }
    }
}
