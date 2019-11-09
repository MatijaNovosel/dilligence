using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tvz2api.Models
{
    public partial class Izvrsitelj
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("ZaposlenikID")]
        public int? ZaposlenikId { get; set; }
        [Column("KolegijID")]
        public int? KolegijId { get; set; }
        [Column("UlogaIzvrsiteljaID")]
        public int? UlogaIzvrsiteljaId { get; set; }

        [ForeignKey("KolegijId")]
        [InverseProperty("Izvrsitelj")]
        public virtual Kolegij Kolegij { get; set; }
        [ForeignKey("UlogaIzvrsiteljaId")]
        [InverseProperty("Izvrsitelj")]
        public virtual UlogaIzvrsitelja UlogaIzvrsitelja { get; set; }
        [ForeignKey("ZaposlenikId")]
        [InverseProperty("Izvrsitelj")]
        public virtual Zaposlenik Zaposlenik { get; set; }
    }
}
