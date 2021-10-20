using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Mappe2.Models;

namespace WebApp_Mappe2.DAL
{
    public class RuteRepository : IRuteRepository
    {
        private readonly DBContext _db;
        public RuteRepository(DBContext db)
        {
            _db = db;
        }
        public async Task<List<Destinasjon>> HentAlleDestinasjoner()
        {
            throw new NotImplementedException();
        }
        public async Task<Destinasjon> HentDestinasjon()
        {
            throw new NotImplementedException();
        }
        public async Task<List<Rute>> HentRuter(int id)
        {
            //ID er id til fra-destinasjon
            throw new NotImplementedException();
        }
        public async Task<Rute> HentRute(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<List<Avganger>> HentAvganger(int RuteId, DateTime Tid)
        {
            throw new NotImplementedException();
        }
        public async  Task<Avganger> HentAvgang(int id)
        {
            throw new NotImplementedException();
        }
    }
}
