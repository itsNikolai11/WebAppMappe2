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
    [Route("api/[controller]")]
    public class RuteController : ControllerBase
    {
        private const string _loggetInn = "loggetInn";

        private IRuteRepository _db;

        private ILogger<RuteController> _log;

        public RuteController(IRuteRepository db, ILogger<RuteController> log)
        {
            _db = db;
            _log = log;
        }

        [HttpGet]
        public async Task<ActionResult> HentRuter()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {

                return Unauthorized();
            }
            //ID er id til fra-destinasjon
            List<Rute> alleRuter = await _db.HentRuter();
            return Ok(alleRuter);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> HentRute(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {

                return Unauthorized();
            }
            //mangler log
            Rute ruten = await _db.HentRute(id);
            return Ok(ruten);
        }

        [HttpPost]
        public async Task<ActionResult> LagreRute(Rute r)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {

                return Unauthorized();
            }
            throw new NotImplementedException();
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> SlettRute(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {

                return Unauthorized();
            }
            throw new NotImplementedException();
            //skrive denne for å  kunne slette
        }
     
    }
}
