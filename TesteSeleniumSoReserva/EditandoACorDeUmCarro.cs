using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Reflection;
using Xunit;
using SoReserva.Services;
using SoReserva.Data;


namespace TesteSeleniumSoReserva
{
    
    public class EditandoACorDeUmCarro
    {
        [Fact]
        public void TestandoInterfaceCarregaPaginaHomeEVerificaTitulo() 
        {
            //Arrange
            
            // Descubro por reflection a localização do ChromeDriver
            IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            //Act
            driver.Navigate().GoToUrl("https://localhost:44331/");

            //Assert
            Assert.Contains("Home Page - SoReserva", driver.Title);           

        }       
    }
}
