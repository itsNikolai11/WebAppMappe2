using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Mappe2.DAL;
using WebApp_Mappe2.Models;

namespace WebApp_Mappe2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class BrukerController : ControllerBase
    {
        private readonly IBrukerRepository _db;

        private const string _loggetInn = "loggetInn";

        private ILogger<BrukerController> _log;
        public BrukerController(IBrukerRepository db, ILogger<BrukerController> log)
        {
            _db = db;
            _log = log;
        }
        [HttpPost]
        public async Task<ActionResult> LoggInn(Bruker bruker)
        {
            
            //TODO legg inn validering
            bool loginOk = await _db.LoggInn(bruker);
            if (!loginOk)
            {
                HttpContext.Session.SetString(_loggetInn, "");
                _log.LogInformation("Innlogging av bruker ikke godkjent!");
                return Unauthorized();
            }
            HttpContext.Session.SetString(_loggetInn, "OK");
            _log.LogInformation("Innlogging av bruker godkjent");
            return Ok();

        }
        [HttpGet]
        public async Task<ActionResult> LoggUt()
        {
            HttpContext.Session.SetString(_loggetInn, "");
            return Ok();
        }

    }
}
