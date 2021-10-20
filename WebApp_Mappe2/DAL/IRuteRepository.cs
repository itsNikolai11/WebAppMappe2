﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Mappe2.Models;

namespace WebApp_Mappe2.DAL
{
    interface IRuteRepository
    {
        Task<List<Destinasjon>> HentAlleDestinasjoner();
        Task<Destinasjon> HentDestinasjon();
        Task<List<Rute>> HentRuter(int id); //ID er id til fra-destinasjon
        Task<Rute> HentRute(int id);
        Task<List<Avganger>> HentAvganger(int RuteId, DateTime Tid);
        Task<Avganger> HentAvgang(int id);
    }
}
