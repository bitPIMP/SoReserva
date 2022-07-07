using System;
using Xunit;
using SoReserva;
using SoReserva.Data;
using SoReserva.Models;
using SoReserva.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    public class TestingBookingService : IClassFixture<WebApplicationFactory<SoReserva.Services.BookingService>>
    {
        private readonly WebApplicationFactory<SoReserva.Services.BookingService> _factory;

        public TestingBookingService(WebApplicationFactory<SoReserva.Services.BookingService> factory)
        {
            _factory = factory;
        }

        [Fact]
        public void NaoCriaReservaParaDatasNoPassado()
        {
            //Arrange
            
            var client = _factory.CreateClient();   
            DateTime DataInicioPassado = DateTime.Today.AddDays(-1);
            DateTime DataDevolucao = DateTime.Today.AddDays(10);

            //Act
            var context = new SoReservaContext(DbContextOptions < SoReservaContext > options);
            a = new BookingService(context);
            var teste = a.PodeCriarReserva(DataInicioPassado, DataDevolucao);

            //var PodeCriarReserva = _factory.PodeCriarReserva(DataInicioPassado, DataDevolucao);

            //Assert
            //Assert.False(PodeCriarReserva);

        }

    }
}
    

