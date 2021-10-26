using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
       
        public async Task<List<Rute>> HentRuter()
        {
            //ID er id til fra-destinasjon
            try
            {
                List<Rute> ruter = await _db.Ruter.Select(r => new Rute
                {
                    Id = r.Id,
                    FraDestinasjon = r.FraDestinasjon.Sted,
                    TilDestinasjon = r.TilDestinasjon.Sted,
                    PrisBarn = r.PrisBarn,
                    PrisVoksen = r.PrisVoksen

                }).ToListAsync();

                return ruter;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Rute> HentRute(int id)
        {
            try
            {
                Ruter enRute = await _db.Ruter.FindAsync(id);
                var hentetRute = new Rute()
                {
                    Id = enRute.Id,
                    FraDestinasjon = enRute.FraDestinasjon.Sted,
                    TilDestinasjon = enRute.TilDestinasjon.Sted,
                    PrisBarn = enRute.PrisBarn,
                    PrisVoksen = enRute.PrisVoksen
                };

                return hentetRute;
            }
            catch
            {
                return null;
            }
        }
    
 
        
        public async Task<bool> LagreRute(Rute r)
        {
            try
            {
                var nyRute = new Ruter();

                var sjekkFraDest = _db.Destinasjoner.Find(r.FraDestinasjon);
                var sjekkTilDest = _db.Destinasjoner.Find(r.TilDestinasjon);
                nyRute.FraDestinasjon = sjekkFraDest;
                nyRute.TilDestinasjon = sjekkTilDest;
                nyRute.PrisBarn = r.PrisBarn;
                nyRute.PrisVoksen = r.PrisVoksen;

                _db.Ruter.Add(nyRute);
                await _db.SaveChangesAsync();
                return true;

               
            }
            catch
            {
                return false;
            }
        }
     
       
        public async Task<bool> SlettRute(int id)
        {
            try
            {
                Ruter enRute = await _db.Ruter.FindAsync(id);
                _db.Ruter.Remove(enRute);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<bool> EndreRute(Rute r)
        {
            throw new NotImplementedException();
        }

    }
}
