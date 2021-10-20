using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Mappe2.Models;

namespace WebApp_Mappe2.Controllers
{
    [Route("api/[controller]")]
    public class AvgangController
    {
        [HttpGet]
        public async Task<ActionResult> HentAvganger(int RuteId, DateTime Tid)
        {
            throw new NotImplementedException();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> HentAvgang(int id)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public async Task<ActionResult> LagreAvgang(Avgang r)
        {
            throw new NotImplementedException();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> SlettAvgang(int id)
        {
            throw new NotImplementedException();
        }
        [HttpPut]
        public async Task<ActionResult> EndreAvgang(Avgang r)
        {
            throw new NotImplementedException();
        }
    }
}
