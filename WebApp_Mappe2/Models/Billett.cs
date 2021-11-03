using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Mappe2.Models
{
    [ExcludeFromCodeCoverage]
    public class Billett
    {
        public int Id { get; set; }
        public int AntallBarn { get; set; }
        public int AntallVoksen { get; set; }
        public string RefPers { get; set; }
        public int AvgangNr { get; set; }
        public int RuteNr { get; set; }

    }
}
