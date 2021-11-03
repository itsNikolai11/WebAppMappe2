using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Mappe2.Models;

namespace WebApp_Mappe2.DAL
{
    public interface IAvgangRepository
    {
        Task<List<Avgang>> HentAvganger(/*int RuteId, DateTime Tid*/);
        Task<Avgang> HentAvgang(int id);
        Task<bool> LagreAvgang(Avgang r);
        Task<bool> SlettAvgang(int id);
        Task<bool> EndreAvgang(Avgang a);
    }
}
