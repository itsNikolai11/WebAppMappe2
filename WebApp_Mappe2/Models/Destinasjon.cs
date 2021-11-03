using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Mappe2.Models
{
    [ExcludeFromCodeCoverage]
    public class Destinasjon
    {
        public int Id { get; set; }
        public string Sted { get; set; }
        public string Land { get; set; }
    }
}
