using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SoReserva.Interface;
using System.IO;
using System.Reflection;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace TesteSeleniumSoReserva
{
    
    public class EditandoACorDeUmCarro
    {

        private readonly IBookingService _bookingService;

        public EditandoACorDeUmCarro(IBookingService bs)
        {
            _bookingService = bs;
        }


        [Fact]
        public void TestandoInterfaceCarregaPaginaHomeEVerificaTitulo() 
        {
            //Arrange
            IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            //Act
            driver.Navigate().GoToUrl("https://localhost:44331/");

            //Assert
            Assert.Contains("Home Page - SoReserva", driver.Title);

        }

        //[Fact]
        //public void TestePodeCriarReserva() 
        //{
        //    bool a = _bookingService.PodeCriarReserva(DateTime.Today.AddDays(-1), DateTime.Today.AddDays(3));           
        //    Assert.False(a);
        //}
    }
}
