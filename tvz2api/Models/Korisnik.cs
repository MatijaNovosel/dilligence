using System;
using System.Collections.Generic;

namespace tvz2api.Models
{
    public partial class Korisnik
    {
        public Korisnik()
        {
            Exam = new HashSet<Exam>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime? Created { get; set; }

        public virtual ICollection<Exam> Exam { get; set; }
    }
}
