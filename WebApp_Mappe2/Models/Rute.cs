using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Mappe2.Models
{
    public class Rute
    {
        public int Id { get; set; }
        public string FraDestinasjon { get; set; }
        public string TilDestinasjon { get; set; }
        public int PrisBarn { get; set; }
        public int PrisVoksen { get; set; }
    }
}
