﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Mappe2.Models;

namespace WebApp_Mappe2.DAL
{
    [ExcludeFromCodeCoverage]
    public class AvgangRepository : IAvgangRepository
    {
        private readonly DBContext _db;
        public AvgangRepository(DBContext db)
        {
            _db = db;
        }
        public async Task<List<Avgang>> HentAvganger()
        {
            try
            {
                List<Avgang> alleAvganger = await _db.Avganger.Select(a => new Avgang
                {
                    Id = a.Id,
                    AvgangTid = a.AvgangTid,
                    RuteNr = a.RuteNr.Id, 
                }).ToListAsync();
                return alleAvganger;
            }
            catch
            {
                return null;
            }
        }
        public async Task<Avgang> HentAvgang(int id)
        {
            Avganger enAvgang = await _db.Avganger.FindAsync(id);
            var hentetAvgang = new Avgang()
            {
                Id = enAvgang.Id,
                AvgangTid = enAvgang.AvgangTid,
                RuteNr = enAvgang.RuteNr.Id
            };
            return hentetAvgang;
        }
        public async Task<bool> LagreAvgang(Avgang r)
        {
            try
            {
                var sjekkId = await _db.Ruter.FindAsync(r.RuteNr);
                
                var nyAvgangRad = new Avganger();
                
                nyAvgangRad.AvgangTid = r.AvgangTid;
                nyAvgangRad.RuteNr = sjekkId;

                _db.Avganger.Add(nyAvgangRad);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> SlettAvgang(int id)
        {
            try
            {
                Avganger enAvgang = await _db.Avganger.FindAsync(id);
                _db.Avganger.Remove(enAvgang);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> EndreAvgang(Avgang r)
        {
            try
            {
                var endreAvgang = await _db.Avganger.FindAsync(r.Id);
                var sjekkId = await _db.Ruter.FindAsync(r.RuteNr);

                endreAvgang.AvgangTid = r.AvgangTid;
                endreAvgang.RuteNr = sjekkId;
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
