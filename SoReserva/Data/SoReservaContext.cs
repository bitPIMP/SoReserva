using Microsoft.EntityFrameworkCore;
using SoReserva.Models;

namespace SoReserva.Data
{
    public class SoReservaContext : DbContext
    {
        public SoReservaContext(DbContextOptions<SoReservaContext> options) : base(options)
        {
        }
        public DbSet<SoReserva.Models.Booking> Booking { get; set; }

        public DbSet<SoReserva.Models.Vehicle> Vehicle { get; set; }
    } 


}




