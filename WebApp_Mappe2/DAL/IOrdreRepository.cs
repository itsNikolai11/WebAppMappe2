using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Mappe2.Models;

namespace WebApp_Mappe2.DAL
{
    public interface IOrdreRepository
    {
        Task<List<Billett>> hentAlle();
        Task<Billett> hentBillett(int id);
        Task<bool> slettBillett(int id);
        Task<bool> endreBillett(Billett b);
        Task<bool> lagreBillett(Billett b);
    }
}
