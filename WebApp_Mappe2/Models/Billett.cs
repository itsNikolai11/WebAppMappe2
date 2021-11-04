using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Mappe2.Models
{
    [ExcludeFromCodeCoverage]
    public class Billett
    {
        public int Id { get; set; }
        [RegularExpression(@"[0-9]{1,5}")]
        public int AntallBarn { get; set; }
        [RegularExpression(@"[0-9]{1,5}")]
        public int AntallVoksen { get; set; }
        [RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{2,30}")]
        public string RefPers { get; set; }
        public int AvgangNr { get; set; }
        public int RuteNr { get; set; }

    }
}
