using System;
using System.Collections.Generic;

namespace tvz2api
{
    public partial class UlogaIzvrsitelja
    {
        public UlogaIzvrsitelja()
        {
            Izvrsitelj = new HashSet<Izvrsitelj>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }

        public virtual ICollection<Izvrsitelj> Izvrsitelj { get; set; }
    }
}
