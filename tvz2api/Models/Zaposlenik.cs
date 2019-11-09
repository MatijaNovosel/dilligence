using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tvz2api.Models
{
    public partial class Zaposlenik
    {
        public Zaposlenik()
        {
            Izvrsitelj = new HashSet<Izvrsitelj>();
            Vijest = new HashSet<Vijest>();
        }

        [Column("ID")]
        public int Id { get; set; }
        [StringLength(255)]
        public string Ime { get; set; }
        [StringLength(255)]
        public string Prezime { get; set; }
        [StringLength(255)]
        public string TitulaIspred { get; set; }
        [StringLength(255)]
        public string TitulaIza { get; set; }
        [StringLength(255)]
        public string ImagePath { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        [Column("VrstaZaposljenjaID")]
        public int? VrstaZaposljenjaId { get; set; }
        [Column("OdjelID")]
        public int? OdjelId { get; set; }

        [ForeignKey("OdjelId")]
        [InverseProperty("Zaposlenik")]
        public virtual Odjel Odjel { get; set; }
        [ForeignKey("VrstaZaposljenjaId")]
        [InverseProperty("Zaposlenik")]
        public virtual VrstaZaposljenja VrstaZaposljenja { get; set; }
        [InverseProperty("Zaposlenik")]
        public virtual ICollection<Izvrsitelj> Izvrsitelj { get; set; }
        [InverseProperty("Objavio")]
        public virtual ICollection<Vijest> Vijest { get; set; }
    }
}
