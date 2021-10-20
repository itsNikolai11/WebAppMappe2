using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Mappe2.DAL;
using WebApp_Mappe2.Models;

namespace WebApp_Mappe2.Controllers
{
    [Route("api/[controller]")]
    public class RuteController
    {
        public async Task<ActionResult> HentAlleDestinasjoner()
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult> HentDestinasjon()
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult> HentRuter(int id)
        {
            //ID er id til fra-destinasjon
            throw new NotImplementedException();
        }
        public async Task<ActionResult> HentRute(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult> HentAvganger(int RuteId, DateTime Tid)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult> HentAvgang(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult> LagreDestinasjon(Destinasjon d)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult> LagreRute(Rute r)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult> LagreAvgang(Avgang r)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult> SlettDestinasjon(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult> SlettRute(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult> SlettAvgang(int id)
        {
            throw new NotImplementedException();
        }
    }
}
