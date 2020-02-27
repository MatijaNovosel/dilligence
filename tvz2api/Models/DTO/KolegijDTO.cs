using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tvz2api.Enumerations;

namespace tvz2api.Models.DTO
{
    public class KolegijDTO : BaseDTO
    {
        public string Naziv { get; set; }
        public string Isvu { get; set; }
        public int? Ects { get; set; }
        public string Url { get; set; }
        public SmjerEnum Smjer { get; set; }
    }
}
