using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tvz2api.Models
{
    public partial class VrstaZaposljenja
    {
        public VrstaZaposljenja()
        {
            Zaposlenik = new HashSet<Zaposlenik>();
        }

        [Column("ID")]
        public int Id { get; set; }
        [StringLength(255)]
        public string Naziv { get; set; }

        [InverseProperty("VrstaZaposljenja")]
        public virtual ICollection<Zaposlenik> Zaposlenik { get; set; }
    }
}
