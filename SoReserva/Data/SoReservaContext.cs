using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SoReserva.Models;

namespace SoReserva.Data
{
    public class SoReservaContext : DbContext, ISoReservaContext
    {
        public SoReservaContext (DbContextOptions<SoReservaContext> options)
            : base(options)
        {
        }

        public DbSet<SoReserva.Models.Booking> Booking { get; set; }

        public DbSet<SoReserva.Models.Vehicle> Vehicle { get; set; }
    }

    public interface ISoReservaContext
    {
        void SoReservaContext(DbContextOptions<SoReservaContext> options)
            : base(options)
        {
        }

        DbSet<Booking> Booking { get; set; }

        DbSet<Vehicle> Vehicle { get; set; }
    }

}
