using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.QueryModels
{
  public class EmployeeQueryModel
  {
    public int Id { get; set; }
    public string Ime { get; set; }
    public string Prezime { get; set; }
    public string TitulaIspred { get; set; }
    public string TitulaIza { get; set; }
    public string Email { get; set; }
    public int? VrstaZaposljenjaId { get; set; }
    public int? OdjelId { get; set; }
  }
}
