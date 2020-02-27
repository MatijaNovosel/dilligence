using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tvz2api.Models.DTO
{
    public class KorisnikDTO : BaseDTO
    {
        public string Username { get; set; }
        public DateTime? Created { get; set; }
    }
}
