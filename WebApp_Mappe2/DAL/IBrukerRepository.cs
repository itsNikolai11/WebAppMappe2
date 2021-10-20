using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Mappe2.Models;

namespace WebApp_Mappe2.DAL
{
    public interface IBrukerRepository
    {
        Task<bool> LoggInn(Bruker bruker);

    }
}
