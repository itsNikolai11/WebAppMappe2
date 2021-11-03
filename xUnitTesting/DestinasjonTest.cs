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

        //HentAlleDestinasjoner

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
            //Arrange
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

        //HentEnDestinasjon

        [Fact]
        public async Task HentEnDestinasjonLoggetInnOK()
        {
            //Arrange 
            var dest1 = new Destinasjon
            {
                Id = 1,
                Sted = "Helsinki",
                Land = "Finland"
            };

            mockRep.Setup(d => d.HentDestinasjon(It.IsAny<int>())).ReturnsAsync(dest1);

            var destinasjonController = new DestinasjonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            destinasjonController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await destinasjonController.HentDestinasjon(It.IsAny<int>()) as OkObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.OK,resultat.StatusCode);
            Assert.Equal<Destinasjon>(dest1,(Destinasjon)resultat.Value);
        }

        [Fact]
        public async Task HentEnDestinasjonLoggetInnIkkeOK()
        {
            //Arrange 
            mockRep.Setup(d => d.HentDestinasjon(It.IsAny<int>())).ReturnsAsync( ()=> null);

            var destinasjonController = new DestinasjonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            destinasjonController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await destinasjonController.HentDestinasjon(It.IsAny<int>()) as NotFoundObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Fant ikke destinasjonen", resultat.Value);
        }

        [Fact]
        public async Task HentEnDestinasjonIkkeLoggetInnOK()
        {
            //Arrange 
            mockRep.Setup(d => d.HentDestinasjon(It.IsAny<int>())).ReturnsAsync(() => null);

            var destinasjonController = new DestinasjonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            destinasjonController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await destinasjonController.HentDestinasjon(It.IsAny<int>()) as UnauthorizedObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        //LagreDestinasjon

        [Fact]
        public async Task LagreDestinasjonLoggetInnOK()
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

        [Fact]
        public async Task LagreDestinasjonLoggetInnOKDBFeil()
        {
            //Arrange
            mockRep.Setup(d => d.LagreDestinasjon(It.IsAny<Destinasjon>())).ReturnsAsync(false);

            var destinasjonController = new DestinasjonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            destinasjonController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await destinasjonController.LagreDestinasjon(It.IsAny<Destinasjon>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Destinasjon kunne ikke lagres", resultat.Value);
        }

        [Fact]
        public async Task LagreDestinasjonIkkeLoggetInn()
        {
            //Arrange
            mockRep.Setup(d => d.HentAlleDestinasjoner()).ReturnsAsync(It.IsAny<List<Destinasjon>>());

            var destinasjonController = new DestinasjonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            destinasjonController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await destinasjonController.LagreDestinasjon(It.IsAny<Destinasjon>()) as UnauthorizedObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task LagreDestinasjonLoggetInnFeilInput()
        {
            //Arrange
            var dest1 = new Destinasjon
            {
                Id = 1,
                Sted = "",
                Land = "Norge"
            };

            mockRep.Setup(d => d.LagreDestinasjon(dest1)).ReturnsAsync(true);

            var destinasjonController = new DestinasjonController(mockRep.Object, mockLog.Object);

            destinasjonController.ModelState.AddModelError("Sted", "Feil i inputvalidering ved endring av destinasjon");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            destinasjonController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await destinasjonController.LagreDestinasjon(dest1) as BadRequestObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering ved lagring av destinasjon", resultat.Value);
        }

        //SlettDestinasjon

        [Fact]
        public async Task SlettDestinasjonLoggetInnOK()
        {
            //Arrange
            mockRep.Setup(d => d.SlettDestinasjon(It.IsAny<int>())).ReturnsAsync(true);

            var destinasjonController = new DestinasjonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            destinasjonController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await destinasjonController.SlettDestinasjon(It.IsAny<int>()) as OkObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Destinasjon slettet", resultat.Value);
        }

        [Fact]
        public async Task SlettDestinasjonLoggetInnIkkeOK()
        {
            //Arrange
            mockRep.Setup(d => d.SlettDestinasjon(It.IsAny<int>())).ReturnsAsync(false);

            var destinasjonController = new DestinasjonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            destinasjonController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await destinasjonController.SlettDestinasjon(It.IsAny<int>()) as NotFoundObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Sletting av destinasjon ble ikke utført", resultat.Value);
        }

        [Fact]
        public async Task SlettDestinasjonIkkeLoggetInn()
        {
            //Arrange
            mockRep.Setup(d => d.SlettDestinasjon(It.IsAny<int>())).ReturnsAsync(true);

            var destinasjonController = new DestinasjonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            destinasjonController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await destinasjonController.SlettDestinasjon(It.IsAny<int>()) as UnauthorizedObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        //EndreDestinasjon

        [Fact]
        public async Task EndreDestinasjonLoggetInnOK()
        {
            //Arrange
            mockRep.Setup(d => d.EndreDestinasjon(It.IsAny<Destinasjon>())).ReturnsAsync(true);

            var destinasjonController = new DestinasjonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            destinasjonController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await destinasjonController.EndreDestinasjon(It.IsAny<Destinasjon>()) as OkObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Destinasjon endret", resultat.Value);
        }

        [Fact]
        public async Task EndreDestinasjonLoggetInnIkkeOK()
        {
            //Arrange
            mockRep.Setup(d => d.EndreDestinasjon(It.IsAny<Destinasjon>())).ReturnsAsync(false);

            var destinasjonController = new DestinasjonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            destinasjonController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await destinasjonController.EndreDestinasjon(It.IsAny<Destinasjon>()) as NotFoundObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Endringen kunne ikke utføres", resultat.Value);
        }

        [Fact]
        public async Task EndreDestinasjonIkkeLoggetInn()
        {
            //Arrange
            mockRep.Setup(d => d.EndreDestinasjon(It.IsAny<Destinasjon>())).ReturnsAsync(true);

            var destinasjonController = new DestinasjonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            destinasjonController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await destinasjonController.EndreDestinasjon(It.IsAny<Destinasjon>()) as UnauthorizedObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }


        [Fact]
        public async Task EndreDestinasjonLoggetInnFeilInput()
        {
            //Arrange
            var dest1 = new Destinasjon
            {
                Id = 1,
                Sted = "",
                Land = "Norge"
            };

            mockRep.Setup(d => d.EndreDestinasjon(dest1)).ReturnsAsync(true);

            var destinasjonController = new DestinasjonController(mockRep.Object, mockLog.Object);

            destinasjonController.ModelState.AddModelError("Sted", "Feil i inputvalidering ved endring av Destinasjon");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            destinasjonController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await destinasjonController.EndreDestinasjon(dest1) as BadRequestObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering ved endring av Destinasjon", resultat.Value);
        }
    }
}
