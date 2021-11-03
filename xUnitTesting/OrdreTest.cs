using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using WebApp_Mappe2.Models;
using WebApp_Mappe2.DAL;
using Moq;
using WebApp_Mappe2.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace xUnitTesting
{
    public class OrdreTest
    {
        private const string _loggetInn = "loggetInn";
        private const string _IkkeLoggetInn = "";
        private readonly Mock<ILogger<OrdreController>> mockLogger = new Mock<ILogger<OrdreController>>();
        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession();
        [Fact]
        public async Task LagreOrdreOk()
        {
            /*----------Arrange---------*/
            Billett enBillett = new Billett
            {
                RuteNr = 1,
                AvgangNr = 1,
                RefPers = "Ola Nordmann",
                AntallBarn = 1,
                AntallVoksen = 2
            };
            var repositorymock = new Mock<IOrdreRepository>();
            
            repositorymock.Setup(b => b.lagreBillett(It.IsAny<Billett>())).ReturnsAsync(true);
            var ordreController = new OrdreController(repositorymock.Object, mockLogger.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ordreController.ControllerContext.HttpContext = mockHttpContext.Object;

            /*--------Act--------*/

            var resultat = await ordreController.LagreOrdre(It.IsAny<Billett>()) as OkObjectResult;

            /*--------Assert--------*/
            Assert.Equal((int) HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Lagre ok", resultat.Value);
            



        }
        [Fact]
        public async Task LagreOrdreIkkeLoggetInn()
        {
            var repositorymock = new Mock<IOrdreRepository>();

            repositorymock.Setup(b => b.lagreBillett(It.IsAny<Billett>())).ReturnsAsync(true);
            var ordreController = new OrdreController(repositorymock.Object, mockLogger.Object);

            mockSession[_loggetInn] = _IkkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ordreController.ControllerContext.HttpContext = mockHttpContext.Object;

            /*--------Act--------*/

            var resultat = await ordreController.LagreOrdre(It.IsAny<Billett>()) as UnauthorizedObjectResult;

            /*--------Assert--------*/
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ingen tilgang", resultat.Value);
        }
        public async Task LagreOrdreIkkeOk()
        {

        }
            public async Task EndreOrdre()
        {
            /*----------Arrange---------*/

        }
        public async Task SlettOrdre()
        {
            /*----------Arrange---------*/

        }
        public async Task HentOrdre()
        {
            /*----------Arrange---------*/

        }
        public async Task HentEnOrdre()
        {
            /*----------Arrange---------*/

        }
    }
}
