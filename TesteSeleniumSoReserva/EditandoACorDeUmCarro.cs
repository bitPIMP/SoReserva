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
            
            // Descubro por reflection a localização do ChromeDriver
            IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            //Act
            driver.Navigate().GoToUrl("https://localhost:44331/");

            //Assert
            Assert.Contains("Home Page - SoReserva", driver.Title);           

        }

        #region Não consegui alimentar os pré-requisitos dos objetos utilizados pelo método PodeCriarReserva. Se eu criar uma mensagem de confirmação de criação de reserva eu consigo testar a funcionalidade por interface(FrontEnd) ao invés de testar por código de regra de negócio (BackEnd).
        // Mas será que ficar criando flags disparados pelo código só para testar é uma boa prática que não compromete a usabilidade, sem incomdar o usuário?
        // Acho que é boa ideia sim. Aliás, ajuda a melhorar a usabilidade. Essa necessidade de acréscimo pode ser chamada de TDD?
        // Acho que não pq não comecei com um teste mas foi a incapacidade de testar que me fez acrescentar tal funcionalidade de mostrar mensagem de sucesso


        [Fact]
        public void TestaBookingService()
        {
            //Arrange            
            IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }

            
            var teste = new BookingService(ISoReservaRepository b);
            // Não sei criar interface para injetar dependência no construtor. 
            
            public bool a = teste.PodeCriarReserva(DateTime.Today.AddDays(-1), DateTime.Today.AddDays(3)); 
        }
        
        #endregion
    }
}
