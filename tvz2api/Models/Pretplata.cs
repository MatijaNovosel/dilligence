using System;
using System.Collections.Generic;

namespace tvz2api.Models
{
    public partial class Pretplata
    {
        public int Id { get; set; }
        public int? StudentId { get; set; }
        public int? KolegijId { get; set; }

        public virtual Kolegij Kolegij { get; set; }
        public virtual Student Student { get; set; }
    }
}
