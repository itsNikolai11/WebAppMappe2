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
    public class RuteController : ControllerBase
    {
        private const string _loggetInn = "loggetInn";


        [HttpGet]
        public async Task<ActionResult> HentRuter(int id)
        {
            //ID er id til fra-destinasjon
            throw new NotImplementedException();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> HentRute(int id)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public async Task<ActionResult> LagreRute(Rute r)
        {
            throw new NotImplementedException();
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> SlettRute(int id)
        {
            throw new NotImplementedException();
        }
     
    }
}
