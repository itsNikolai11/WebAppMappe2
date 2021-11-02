using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Mappe2.Models;

namespace WebApp_Mappe2.DAL
{
    public class OrdreRepository : IOrdreRepository
    {
        private readonly DBContext _db;
        public OrdreRepository(DBContext db)
        {
            _db = db;
        }
        public async Task<bool> endreBillett(Billett b)
        {
            try
            {
                var dbBillett = await _db.Ordrer.FindAsync(b.Id);
                var dbRute = await _db.Ruter.FindAsync(b.RuteNr);
                var dbAvgang = await _db.Avganger.FindAsync(b.AvgangNr);
                dbBillett.AvgangNr = dbAvgang;
                dbBillett.RuteNr = dbRute;
                dbBillett.RefPers = b.RefPers;
                dbBillett.AntallBarn = b.AntallBarn;
                dbBillett.AntallVoksen = b.AntallVoksen;
                await _db.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<List<Billett>> hentAlle()
        {
            try
            {
                List<Billett> alleBilletter = await _db.Ordrer.Select(b => new Billett
                {
                    AvgangNr = b.AvgangNr.Id,
                    RuteNr = b.RuteNr.Id,
                    AntallBarn = b.AntallBarn,
                    AntallVoksen = b.AntallVoksen,
                    RefPers = b.RefPers,
                    Id = b.Id
                }).ToListAsync();
                return alleBilletter;

            }
            catch
            {
                return null;
            }
        }

        public async Task<Billett> hentBillett(int id)
        {
            try
            {
                var dbBillett = await _db.Ordrer.FindAsync(id);
                Billett billett = new Billett
                {
                    AntallBarn = dbBillett.AntallBarn,
                    AntallVoksen = dbBillett.AntallVoksen,
                    Id = dbBillett.Id,
                    AvgangNr = dbBillett.AvgangNr.Id,
                    RuteNr = dbBillett.RuteNr.Id,
                    RefPers = dbBillett.RefPers
                };
                return billett;
            }
            catch
            {
                return null;
            }

        }

        public async Task<bool> lagreBillett(Billett b)
        {
            try
            {
                var nyBillett = new Ordrer();
                var sjekkRuteNr = await _db.Ruter.FindAsync(b.RuteNr);
                var sjekkAvgang = await _db.Avganger.FindAsync(b.AvgangNr);

                nyBillett.AntallBarn = b.AntallBarn;
                nyBillett.AntallVoksen = b.AntallVoksen;
                nyBillett.RefPers = b.RefPers;
                nyBillett.AvgangNr = sjekkAvgang;
                nyBillett.RuteNr = sjekkRuteNr;

                _db.Ordrer.Add(nyBillett);
                await _db.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
           
        }

        public async Task<bool> slettBillett(int id)
        {
            try
            {
            var enBillett = _db.Ordrer.Find(id);
            _db.Ordrer.Remove(enBillett);
            await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
            
            
        }
    }
}
