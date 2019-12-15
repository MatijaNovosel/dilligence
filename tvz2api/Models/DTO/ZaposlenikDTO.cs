using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tvz2api.Models.DTO
{
    public class ZaposlenikDTO : BaseDTO
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string TitulaIspred { get; set; }
        public string TitulaIza { get; set; }
        public string ImagePath { get; set; }
        public string Email { get; set; }
        public string VrstaZaposljenja { get; set; }
        public string Odjel { get; set; }
    }
}
