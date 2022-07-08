using SoReserva.Services;
using SoReserva.Data;
using System;
using Xunit;
using AutoFixture;
using SoReserva.Interface;
using TestProject1.Fixture;

namespace TestProject1
{
    public class UnitTest1 : IClassFixture<BSFixture>
    {
        //Setup
        private readonly IBookingService _bookingService;

        public UnitTest1(BSFixture bs)
        {
            _bookingService = bs;
        }

        [Fact]
        public void Test1() 
        {
            var fixture = new Fixture();    


            bool a = _bookingService.PodeCriarReserva(DateTime.Today.AddDays(-1), DateTime.Today.AddDays(3));
            Assert.False(a);
        } 
    }
}

