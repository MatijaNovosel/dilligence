using System;
using System.Collections.Generic;

namespace tvz2api.Models
{
    public partial class Korisnik 
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public byte[] passwordHash { 
            get; 
            set; 
        }
        public byte[] passwordSalt { 
            get; 
            set; 
        }
        public DateTime Created { get; set; }
    }
}