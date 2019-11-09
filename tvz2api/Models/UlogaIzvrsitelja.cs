using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tvz2api.Models
{
    public partial class UlogaIzvrsitelja
    {
        public UlogaIzvrsitelja()
        {
            Izvrsitelj = new HashSet<Izvrsitelj>();
        }

        [Column("ID")]
        public int Id { get; set; }
        [StringLength(255)]
        public string Naziv { get; set; }

        [InverseProperty("UlogaIzvrsitelja")]
        public virtual ICollection<Izvrsitelj> Izvrsitelj { get; set; }
    }
}
