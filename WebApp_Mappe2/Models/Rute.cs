using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Mappe2.Models
{
    [ExcludeFromCodeCoverage]
    public class Rute
    {
        public int Id { get; set; }
        public string FraDestinasjon { get; set; }
        public string TilDestinasjon { get; set; }
        [RegularExpression(@"[0-9]{1,5}")]
        public int PrisBarn { get; set; }
        [RegularExpression(@"[0-9]{1,5}")]
        public int PrisVoksen { get; set; }
    }
}
