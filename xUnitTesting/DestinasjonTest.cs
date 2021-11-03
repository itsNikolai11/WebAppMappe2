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
    public class DestinasjonTest
    {
        private const string _loggetInn = "loggetInn";
        private const string _ikkeLoggetInn = "";

        private readonly Mock<IDestinasjonRepository> mockRep = new();
        private readonly Mock<ILogger<DestinasjonController>> mockLog = new();

        private readonly Mock<HttpContext> mockHttpContext = new();
        private readonly MockHttpSession mockSession = new();

        [Fact]
        public async Task HentAlleDestinasjonerLoggetInnOK()
        {
            //Arrange 
            var dest1 = new Destinasjon
            {
                Id = 1,
                Sted = "Helsinki",
                Land = "Finland"
            };

            var dest2 = new Destinasjon
            {
                Id = 2,
                Sted = "Gøteborg",
                Land = "Danmark"
            };

            var destListe = new List<Destinasjon>
            {
                dest1,
                dest2
            };


            mockRep.Setup(d => d.HentAlleDestinasjoner()).ReturnsAsync(destListe);

            var destinasjonController = new DestinasjonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            destinasjonController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await destinasjonController.HentAlleDestinasjoner() as OkObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.OK,resultat.StatusCode);
            Assert.Equal<List<Destinasjon>>((List<Destinasjon>)resultat.Value, destListe);
        }

        
        [Fact]
        public async Task HentAlleDestinasjonerLoggetInnOKFeilDB()
        {

            var destListe = new List<Destinasjon>();

            mockRep.Setup(d => d.HentAlleDestinasjoner()).ReturnsAsync(()=>null);

            var destinasjonController = new DestinasjonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            destinasjonController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await destinasjonController.HentAlleDestinasjoner() as NotFoundObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Henting av alle destinasjoner fungerte ikke", resultat.Value);
        }

        [Fact]
        public async Task HentAlleDestinasjonerLoggetInnFeilOKDB()
        {
            //Arrange

            mockRep.Setup(d => d.HentAlleDestinasjoner()).ReturnsAsync(It.IsAny<List<Destinasjon>>());

            var destinasjonController = new DestinasjonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            destinasjonController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await destinasjonController.HentAlleDestinasjoner() as UnauthorizedObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }



        [Fact]
        public async Task LagreDestinasjonLoggetInnOk()
        {
            //Arrange

            mockRep.Setup(d => d.LagreDestinasjon(It.IsAny<Destinasjon>())).ReturnsAsync(true);

            var destinasjonController = new DestinasjonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            destinasjonController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await destinasjonController.LagreDestinasjon(It.IsAny<Destinasjon>()) as OkObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Destinasjon lagret", resultat.Value);
        }
    }
}
