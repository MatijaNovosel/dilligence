using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tvz2api.Models
{
    public partial class StudentKolegij
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("StudentID")]
        public int? StudentId { get; set; }
        [Column("KolegijID")]
        public int? KolegijId { get; set; }

        [ForeignKey("KolegijId")]
        [InverseProperty("StudentKolegij")]
        public virtual Kolegij Kolegij { get; set; }
        [ForeignKey("StudentId")]
        [InverseProperty("StudentKolegij")]
        public virtual Student Student { get; set; }
    }
}
