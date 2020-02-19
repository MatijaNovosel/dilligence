using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.QueryModels
{
  public class StudentQueryModel
  {
    public int Id { get; set; }
    public string Jmbag { get; set; }
    public string Ime { get; set; }
    public string Prezime { get; set; }
    public string Email { get; set; }
  }
}
