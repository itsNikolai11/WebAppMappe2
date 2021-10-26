using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Mappe2.Models;

namespace WebApp_Mappe2.DAL
{
    public interface IRuteRepository
    {
        Task<List<Rute>> HentRuter(); //ID er id til fra-destinasjon
        Task<Rute> HentRute(int id);
        Task<bool> LagreRute(Rute r);
        Task<bool> SlettRute(int id);
        Task<bool> EndreRute(Rute r);
    }
}
