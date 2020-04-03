using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
    public partial class Kolegij
    {
        public Kolegij()
        {
            Exam = new HashSet<Exam>();
            Izvrsitelj = new HashSet<Izvrsitelj>();
            Pretplata = new HashSet<Pretplata>();
            SidebarContent = new HashSet<SidebarContent>();
            StudentKolegij = new HashSet<StudentKolegij>();
            Vijest = new HashSet<Vijest>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Isvu { get; set; }
        public int? Ects { get; set; }
        public string Lozinka { get; set; }
        public int? IzradioId { get; set; }
        public int? SmjerId { get; set; }

        public virtual Smjer Smjer { get; set; }
        public virtual ICollection<Exam> Exam { get; set; }
        public virtual ICollection<Izvrsitelj> Izvrsitelj { get; set; }
        public virtual ICollection<Pretplata> Pretplata { get; set; }
        public virtual ICollection<SidebarContent> SidebarContent { get; set; }
        public virtual ICollection<StudentKolegij> StudentKolegij { get; set; }
        public virtual ICollection<Vijest> Vijest { get; set; }
    }
}
