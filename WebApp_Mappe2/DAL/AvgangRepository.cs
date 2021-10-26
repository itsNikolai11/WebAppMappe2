using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Mappe2.Models;

namespace WebApp_Mappe2.DAL
{
    public class AvgangRepository : IAvgangRepository
    {
        private readonly DBContext _db;
        public AvgangRepository(DBContext db)
        {
            _db = db;
        }
        public async Task<List<Avganger>> HentAvganger(int RuteId, DateTime Tid)
        {
            throw new NotImplementedException();
        }
        public async Task<Avganger> HentAvgang(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> LagreAvgang(Avgang r)
        {
            //throw new NotImplementedException();
            return true;
        }
        public async Task<bool> SlettAvgang(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> EndreAvgang(Avgang r)
        {
            throw new NotImplementedException();
        }
    }
}
