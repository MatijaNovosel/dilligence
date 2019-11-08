using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tvz2api.Model
{
    public partial class Student
    {
        public Student()
        {
            StudentKolegij = new HashSet<StudentKolegij>();
        }

        [Column("ID")]
        public int Id { get; set; }
        [Column("JMBAG")]
        [StringLength(10)]
        public string Jmbag { get; set; }
        [StringLength(255)]
        public string Ime { get; set; }
        [StringLength(255)]
        public string Prezime { get; set; }
        [StringLength(255)]
        public string ImagePath { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        [Column("SmjerID")]
        public int? SmjerId { get; set; }

        [ForeignKey("SmjerId")]
        [InverseProperty("Student")]
        public virtual Smjer Smjer { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<StudentKolegij> StudentKolegij { get; set; }
    }
}
