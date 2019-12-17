using System;
using System.Collections.Generic;

namespace tvz2api
{
    public partial class Zaposlenik
    {
        public Zaposlenik()
        {
            Izvrsitelj = new HashSet<Izvrsitelj>();
            Vijest = new HashSet<Vijest>();
        }

        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string TitulaIspred { get; set; }
        public string TitulaIza { get; set; }
        public string ImagePath { get; set; }
        public string Email { get; set; }
        public int? VrstaZaposljenjaId { get; set; }
        public int? OdjelId { get; set; }

        public virtual Odjel Odjel { get; set; }
        public virtual VrstaZaposljenja VrstaZaposljenja { get; set; }
        public virtual ICollection<Izvrsitelj> Izvrsitelj { get; set; }
        public virtual ICollection<Vijest> Vijest { get; set; }
    }
}
