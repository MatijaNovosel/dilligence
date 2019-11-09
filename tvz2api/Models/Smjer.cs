using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tvz2api.Models
{
    public partial class Smjer
    {
        public Smjer()
        {
            Kolegij = new HashSet<Kolegij>();
            Student = new HashSet<Student>();
        }

        [Column("ID")]
        public int Id { get; set; }
        [StringLength(255)]
        public string Naziv { get; set; }
        [StringLength(20)]
        public string SkraceniNaziv { get; set; }
        public bool? Vanredno { get; set; }

        [InverseProperty("Smjer")]
        public virtual ICollection<Kolegij> Kolegij { get; set; }
        [InverseProperty("Smjer")]
        public virtual ICollection<Student> Student { get; set; }
    }
}
