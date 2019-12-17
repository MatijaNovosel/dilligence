using System;
using System.Collections.Generic;

namespace tvz2api
{
    public partial class Korisnik
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime? Created { get; set; }
    }
}
