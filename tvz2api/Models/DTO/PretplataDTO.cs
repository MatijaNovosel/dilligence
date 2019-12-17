using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tvz2api.Models.DTO
{
    public class PretplataDTO : BaseDTO
    {
        public int? StudentId { get; set; }
        public int? KolegijId { get; set; }
    }
}
