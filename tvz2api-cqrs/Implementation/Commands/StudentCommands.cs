using tvz2api_cqrs.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace tvz2api_cqrs.Implementation.Commands
{
  public class StudentUpdatePretplataCommand : ICommand
  {
    public StudentUpdatePretplataCommand() { }
    public StudentUpdatePretplataCommand(int id, List<int> kolegijIds)
    {
      Id = id;
      KolegijIds = kolegijIds;
    }
    public int Id { get; set; }
    public List<int> KolegijIds { get; set; }
  }
}
