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
                List<Rute> alleRuter = await _db.Ruter.Select(r => new Rute
                {
                    Id = r.Id,
                    //sjekke at fra-til ikke finnes fra før
                    FraDestinasjon = r.FraDestinasjon.Sted, //dropdown liste med destinasjoner
                    TilDestinasjon = r.TilDestinasjon.Sted,
                    PrisBarn = r.PrisBarn,
                    PrisVoksen = r.PrisVoksen

                }).ToListAsync();

                return alleRuter;
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
                Rute hentetRute = new Rute()
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
    
 
        
        public async Task<bool> LagreRute(Rute innRute)
        {
            try
            {
                //sjekke om rute finnes fra før
                //sjekke om man velger f.eks. oslo-oslo
                var nyRute = new Ruter();

                var sjekkFraDest =  await _db.Destinasjoner.FirstOrDefaultAsync(r => r.Sted == innRute.FraDestinasjon);
                var sjekkTilDest = await _db.Destinasjoner.FirstOrDefaultAsync(r => r.Sted == innRute.TilDestinasjon);


                nyRute.FraDestinasjon = sjekkFraDest;
                nyRute.TilDestinasjon = sjekkTilDest;

                
                nyRute.PrisBarn = innRute.PrisBarn;
                nyRute.PrisVoksen = innRute.PrisVoksen;

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
                var enRute =  _db.Ruter.Find(id);
               
                _db.Ruter.Remove(enRute);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<bool> EndreRute(Rute endreRute)
        {

            try
            {
                var endre = await _db.Ruter.FindAsync(endreRute.Id);
                var sjekkFraDest = await _db.Destinasjoner.FirstOrDefaultAsync(r => r.Sted == endreRute.FraDestinasjon);
                var sjekkTilDest = await _db.Destinasjoner.FirstOrDefaultAsync(r => r.Sted == endreRute.TilDestinasjon);
                endre.FraDestinasjon = sjekkFraDest;
                endre.TilDestinasjon = sjekkTilDest;
                endre.PrisBarn = endreRute.PrisBarn;
                endre.PrisVoksen = endreRute.PrisVoksen;

                
                await _db.SaveChangesAsync();
              

            }
            catch
            {
                return false;
            }
            return true;
        }

    }
}
