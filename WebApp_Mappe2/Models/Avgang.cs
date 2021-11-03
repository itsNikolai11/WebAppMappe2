using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Mappe2.Models
{
    [ExcludeFromCodeCoverage]
    public class Avgang
    {
        public int Id { get; set; }
        public DateTime AvgangTid { get; set; }
        //public string AvgangTid { get; set; }
        public int RuteNr { get; set; }
        //public string FraDestinasjon { get; set; }
        //public string TilDestinasjon { get; set; }
    }
}
