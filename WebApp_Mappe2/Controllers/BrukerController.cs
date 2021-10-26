using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public BrukerController(IBrukerRepository db)
        {
            _db = db;
        }
        [HttpPost]
        public async Task<ActionResult> LoggInn(Bruker bruker)
        {
            //TODO legg inn logg
            //TODO legg inn validering
            bool loginOk = await _db.LoggInn(bruker);
            if (!loginOk)
            {
                HttpContext.Session.SetString(_loggetInn, "");
                return Ok(false);
            }
            HttpContext.Session.SetString(_loggetInn, "OK");
            return Ok(true);

        }

    }
}
