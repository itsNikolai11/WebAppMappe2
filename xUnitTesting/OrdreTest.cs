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
            Assert.True((bool)resultat.Value);
        }

        [Fact]
        public async Task LagreOrdreIkkeLoggetInn()
        {
            /*----------Arrange---------*/

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

        [Fact]
        public async Task LagreOrdreDatabaseFeil()
        {
            /*----------Arrange---------*/

            var repositorymock = new Mock<IOrdreRepository>();

            repositorymock.Setup(b => b.lagreBillett(It.IsAny<Billett>())).ReturnsAsync(false);
            var ordreController = new OrdreController(repositorymock.Object, mockLogger.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ordreController.ControllerContext.HttpContext = mockHttpContext.Object;

            /*--------Act--------*/

            var resultat = await ordreController.LagreOrdre(It.IsAny<Billett>()) as BadRequestObjectResult;

            /*--------Assert--------*/

            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Lagring feilet", resultat.Value);
        }

        [Fact]
        public async Task EndreOrdreOk()
        {
            /*----------Arrange---------*/
            var repositorymock = new Mock<IOrdreRepository>();

            repositorymock.Setup(b => b.endreBillett(It.IsAny<Billett>())).ReturnsAsync(true);
            var ordreController = new OrdreController(repositorymock.Object, mockLogger.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ordreController.ControllerContext.HttpContext = mockHttpContext.Object;
            /*--------Act--------*/
            var resultat = await ordreController.EndreOrdre(It.IsAny<Billett>()) as OkObjectResult;

            /*--------Assert--------*/
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.True((bool)resultat.Value);

        }

        [Fact]
        public async Task EndreOrdreDatabaseFeil()
        {
            /*----------Arrange---------*/
            var repositorymock = new Mock<IOrdreRepository>();

            repositorymock.Setup(b => b.endreBillett(It.IsAny<Billett>())).ReturnsAsync(false);
            var ordreController = new OrdreController(repositorymock.Object, mockLogger.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ordreController.ControllerContext.HttpContext = mockHttpContext.Object;

            /*--------Act--------*/
            var resultat = await ordreController.EndreOrdre(It.IsAny<Billett>()) as NotFoundObjectResult;

            /*--------Assert--------*/
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Ordre ikke funnet", resultat.Value);
        }

        [Fact]
        public async Task EndreOrdreIkkeLoggetInn()
        {
            /*----------Arrange---------*/
            var repositorymock = new Mock<IOrdreRepository>();

            repositorymock.Setup(b => b.endreBillett(It.IsAny<Billett>())).ReturnsAsync(true);
            var ordreController = new OrdreController(repositorymock.Object, mockLogger.Object);

            mockSession[_loggetInn] = _IkkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ordreController.ControllerContext.HttpContext = mockHttpContext.Object;

            /*--------Act--------*/
            var resultat = await ordreController.EndreOrdre(It.IsAny<Billett>()) as UnauthorizedObjectResult;

            /*--------Assert--------*/
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ingen tilgang", resultat.Value);
        }

        [Fact]
        public async Task SlettOrdreOk()
        {
            /*----------Arrange---------*/
            var repositorymock = new Mock<IOrdreRepository>();

            repositorymock.Setup(b => b.slettBillett(It.IsAny<int>())).ReturnsAsync(true);
            var ordreController = new OrdreController(repositorymock.Object, mockLogger.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ordreController.ControllerContext.HttpContext = mockHttpContext.Object;

            /*--------Act--------*/
            var resultat = await ordreController.SlettOrdre(It.IsAny<int>()) as OkObjectResult;

            /*--------Assert--------*/

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.True((bool)resultat.Value);
        }

        [Fact]
        public async Task SlettOrdreDatabaseFeil()
        {
            /*----------Arrange---------*/

            var repositorymock = new Mock<IOrdreRepository>();

            repositorymock.Setup(b => b.slettBillett(It.IsAny<int>())).ReturnsAsync(false);
            var ordreController = new OrdreController(repositorymock.Object, mockLogger.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ordreController.ControllerContext.HttpContext = mockHttpContext.Object;

            /*--------Act--------*/
            var resultat = await ordreController.SlettOrdre(It.IsAny<int>()) as NotFoundObjectResult;

            /*--------Assert--------*/

            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Ordre ikke funnet", resultat.Value);


        }

        [Fact]
        public async Task SlettOrdreIkkeLoggetInn()
        {
            /*----------Arrange---------*/
            var repositorymock = new Mock<IOrdreRepository>();

            repositorymock.Setup(b => b.slettBillett(It.IsAny<int>())).ReturnsAsync(true);
            var ordreController = new OrdreController(repositorymock.Object, mockLogger.Object);

            mockSession[_loggetInn] = _IkkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ordreController.ControllerContext.HttpContext = mockHttpContext.Object;

            /*--------Act--------*/
            var resultat = await ordreController.SlettOrdre(It.IsAny<int>()) as UnauthorizedObjectResult;

            /*--------Assert--------*/

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ingen tilgang", resultat.Value);


        }

        [Fact]
        public async Task HentOrdreOk()
        {
            /*----------Arrange---------*/
            Billett billett1 = new Billett
            {
                RuteNr = 1,
                AvgangNr = 1,
                RefPers = "Ola Nordmann",
                AntallBarn = 1,
                AntallVoksen = 2
            };
            Billett billett2 = new Billett
            {
                RuteNr = 1,
                AvgangNr = 1,
                RefPers = "Ola Nordmann",
                AntallBarn = 1,
                AntallVoksen = 2
            };
            List<Billett> billetter = new List<Billett>();
            billetter.Add(billett1);
            billetter.Add(billett2);

            var repositorymock = new Mock<IOrdreRepository>();
            repositorymock.Setup(b => b.hentAlle()).ReturnsAsync(billetter);
            var ordreController = new OrdreController(repositorymock.Object, mockLogger.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ordreController.ControllerContext.HttpContext = mockHttpContext.Object;

            /*--------Act--------*/
            var resultat = await ordreController.HentOrdre() as OkObjectResult;
            /*--------Assert--------*/
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<List<Billett>>(billetter, (List<Billett>)resultat.Value);

        }

        [Fact]
        public async Task HentOrdreDatabaseFeil()
        {
            /*----------Arrange---------*/

            var repositorymock = new Mock<IOrdreRepository>();
            repositorymock.Setup(b => b.hentAlle()).ReturnsAsync(()=>null);
            var ordreController = new OrdreController(repositorymock.Object, mockLogger.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ordreController.ControllerContext.HttpContext = mockHttpContext.Object;

            /*--------Act--------*/

            var resultat = await ordreController.HentOrdre() as OkObjectResult;

            /*--------Assert--------*/
            
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Null(resultat.Value);

        }

        [Fact]
        public async Task HentOrdreIkkeLoggetInn()
        {
            /*----------Arrange---------*/

            var repositorymock = new Mock<IOrdreRepository>();
            repositorymock.Setup(b => b.hentAlle()).ReturnsAsync(It.IsAny<List<Billett>>);
            var ordreController = new OrdreController(repositorymock.Object, mockLogger.Object);

            mockSession[_loggetInn] = _IkkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ordreController.ControllerContext.HttpContext = mockHttpContext.Object;

            /*--------Act--------*/

            var resultat = await ordreController.HentOrdre() as UnauthorizedObjectResult;

            /*--------Assert--------*/

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ingen tilgang", resultat.Value);
        }

        [Fact]
        public async Task HentEnOrdreOk()
        {
            /*----------Arrange---------*/
            Billett billett1 = new Billett
            {
                RuteNr = 1,
                AvgangNr = 1,
                RefPers = "Ola Nordmann",
                AntallBarn = 1,
                AntallVoksen = 2
            };
            var repositorymock = new Mock<IOrdreRepository>();
            repositorymock.Setup(b => b.hentBillett(It.IsAny<int>())).ReturnsAsync(billett1);
            var ordreController = new OrdreController(repositorymock.Object, mockLogger.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ordreController.ControllerContext.HttpContext = mockHttpContext.Object;

            /*--------Act--------*/

            var resultat = await ordreController.HentEnOrdre(It.IsAny<int>()) as OkObjectResult;

            /*--------Assert--------*/
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal(billett1, (Billett)resultat.Value);
        }

        [Fact]
        public async Task HentEnOrdreDatabaseFeil()
        {
            /*----------Arrange---------*/
            var repositorymock = new Mock<IOrdreRepository>();
            repositorymock.Setup(b => b.hentBillett(It.IsAny<int>())).ReturnsAsync(()=>null);
            var ordreController = new OrdreController(repositorymock.Object, mockLogger.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ordreController.ControllerContext.HttpContext = mockHttpContext.Object;

            /*--------Act--------*/

            var resultat = await ordreController.HentEnOrdre(It.IsAny<int>()) as NotFoundObjectResult;

            /*--------Assert--------*/
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Ordre ikke funnet", resultat.Value);
        }

        [Fact]
        public async Task HentEnOrdreIkkeLoggetInn()
        {
            /*----------Arrange---------*/
            var repositorymock = new Mock<IOrdreRepository>();
            repositorymock.Setup(b => b.hentBillett(It.IsAny<int>())).ReturnsAsync(It.IsAny<Billett>());
            var ordreController = new OrdreController(repositorymock.Object, mockLogger.Object);

            mockSession[_loggetInn] = _IkkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ordreController.ControllerContext.HttpContext = mockHttpContext.Object;

            /*--------Act--------*/
            var resultat = await ordreController.HentEnOrdre(It.IsAny<int>()) as UnauthorizedObjectResult;

            /*--------Assert--------*/

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ingen tilgang", resultat.Value);
        }
    }
}
