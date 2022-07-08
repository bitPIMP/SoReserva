using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Reflection;
using Xunit;


namespace TesteSeleniumSoReserva
{
    
    public class t1
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
    }
}
