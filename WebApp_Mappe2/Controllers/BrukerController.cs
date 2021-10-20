using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Mappe2.DAL;
using WebApp_Mappe2.Models;

namespace WebApp_Mappe2.Controllers
{
    public class BrukerController : ControllerBase
    {
        private readonly IBrukerRepository _db;
        public BrukerController(IBrukerRepository db)
        {
            _db = db;
        }
        public async Task<ActionResult> LoggInn(Bruker bruker)
        {
            //TODO legg inn logg
            //TODO legg inn validering
            bool loginOk = await _db.LoggInn(bruker);
            if (!loginOk)
            {
                return Ok(false);
            }
            return Ok(true);
            
        }

    }
}
