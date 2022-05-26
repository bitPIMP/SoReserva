using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoReserva.Data;
using SoReserva.Models;
using SoReserva.Services;

namespace SoReserva.Controllers
{
    public class BookingsController : Controller
    {
        private readonly SoReservaContext _context;
        private readonly BookingService _bookingService;

        public BookingsController(SoReservaContext context, BookingService bookingService)
        {
            _context = context;
            _bookingService = bookingService;
        }





        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Booking.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .FirstOrDefaultAsync(m => m.id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,NameOfVehicle,Beginning,Returning")] Booking booking)
        {

            // Dúvidas
            // 01) como chamar o método PodeCriarReserva(d_início, d_devolucao) de BookingService ?
            // 02) como passar os argumentos Beginning e Returning o PodeCriarReserva? 
            // Não chama o método aqui. Tem que transferir essa estrutura para o BookingService.
            // Acho que tem que declarar tudo lá. 

            if (_bookingService.PodeCriarReserva(booking.Beginning, booking.Returning))
            {
                if (ModelState.IsValid)
                {

                    _context.Add(booking);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

            } else return RedirectToAction(nameof(Index));


            //if (ModelState.IsValid)
            //{

            //    _context.Add(booking);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}


            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,NameOfVehicle,Beginning,Returning")] Booking booking)
        {
            if (id != booking.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .FirstOrDefaultAsync(m => m.id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            _context.Booking.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.id == id);
        }
    }
}
