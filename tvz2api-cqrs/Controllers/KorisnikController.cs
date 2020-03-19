using tvz2api_cqrs.Models;
using tvz2api_cqrs.Infrastructure.Commands;
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
  public class KorisnikController : ControllerBase
  {
    private readonly ICommandBus _commandBus;
    private readonly IQueryBus _queryBus;

    public KorisnikController(ICommandBus commandBus, IQueryBus queryBus)
    {
      _commandBus = commandBus;
      _queryBus = queryBus;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string name = null)
    {
      // var queryOptions = QueryOptionsExtensions.GetFromRequest(Request);
      var specification = new KorisnikSpecification(name);
      var result = await _queryBus.ExecuteAsync(new KorisnikQuery(specification));
      var count = await _queryBus.ExecuteAsync(new KorisnikTotalQuery(specification));
      return Ok(new PageableCollection<KorisnikQueryModel>() { Results = result, Total = count });
    }

    [HttpGet("chat/{id}")]
    public async Task<IActionResult> GetChats(int id)
    {
      var result = await _queryBus.ExecuteAsync(new KorisnikChatQuery() { Id = id });
      return Ok(result);
    }
  }
}