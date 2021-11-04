﻿using Microsoft.AspNetCore.Http;
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

        [Fact]
        public async Task LagreRuteLoggetInnOK()
        {
            //Arrange
            mockRep.Setup(d => d.LagreRute(It.IsAny<Rute>())).ReturnsAsync(true);

            var ruteController = new RuteController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ruteController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await ruteController.LagreRute(It.IsAny<Rute>()) as OkObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Rute lagret", resultat.Value);
        }

        [Fact]
        public async Task LagreRuteLoggetInnOKDBFeil()
        {
            //Arrange
            mockRep.Setup(d => d.LagreRute(It.IsAny<Rute>())).ReturnsAsync(false);

            var ruteController = new RuteController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ruteController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await ruteController.LagreRute(It.IsAny<Rute>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Ruten kunne ikke lagres", resultat.Value);
        }

        [Fact]
        public async Task LagreRuteIkkeLoggetInn()
        {
            //Arrange
            mockRep.Setup(d => d.HentRuter()).ReturnsAsync(It.IsAny<List<Rute>>());

            var ruteController = new RuteController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ruteController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await ruteController.LagreRute(It.IsAny<Rute>()) as UnauthorizedObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task LagreRuteLoggetInnFeilInput()
        {
            //Arrange
            var rute1 = new Rute
            {
                Id = 1,
                FraDestinasjon = "",
                TilDestinasjon = "København",
                PrisBarn = 130,
                PrisVoksen = 260
            };

            mockRep.Setup(d => d.LagreRute(rute1)).ReturnsAsync(true);

            var ruteController = new RuteController(mockRep.Object, mockLog.Object);

            ruteController.ModelState.AddModelError("FraDestinasjon", "Feil i inputvalidering ved lagring av rute");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ruteController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await ruteController.LagreRute(rute1) as BadRequestObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering ved lagring av rute", resultat.Value);
        }


        //SlettRute

        [Fact]
        public async Task SlettRuteLoggetInnOK()
        {
            //Arrange
            mockRep.Setup(r => r.SlettRute(It.IsAny<int>())).ReturnsAsync(true);

            var ruteController = new RuteController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ruteController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await ruteController.SlettRute(It.IsAny<int>()) as OkObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Rute slettet", resultat.Value);
        }

        [Fact]
        public async Task SlettRuteLoggetInnIkkeOK()
        {
            //Arrange
            mockRep.Setup(r => r.SlettRute(It.IsAny<int>())).ReturnsAsync(false);

            var ruteController = new RuteController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ruteController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await ruteController.SlettRute(It.IsAny<int>()) as NotFoundObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Sletting av rute ble ikke utført", resultat.Value);
        }

        [Fact]
        public async Task SlettRutekkeLoggetInn()
        {
            //Arrange
            mockRep.Setup(r => r.SlettRute(It.IsAny<int>())).ReturnsAsync(true);

            var ruteController = new RuteController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ruteController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await ruteController.SlettRute(It.IsAny<int>()) as UnauthorizedObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        //EndreRute

        [Fact]
        public async Task EndreRuteLoggetInnOK()
        {
            //Arrange
            mockRep.Setup(r => r.EndreRute(It.IsAny<Rute>())).ReturnsAsync(true);

            var ruteController = new RuteController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ruteController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await ruteController.EndreRute(It.IsAny<Rute>()) as OkObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Rute endret", resultat.Value);
        }

        [Fact]
        public async Task EndreRuteLoggetInnOKDBFeil()
        {
            //Arrange
            mockRep.Setup(r => r.EndreRute(It.IsAny<Rute>())).ReturnsAsync(false);

            var ruteController = new RuteController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ruteController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await ruteController.EndreRute(It.IsAny<Rute>()) as NotFoundObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Ruten kunne ikke endres", resultat.Value);
        }

        [Fact]
        public async Task EndreRuteIkkeLoggetInn()
        {
            //Arrange
            mockRep.Setup(r => r.EndreRute(It.IsAny<Rute>())).ReturnsAsync(true);

            var ruteController = new RuteController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ruteController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await ruteController.EndreRute(It.IsAny<Rute>()) as UnauthorizedObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task EndreRuteLoggetInnFeilInput()
        {
            //Arrange

            var rute1 = new Rute
            {
                Id = 1,
                FraDestinasjon = "",
                TilDestinasjon = "København",
                PrisBarn = 130,
                PrisVoksen = 260
            };

            mockRep.Setup(r => r.EndreRute(rute1)).ReturnsAsync(true);

            var ruteController = new RuteController(mockRep.Object, mockLog.Object);

            ruteController.ModelState.AddModelError("FraDestinasjon", "Feil i inputvalidering ved endring av Destinasjon");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            ruteController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await ruteController.EndreRute(rute1) as BadRequestObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering ved endring av Rute", resultat.Value);
        }

    }

}
