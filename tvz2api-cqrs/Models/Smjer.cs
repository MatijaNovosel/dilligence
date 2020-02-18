using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
  public partial class Smjer
  {
    public Smjer()
    {
      Kolegij = new HashSet<Kolegij>();
      Student = new HashSet<Student>();
    }

    public int Id { get; set; }
    public string Naziv { get; set; }
    public string SkraceniNaziv { get; set; }
    public bool? Vanredno { get; set; }

    public virtual ICollection<Kolegij> Kolegij { get; set; }
    public virtual ICollection<Student> Student { get; set; }
  }
}
