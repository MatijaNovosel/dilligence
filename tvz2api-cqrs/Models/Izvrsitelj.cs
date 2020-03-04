using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
    public partial class Izvrsitelj
    {
        public int Id { get; set; }
        public int? ZaposlenikId { get; set; }
        public int? KolegijId { get; set; }
        public int? UlogaIzvrsiteljaId { get; set; }

        public virtual Kolegij Kolegij { get; set; }
        public virtual UlogaIzvrsitelja UlogaIzvrsitelja { get; set; }
        public virtual Zaposlenik Zaposlenik { get; set; }
    }
}
