using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Mappe2.Models;

namespace WebApp_Mappe2.Controllers
{
    [Route("api/[controller]")]
    public class AvgangController : ControllerBase
    {
        private const string _loggetInn = "loggetInn";

        [HttpGet]
        public async Task<ActionResult> HentAvganger(int RuteId, DateTime Tid)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {

                return Unauthorized();
            }
            throw new NotImplementedException();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> HentAvgang(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {

                return Unauthorized();
            }
            throw new NotImplementedException();
        }
        [HttpPost]
        public async Task<ActionResult> LagreAvgang(Avgang r)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {

                return Unauthorized();
            }
            throw new NotImplementedException();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> SlettAvgang(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {

                return Unauthorized();
            }
            throw new NotImplementedException();
        }
        [HttpPut]
        public async Task<ActionResult> EndreAvgang(Avgang r)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {

                return Unauthorized();
            }
            throw new NotImplementedException();
        }
    }
}
