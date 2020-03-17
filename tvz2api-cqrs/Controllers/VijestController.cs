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
using Microsoft.AspNetCore.SignalR;
using tvz2api_cqrs.Hubs;

namespace tvz2api_cqrs.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class VijestController : ControllerBase
  {
    private readonly ICommandBus _commandBus;
    private readonly IQueryBus _queryBus;
    private readonly IHubContext<VijestiHub> _hubContext;

    public VijestController(ICommandBus commandBus, IQueryBus queryBus, IHubContext<VijestiHub> vijestiHub)
    {
      _commandBus = commandBus;
      _queryBus = queryBus;
      _hubContext = vijestiHub;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetails(int id)
    {
      var result = await _queryBus.ExecuteAsync(new VijestQuery(id));
      await this._hubContext.Clients.All.SendAsync("EVENT", "Hello world!");
      return Ok(result);
    }
  }
}