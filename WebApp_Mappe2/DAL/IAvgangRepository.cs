using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Mappe2.Models;

namespace WebApp_Mappe2.DAL
{
    interface IAvgangRepository
    {
        Task<List<Avganger>> HentAvganger(int RuteId, DateTime Tid);
        Task<Avganger> HentAvgang(int id);
        Task<bool> LagreAvgang(Avgang r);
        Task<bool> SlettAvgang(int id);
        Task<bool> EndreAvgang(Avgang a);
    }
}
