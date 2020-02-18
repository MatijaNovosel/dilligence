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
    public string Url { get; set; }
    public string AkademskaGodina { get; set; }
    public string Status { get; set; }
    public string IzvedbaNastave { get; set; }
    public string Cilj { get; set; }
    public string Ishodi { get; set; }
    public string NacinIzvodenjaPredavanja { get; set; }
    public string NacinIzvodenjaAuditornih { get; set; }
    public string NacinIzvodenjaLaboratorijskih { get; set; }
    public string SadrzajPredavanja { get; set; }
    public string SadrzajAuditornih { get; set; }
    public string SadrzajLaboratorijskih { get; set; }
    public string MaterijalniUvjeti { get; set; }
    public string Literatura { get; set; }
    public string Uvjet { get; set; }
    public string ProvjeraZnanja { get; set; }
    public string NaciniPolaganja { get; set; }
    public string PracenjeRada { get; set; }
    public string Napomena { get; set; }
    public string Preduvjeti { get; set; }
    public string Isvuekvivalencije { get; set; }
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
