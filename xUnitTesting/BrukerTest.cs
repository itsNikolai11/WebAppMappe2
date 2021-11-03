using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApp_Mappe2.Controllers;
using WebApp_Mappe2.DAL;
using WebApp_Mappe2.Models;
using Xunit;

namespace xUnitTesting
{
    public class BrukerTest
    {
        private const string _loggetInn = "loggetInn";
        private const string _IkkeLoggetInn = "";
        private readonly Mock<ILogger<BrukerController>> mockLogger = new Mock<ILogger<BrukerController>>();
        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession();
        [Fact]
        public async Task LoggInnOK()
        {
            /*----------Arrange---------*/

            var repositorymock = new Mock<IBrukerRepository>();

            repositorymock.Setup(rep => rep.LoggInn(It.IsAny<Bruker>())).ReturnsAsync(true);
            var brukerController = new BrukerController(repositorymock.Object, mockLogger.Object);

            mockSession[_loggetInn] = _IkkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;

            /*--------Act--------*/

            var resultat = await brukerController.LoggInn(It.IsAny<Bruker>()) as OkObjectResult;

            /*--------Assert--------*/
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.True((bool)resultat.Value);
            //Assert.Equal(_loggetInn, mockSession[_loggetInn]);


        }
        [Fact]
        public async Task LoggInnFeilBrukernavnPassord()
        {
            /*----------Arrange---------*/
            var repositorymock = new Mock<IBrukerRepository>();

            repositorymock.Setup(rep => rep.LoggInn(It.IsAny<Bruker>())).ReturnsAsync(false);
            var brukerController = new BrukerController(repositorymock.Object, mockLogger.Object);

            mockSession[_loggetInn] = _IkkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;
            /*--------Act--------*/
            var resultat = await brukerController.LoggInn(It.IsAny<Bruker>()) as UnauthorizedObjectResult;

            /*--------Assert--------*/
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Feil brukernavn eller passord", resultat.Value);

        }

        [Fact]
        public async Task LoggUt()
        {
            /*----------Arrange---------*/

            var repositorymock = new Mock<IBrukerRepository>();
            var brukerController = new BrukerController(repositorymock.Object, mockLogger.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;
            /*--------Act--------*/
            await brukerController.LoggUt();
            /*--------Assert--------*/
            Assert.Equal(_IkkeLoggetInn, mockSession[_loggetInn]);


        }
    }
}
