using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApp_Mappe2.Controllers;
using WebApp_Mappe2.DAL;
using WebApp_Mappe2.Models;
using Xunit;

namespace xUnitTesting
{
    public class RuteTest
    {
        private const string _loggetInn = "loggetInn";
        private const string _ikkeLoggetInn = "";

        private readonly Mock<IRuteRepository> mockRep = new();
        private readonly Mock<ILogger<RuteController>> mockLog = new();

        private readonly Mock<HttpContext> mockHttpContext = new();
        private readonly MockHttpSession mockSession = new();


        //HentRuter

        [Fact]
        public async Task HentRuterLoggetInnOK()
        {
            //Arrange 
            var rute1 = new Rute
            {
                Id = 1,
                FraDestinasjon = "Hamburg",
                TilDestinasjon = "København",
                PrisBarn = 150,
                PrisVoksen = 250
            };

            var rute2 = new Rute
            {
                Id = 2,
                FraDestinasjon = "Helsinki",
                TilDestinasjon = "Vienna",
                PrisBarn = 160,
                PrisVoksen = 350
            };

            var ruteListe = new List<Rute>
            {
                rute1,
                rute2
            };

            mockRep.Setup(r => r.HentRuter()).ReturnsAsync(ruteListe);

            var ruteController = new RuteController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ruteController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await ruteController.HentRuter() as OkObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<List<Rute>>((List<Rute>)resultat.Value, ruteListe);
        }

        [Fact]
        public async Task HentRuterLoggetInnOKFeilDB()
        {
            //Arrange
            var ruteListe = new List<Rute>();

            mockRep.Setup(d => d.HentRuter()).ReturnsAsync(() => null);

            var ruteController = new RuteController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ruteController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await ruteController.HentRuter() as NotFoundObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Henting av alle ruter fungerte ikke", resultat.Value);
        }

        [Fact]
        public async Task HentRuterIkkeLoggetInn()
        {
            //Arrange
            mockRep.Setup(d => d.HentRuter()).ReturnsAsync(It.IsAny<List<Rute>>());

            var ruteController = new RuteController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ruteController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await ruteController.HentRuter() as UnauthorizedObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }


        //HentRute

        [Fact]
        public async Task HentEnRuteLoggetInnOK()
        {
            //Arrange 
            var rute1 = new Rute
            {
                Id = 1,
                FraDestinasjon = "Helsinki",
                TilDestinasjon = "København",
                PrisBarn = 130,
                PrisVoksen = 260
            };

            mockRep.Setup(d => d.HentRute(It.IsAny<int>())).ReturnsAsync(rute1);

            var ruteController = new RuteController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ruteController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await ruteController.HentRute(It.IsAny<int>()) as OkObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<Rute>(rute1, (Rute)resultat.Value);
        }

        [Fact]
        public async Task HentEnRuteLoggetInnIkkeOK()
        {
            //Arrange 
            mockRep.Setup(d => d.HentRute(It.IsAny<int>())).ReturnsAsync(() => null);

            var ruteController = new RuteController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ruteController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await ruteController.HentRute(It.IsAny<int>()) as NotFoundObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Fant ingen matchende rute", resultat.Value);
        }

        [Fact]
        public async Task HentEnRuteIkkeLoggetInnOK()
        {
            //Arrange 
            mockRep.Setup(d => d.HentRute(It.IsAny<int>())).ReturnsAsync(() => null);

            var ruteController = new RuteController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ruteController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await ruteController.HentRute(It.IsAny<int>()) as UnauthorizedObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        //LagreRute


        //SlettRute


        //EndreRute







    }

}
