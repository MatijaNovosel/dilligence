using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tvz2api.Models.DTO
{
    public class StudentDTO : BaseDTO
    {
        public string Jmbag { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string ImagePath { get; set; }
        public string Email { get; set; }
        public string Smjer { get; set; }
    }
}
