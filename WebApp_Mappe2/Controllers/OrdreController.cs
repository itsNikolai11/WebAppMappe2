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
    public class OrdreController : ControllerBase
    {
        private readonly IOrdreRepository _db;
        private const string _loggetInn = "loggetInn";
        public OrdreController(IOrdreRepository db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<ActionResult> HentOrdre()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {

                return Unauthorized();
            }
            List<Billett> billetter = await _db.hentAlle();
            return Ok(billetter);
        }
        [HttpPost]
        public async Task<ActionResult> LagreOrdre(Billett b)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {

                return Unauthorized();
            }
            bool returOk = await _db.lagreBillett(b);
            if (!returOk)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
