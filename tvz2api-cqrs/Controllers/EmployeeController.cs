using tvz2api_cqrs.Models;
using tvz2api_cqrs.Infrastructure.Commands;
using tvz2api_cqrs.Implementation.Commands;
using tvz2api_cqrs.Enumerations;
using tvz2api_cqrs.Implementation.Queries;
using tvz2api_cqrs.QueryModels;
using tvz2api_cqrs.Infrastructure.Messaging;
using tvz2api_cqrs.Implementation.Specifications;
using Microsoft.AspNetCore.Mvc;
using tvz2api_cqrs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tvz2api_cqrs.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EmployeeController : ControllerBase
  {
    private readonly IQueryBus _queryBus;

    public EmployeeController(IQueryBus queryBus)
    {
      _queryBus = queryBus;
    }

    [HttpGet]
    public async Task<IActionResult> Get(
      [FromQuery(Name = "odjelIds[]")] List<OdjelEnum> odjelIds = null,
      [FromQuery(Name = "zaposljenjeIds[]")] List<ZaposljenjeEnum> zaposljenjeIds = null,
      string name = null,
      string surname = null
    )
    {
      var specification = new EmployeeSpecification(name, surname, odjelIds, zaposljenjeIds);
      var result = await _queryBus.ExecuteAsync(new EmployeeQuery(specification));
      return Ok(result);
    }
  }
}