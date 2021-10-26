﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Mappe2.Models;

namespace WebApp_Mappe2.DAL
{
    public interface IDestinasjonRepository
    {
        Task<List<Destinasjon>> HentAlleDestinasjoner();
        Task<Destinasjon> HentDestinasjon(int id);
        Task<bool> LagreDestinasjon(Destinasjon d);
        Task<bool> SlettDestinasjon(int id);
        Task<bool> EndreDestinasjon(Destinasjon d);
    }
}
