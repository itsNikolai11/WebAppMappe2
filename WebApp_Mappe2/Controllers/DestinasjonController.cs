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
    public class DestinasjonController : ControllerBase  
    {
        private const string _loggetInn = "loggetInn";

        private IDestinasjonRepository _db;

        private ILogger<DestinasjonController> _log;
        public DestinasjonController(IDestinasjonRepository db, ILogger<DestinasjonController> log)
        {
            _db = db;
            _log = log;
        }

        [HttpGet]
        public async Task<ActionResult> HentAlleDestinasjoner()
        {
            List<Destinasjon> alleDestinasjoner = await _db.HentAlleDestinasjoner();
            if(alleDestinasjoner == null)
            {
                _log.LogInformation("Fant ingen destinasjoner");
                return NotFound("Fant ingen destinasjoner");
            }
            return Ok(alleDestinasjoner);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> HentDestinasjon(int id)
        {       
            Destinasjon destinasjon = await _db.HentDestinasjon(id);
            if(destinasjon == null)
            {
                _log.LogInformation("Fant ikke destinasjon med id " + id);
                return NotFound("Fant ikke avgang med id " + id);
            }
            return Ok(destinasjon);
        }


        [HttpPost]
        public async Task<ActionResult> LagreDestinasjon(Destinasjon d)
        {

            throw new NotImplementedException();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> SlettDestinasjon(int id)
        {
            //Destinasjon destinasjon = await _db.SlettDestinasjon

           // if()
           // return Ok();

            throw new NotImplementedException();
        }
        [HttpPut]
        public async Task<ActionResult> EndreDestinasjon(Destinasjon d)
        {
            throw new NotImplementedException();
        }
    }
}
