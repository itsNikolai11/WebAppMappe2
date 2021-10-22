using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Mappe2.DAL;
using WebApp_Mappe2.Models;

namespace WebApp_Mappe2.Controllers
{
    //[Route("api/[controller]")]
    public class DestinasjonController : ControllerBase
    {
        private readonly IDestinasjonRepository _db;
        public DestinasjonController(IDestinasjonRepository db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult> HentAlleDestinasjoner()
        {
            List<Destinasjon> alleDestinasjoner = await _db.HentAlleDestinasjoner();
            return Ok(alleDestinasjoner);
        }




        [HttpGet("{id}")]
        public async Task<ActionResult> HentDestinasjon()
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public async Task<ActionResult> LagreDestinasjon(Destinasjon d)
        {
            throw new NotImplementedException();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> SlettDestinasjon(int id)
        {
            throw new NotImplementedException();
        }
        [HttpPut]
        public async Task<ActionResult> EndreDestinasjon(Destinasjon d)
        {
            throw new NotImplementedException();
        }
    }
}
