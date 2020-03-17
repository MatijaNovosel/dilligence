using tvz2api_cqrs.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tvz2api_cqrs.QueryModels;

namespace tvz2api_cqrs.Implementation.Commands
{
  public class CreateVijestCommand : ICommand<VijestQueryModel>
  {
    public CreateVijestCommand() { }
    public string Naslov { get; set; }
    public string Opis { get; set; }
    public int? ObjavioId { get; set; }
    public int? KolegijId { get; set; }
    public DateTime? Datum { get; set; }
  }
}
