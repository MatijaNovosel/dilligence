using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tvz2api.Models
{
    public partial class Odjel
    {
        public Odjel()
        {
            Zaposlenik = new HashSet<Zaposlenik>();
        }

        [Column("ID")]
        public int Id { get; set; }
        [StringLength(255)]
        public string Naziv { get; set; }

        [InverseProperty("Odjel")]
        public virtual ICollection<Zaposlenik> Zaposlenik { get; set; }
    }
}
