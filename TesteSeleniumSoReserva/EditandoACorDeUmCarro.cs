using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SoReserva.Services;
using SoReserva.Data;
using Microsoft.EntityFrameworkCore;
using SoReserva.Interface;


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

        [Fact]
        public void TestaBookingService()
        {            
            //Arrange     
            IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            // Act
            bool a = _bookingService.PodeCriarReserva(DateTime.Today.AddDays(-1), DateTime.Today.AddDays(3));
            // Tomei esse erro: The following constructor parameters did not have matching fixture data: IBookingService bs

            //Assert
            Assert.False(a);
        }
    }
}
