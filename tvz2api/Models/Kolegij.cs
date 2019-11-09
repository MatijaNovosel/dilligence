using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tvz2api.Models
{
    public partial class Kolegij
    {
        public Kolegij()
        {
            Izvrsitelj = new HashSet<Izvrsitelj>();
            SidebarContent = new HashSet<SidebarContent>();
            StudentKolegij = new HashSet<StudentKolegij>();
            Vijest = new HashSet<Vijest>();
        }

        [Column("ID")]
        public int Id { get; set; }
        [StringLength(255)]
        public string Naziv { get; set; }
        [Column("ISVU")]
        [StringLength(50)]
        public string Isvu { get; set; }
        [Column("ECTS")]
        public int? Ects { get; set; }
        [StringLength(50)]
        public string AkademskaGodina { get; set; }
        [StringLength(255)]
        public string Status { get; set; }
        [StringLength(255)]
        public string IzvedbaNastave { get; set; }
        [StringLength(255)]
        public string Cilj { get; set; }
        [StringLength(255)]
        public string Ishodi { get; set; }
        [StringLength(255)]
        public string NacinIzvodenjaPredavanja { get; set; }
        [StringLength(255)]
        public string NacinIzvodenjaAuditornih { get; set; }
        [StringLength(255)]
        public string NacinIzvodenjaLaboratorijskih { get; set; }
        [StringLength(255)]
        public string SadrzajPredavanja { get; set; }
        [StringLength(255)]
        public string SadrzajAuditornih { get; set; }
        [StringLength(255)]
        public string SadrzajLaboratorijskih { get; set; }
        [StringLength(255)]
        public string MaterijalniUvjeti { get; set; }
        [StringLength(255)]
        public string Literatura { get; set; }
        [StringLength(255)]
        public string Uvjet { get; set; }
        [StringLength(255)]
        public string ProvjeraZnanja { get; set; }
        [StringLength(255)]
        public string NaciniPolaganja { get; set; }
        [StringLength(255)]
        public string PracenjeRada { get; set; }
        [StringLength(255)]
        public string Napomena { get; set; }
        [StringLength(255)]
        public string Preduvjeti { get; set; }
        [Column("ISVUEkvivalencije")]
        [StringLength(255)]
        public string Isvuekvivalencije { get; set; }
        [Column("IzradioID")]
        public int? IzradioId { get; set; }
        [Column("SmjerID")]
        public int? SmjerId { get; set; }
        [Column("URL")]
        [StringLength(50)]
        public string Url { get; set; }

        [ForeignKey("SmjerId")]
        [InverseProperty("Kolegij")]
        public virtual Smjer Smjer { get; set; }
        [InverseProperty("Kolegij")]
        public virtual ICollection<Izvrsitelj> Izvrsitelj { get; set; }
        [InverseProperty("Kolegij")]
        public virtual ICollection<SidebarContent> SidebarContent { get; set; }
        [InverseProperty("Kolegij")]
        public virtual ICollection<StudentKolegij> StudentKolegij { get; set; }
        [InverseProperty("Kolegij")]
        public virtual ICollection<Vijest> Vijest { get; set; }
    }
}
