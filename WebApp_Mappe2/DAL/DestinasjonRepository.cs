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
            throw new NotImplementedException();
        }
        public async Task<bool> LagreDestinasjon(Destinasjon d)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> SlettDestinasjon(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> EndreDestinasjon(Destinasjon d)
        {
            throw new NotImplementedException();
        }
    }
}
