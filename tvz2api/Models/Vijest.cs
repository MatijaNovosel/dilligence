using System;
using System.Collections.Generic;

namespace tvz2api
{
    public partial class Vijest
    {
        public int Id { get; set; }
        public string Naslov { get; set; }
        public DateTime? Datum { get; set; }
        public string Opis { get; set; }
        public int? KolegijId { get; set; }
        public int? ObjavioId { get; set; }

        public virtual Kolegij Kolegij { get; set; }
        public virtual Zaposlenik Objavio { get; set; }
    }
}
