using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Mappe2.Models;

namespace WebApp_Mappe2.DAL
{
    public class DestinasjonRepository : IDestinasjonRepository
    {
        private readonly DBContext _db;
        public DestinasjonRepository(DBContext db)
        {
            _db = db;
        }
        public async Task<List<Destinasjon>> HentAlleDestinasjoner()
        {
            try
            {
                List<Destinasjon> destinasjoner = await _db.Destinasjoner.Select(d => new Destinasjon
                {
                    Id = d.Id,
                    Sted = d.Sted,
                    Land = d.Land
                }).ToListAsync();

                return destinasjoner;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Destinasjon> HentDestinasjon(int id)
        {
            try
            {
                Destinasjoner enDestinasjon = await _db.Destinasjoner.FindAsync(id);
                Destinasjon destinasjon = new Destinasjon()
                {
                    Id = enDestinasjon.Id,
                    Sted = enDestinasjon.Sted,
                    Land = enDestinasjon.Land
                };
                return destinasjon;
            }
            catch
            {
                return null;
            }

        }
        public async Task<bool> LagreDestinasjon(Destinasjon d)
        {
            try
            {
                var nyDestinasjon = new Destinasjoner();

                nyDestinasjon.Sted = d.Sted;
                nyDestinasjon.Land = d.Land;

                _db.Destinasjoner.Add(nyDestinasjon);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public async Task<bool> SlettDestinasjon(int id)
        {
            try
            {
                var enDestinasjon = _db.Destinasjoner.Find(id);
                _db.Destinasjoner.Remove(enDestinasjon);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> EndreDestinasjon(Destinasjon d)
        {
            try
            {
                var endreObjekt = await _db.Destinasjoner.FindAsync(d.Id);

                endreObjekt.Sted = d.Sted;
                endreObjekt.Land = d.Land;
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
