using System;

namespace tvz2api.Models.DTO
{
    public class VijestDTO 
    {
        public string Naslov { get; set; }
        public DateTime? Datum { get; set; }
        public string Opis { get; set; }
        public string Objavio { get; set; }
    }
}