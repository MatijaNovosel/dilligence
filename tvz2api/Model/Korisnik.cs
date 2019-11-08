using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tvz2api.Model
{
    public partial class Korisnik
    {
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(255)]
        public string Username { get; set; }
        [MaxLength(1)]
        public byte[] PasswordHash { get; set; }
        [MaxLength(1)]
        public byte[] PasswordSalt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Created { get; set; }
    }
}
