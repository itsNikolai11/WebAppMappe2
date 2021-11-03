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
	public class AvgangerTest
	{
		private const string _loggetInn = "loggetInn";
		private const string _IkkeLoggetInn = "";
		private Mock<IAvgangRepository> repositorymock = new Mock<IAvgangRepository>();
		private readonly Mock<ILogger<AvgangController>> mockLogger = new Mock<ILogger<AvgangController>>();
		private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
		private readonly MockHttpSession mockSession = new MockHttpSession();
		public void SetSessionActive()
		{
			mockSession[_loggetInn] = _loggetInn;
			mockHttpContext.Setup(s => s.Session).Returns(mockSession);
		}
		public void SetSessionInactive()
		{
			mockSession[_loggetInn] = _IkkeLoggetInn;
			mockHttpContext.Setup(s => s.Session).Returns(mockSession);
		}
		public List<Avgang> LagListe()
		{
			Avgang avgang1 = new Avgang
			{
				AvgangTid = DateTime.Now,
				Id = 1,
				RuteNr = 1
			};
			Avgang avgang2 = new Avgang
			{
				AvgangTid = DateTime.Now,
				Id = 2,
				RuteNr = 2
			};
			List<Avgang> avganger = new List<Avgang>();
			avganger.Add(avgang1);
			avganger.Add(avgang2);
			return avganger;
		}
		[Fact]
		public async Task HentAvgangerOk()
		{
			/*----------Arrange---------*/
			List<Avgang> avganger = this.LagListe();
			repositorymock.Setup(avgang => avgang.HentAvganger()).ReturnsAsync(avganger);
			var avgangController = new AvgangController(repositorymock.Object, mockLogger.Object);
			SetSessionActive();
			avgangController.ControllerContext.HttpContext = mockHttpContext.Object;
			/*--------Act--------*/
			var resultat = await avgangController.HentAvganger() as OkObjectResult;

			/*--------Assert--------*/
			Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
			Assert.Equal<List<Avgang>>(avganger, (List<Avgang>) resultat.Value);

		}
		[Fact]
		public async Task HentAvgangerDatabaseFeil()
		{
			/*----------Arrange---------*/
			List<Avgang> avganger = this.LagListe();
			repositorymock.Setup(avgang => avgang.HentAvganger()).ReturnsAsync(()=>null);
			var avgangController = new AvgangController(repositorymock.Object, mockLogger.Object);
			SetSessionActive();
			avgangController.ControllerContext.HttpContext = mockHttpContext.Object;
			/*--------Act--------*/
			var resultat = await avgangController.HentAvganger() as NotFoundObjectResult;

			/*--------Assert--------*/
			Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
			Assert.Equal("Fant ingen avganger", resultat.Value);
		}
		[Fact]
		public async Task HentAvgangerIkkeInnlogget()
		{
			/*----------Arrange---------*/
			List<Avgang> avganger = this.LagListe();
			repositorymock.Setup(avgang => avgang.HentAvganger()).ReturnsAsync(avganger);
			var avgangController = new AvgangController(repositorymock.Object, mockLogger.Object);
			SetSessionInactive();
			avgangController.ControllerContext.HttpContext = mockHttpContext.Object;
			/*--------Act--------*/
			var resultat = await avgangController.HentAvganger() as UnauthorizedObjectResult;

			/*--------Assert--------*/
			Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
			Assert.Equal("Login ikke gyldig!", resultat.Value);
		}
		[Fact]
		public async Task HentEnAvgangOk()
		{
			/*----------Arrange---------*/
			Avgang avgang1 = new Avgang
			{
				AvgangTid = DateTime.Now,
				Id = 1,
				RuteNr = 1
			};
			repositorymock.Setup(avgang => avgang.HentAvgang(It.IsAny<int>())).ReturnsAsync(avgang1);
			var avgangController = new AvgangController(repositorymock.Object, mockLogger.Object);
			SetSessionActive();
			avgangController.ControllerContext.HttpContext = mockHttpContext.Object;
			/*--------Act--------*/
			var resultat = await avgangController.HentAvgang(It.IsAny<int>()) as OkObjectResult;

			/*--------Assert--------*/
			Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
			Assert.Equal<Avgang>(avgang1, (Avgang)resultat.Value);
		}
		[Fact]
		public async Task HentEnAvgangDatabaseFeil()
		{
			/*----------Arrange---------*/

			repositorymock.Setup(avgang => avgang.HentAvgang(It.IsAny<int>())).ReturnsAsync(()=>null);
			var avgangController = new AvgangController(repositorymock.Object, mockLogger.Object);
			SetSessionActive();
			avgangController.ControllerContext.HttpContext = mockHttpContext.Object;
			/*--------Act--------*/
			var resultat = await avgangController.HentAvgang(It.IsAny<int>()) as NotFoundObjectResult;

			/*--------Assert--------*/
			Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
			Assert.Equal("Fant ikke avgang", resultat.Value);
		}
		[Fact]
		public async Task HentEnAvgangIkkeLoggetInn()
		{
			/*----------Arrange---------*/
			repositorymock.Setup(avgang => avgang.HentAvgang(It.IsAny<int>())).ReturnsAsync(It.IsAny<Avgang>());
			var avgangController = new AvgangController(repositorymock.Object, mockLogger.Object);
			SetSessionInactive();
			avgangController.ControllerContext.HttpContext = mockHttpContext.Object;
			/*--------Act--------*/
			var resultat = await avgangController.HentAvgang(It.IsAny<int>()) as UnauthorizedObjectResult;

			/*--------Assert--------*/
			Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
			Assert.Equal("Login ikke gyldig!", resultat.Value);
		}
		[Fact]
		public async Task LagreAvgangOk()
		{
			/*----------Arrange---------*/
			repositorymock.Setup(avgang => avgang.LagreAvgang(It.IsAny<Avgang>())).ReturnsAsync(true);
			var avgangController = new AvgangController(repositorymock.Object, mockLogger.Object);
			SetSessionActive();
			avgangController.ControllerContext.HttpContext = mockHttpContext.Object;
			/*--------Act--------*/

			var resultat = await avgangController.LagreAvgang(It.IsAny<Avgang>()) as OkObjectResult;

			/*--------Assert--------*/
			Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
			Assert.True((bool)resultat.Value);
		}
		[Fact]
		public async Task LagreAvgangDatabaseFeil()
		{
			/*----------Arrange---------*/
			repositorymock.Setup(avgang => avgang.LagreAvgang(It.IsAny<Avgang>())).ReturnsAsync(false);
			var avgangController = new AvgangController(repositorymock.Object, mockLogger.Object);
			SetSessionActive();
			avgangController.ControllerContext.HttpContext = mockHttpContext.Object;
			/*--------Act--------*/

			var resultat = await avgangController.LagreAvgang(It.IsAny<Avgang>()) as BadRequestObjectResult;

			/*--------Assert--------*/
			Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
			Assert.Equal("Lagring feilet", resultat.Value);
		}
		[Fact]
		public async Task LagreAvgangIkkeInnlogget()
		{
			/*----------Arrange---------*/
			repositorymock.Setup(avgang => avgang.LagreAvgang(It.IsAny<Avgang>())).ReturnsAsync(true);
			var avgangController = new AvgangController(repositorymock.Object, mockLogger.Object);
			SetSessionInactive();
			avgangController.ControllerContext.HttpContext = mockHttpContext.Object;
			/*--------Act--------*/

			var resultat = await avgangController.LagreAvgang(It.IsAny<Avgang>()) as UnauthorizedObjectResult;

			/*--------Assert--------*/
			Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
			Assert.Equal("Login ikke gyldig!", resultat.Value);
		}
		[Fact]
		public async Task SlettAvgangOk()
		{
			/*----------Arrange---------*/
			repositorymock.Setup(avgang => avgang.SlettAvgang(It.IsAny<int>())).ReturnsAsync(true);
			var avgangController = new AvgangController(repositorymock.Object, mockLogger.Object);
			SetSessionActive();
			avgangController.ControllerContext.HttpContext = mockHttpContext.Object;
			/*--------Act--------*/

			var resultat = await avgangController.SlettAvgang(It.IsAny<int>()) as OkObjectResult;

			/*--------Assert--------*/
			Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
			Assert.True((bool)resultat.Value);
		}
		[Fact]
		public async Task SlettAvgangDatabaseFeil()
		{
			/*----------Arrange---------*/
			repositorymock.Setup(avgang => avgang.SlettAvgang(It.IsAny<int>())).ReturnsAsync(false);
			var avgangController = new AvgangController(repositorymock.Object, mockLogger.Object);
			SetSessionActive();
			avgangController.ControllerContext.HttpContext = mockHttpContext.Object;
			/*--------Act--------*/

			var resultat = await avgangController.SlettAvgang(It.IsAny<int>()) as NotFoundObjectResult;

			/*--------Assert--------*/
			Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
			Assert.False((bool)resultat.Value);
		}
		[Fact]
		public async Task SlettAvgangIkkeLoggetInn()
		{
			/*----------Arrange---------*/
			repositorymock.Setup(avgang => avgang.SlettAvgang(It.IsAny<int>())).ReturnsAsync(true);
			var avgangController = new AvgangController(repositorymock.Object, mockLogger.Object);
			SetSessionInactive();
			avgangController.ControllerContext.HttpContext = mockHttpContext.Object;
			/*--------Act--------*/

			var resultat = await avgangController.SlettAvgang(It.IsAny<int>()) as UnauthorizedObjectResult;

			/*--------Assert--------*/
			Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
			Assert.Equal("Login ikke gyldig!", resultat.Value);
		}
		[Fact]
		public async Task EndreAvgangOk()
		{
			/*----------Arrange---------*/
			repositorymock.Setup(avgang => avgang.EndreAvgang(It.IsAny<Avgang>())).ReturnsAsync(true);
			var avgangController = new AvgangController(repositorymock.Object, mockLogger.Object);
			SetSessionActive();
			avgangController.ControllerContext.HttpContext = mockHttpContext.Object;
			/*--------Act--------*/

			var resultat = await avgangController.EndreAvgang(It.IsAny<Avgang>()) as OkObjectResult;

			/*--------Assert--------*/
			Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
			Assert.True((bool)resultat.Value);
		}

		[Fact]
		public async Task EndreAvgangDatabaseFeil()
		{
			/*----------Arrange---------*/
			repositorymock.Setup(avgang => avgang.EndreAvgang(It.IsAny<Avgang>())).ReturnsAsync(false);
			var avgangController = new AvgangController(repositorymock.Object, mockLogger.Object);
			SetSessionActive();
			avgangController.ControllerContext.HttpContext = mockHttpContext.Object;
			/*--------Act--------*/

			var resultat = await avgangController.EndreAvgang(It.IsAny<Avgang>()) as NotFoundObjectResult;

			/*--------Assert--------*/
			Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
			Assert.False((bool)resultat.Value);
		}
		[Fact]
		public async Task EndreAvgangValideringFeil()
		{
			/*----------Arrange---------*/
			repositorymock.Setup(avgang => avgang.EndreAvgang(It.IsAny<Avgang>())).ReturnsAsync(false);
			var avgangController = new AvgangController(repositorymock.Object, mockLogger.Object);
			SetSessionActive();

			avgangController.ControllerContext.HttpContext = mockHttpContext.Object;
			avgangController.ModelState.AddModelError("RuteNr", "Feil i inputvalidering ved endring av Avgang");
			/*--------Act--------*/

			var resultat = await avgangController.EndreAvgang(It.IsAny<Avgang>()) as BadRequestObjectResult;

			/*--------Assert--------*/
			Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
			Assert.Equal("Feil i inputvalidering ved endring av Avgang", resultat.Value);
		}
		[Fact]
		public async Task EndreAvgangIkkeLoggetInn()
		{
			/*----------Arrange---------*/
			repositorymock.Setup(avgang => avgang.EndreAvgang(It.IsAny<Avgang>())).ReturnsAsync(true);
			var avgangController = new AvgangController(repositorymock.Object, mockLogger.Object);
			SetSessionInactive();
			avgangController.ControllerContext.HttpContext = mockHttpContext.Object;
			/*--------Act--------*/

			var resultat = await avgangController.EndreAvgang(It.IsAny<Avgang>()) as UnauthorizedObjectResult;

			/*--------Assert--------*/
			Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
			Assert.Equal("Login ikke gyldig!", resultat.Value);
		}

	}
}
