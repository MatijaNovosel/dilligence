using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tvz2api.Models
{
    public partial class Vijest
    {
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(255)]
        public string Naslov { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Datum { get; set; }
        [StringLength(255)]
        public string Opis { get; set; }
        [Column("KolegijID")]
        public int? KolegijId { get; set; }
        [Column("ObjavioID")]
        public int? ObjavioId { get; set; }

        [ForeignKey("KolegijId")]
        [InverseProperty("Vijest")]
        public virtual Kolegij Kolegij { get; set; }
        [ForeignKey("ObjavioId")]
        [InverseProperty("Vijest")]
        public virtual Zaposlenik Objavio { get; set; }
    }
}
