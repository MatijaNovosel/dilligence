using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
    public partial class Specialization
    {
        public Specialization()
        {
            Course = new HashSet<Course>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }
        public string SkraceniNaziv { get; set; }
        public bool? Vanredno { get; set; }

        public virtual ICollection<Course> Course { get; set; }
    }
}
