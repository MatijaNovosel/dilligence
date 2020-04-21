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
        public string Name { get; set; }
        public string Shorthand { get; set; }

        public virtual ICollection<Course> Course { get; set; }
    }
}
