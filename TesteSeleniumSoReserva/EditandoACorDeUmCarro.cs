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

namespace TesteSeleniumSoReserva
{

    public class EditandoACorDeUmCarro
    {
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

            #region Não consigo criar essa interface para injetar dependência nesse construtor
            
            ISoReservaContext m;
            var teste = new BookingService(m);            
            
            #endregion
            
            // Act
            var a = teste.PodeCriarReserva(DateTime.Today.AddDays(-1), DateTime.Today.AddDays(3));
            // teste é do tipo BookingService que oferece o método PodeCriarReserva()

            //Assert
            Assert.True(false, a);
        }            
    }
} 
