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
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace tvz2api_cqrs.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class KolegijController : ControllerBase
  {
    private readonly ICommandBus _commandBus;
    private readonly IQueryBus _queryBus;

    public KolegijController(ICommandBus commandBus, IQueryBus queryBus)
    {
      _commandBus = commandBus;
      _queryBus = queryBus;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Get(int? userId = null, [FromQuery(Name = "smjerIDs[]")] List<SmjerEnum> smjerIDs = null, string name = null, bool subscribed = false, bool nonSubscribed = false)
    {
      int[] privileges = JsonConvert.DeserializeObject<int[]>(User.FindFirst("Privileges").Value);

      if(!privileges.Any(x => x == (int)PrivilegeEnum.CanViewSubjects)) {
        return Unauthorized();
      }

      // var queryOptions = QueryOptionsExtensions.GetFromRequest(Request);
      var specification = new KolegijSpecification(userId, smjerIDs, name, subscribed, nonSubscribed);
      var result = await _queryBus.ExecuteAsync(new KolegijQuery(specification));
      var count = await _queryBus.ExecuteAsync(new KolegijTotalQuery(specification));
      return Ok(new PageableCollection<KolegijQueryModel>() { Results = result, Total = count });
    }

    [HttpGet("studenti/{id}")]
    public async Task<IActionResult> GetStudenti(int id)
    {
      var result = await _queryBus.ExecuteAsync(new StudentKolegijQuery(id));
      var count = await _queryBus.ExecuteAsync(new StudentKolegijTotalQuery(id));
      return Ok(new PageableCollection<StudentQueryModel>() { Results = result, Total = count });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetails(int id)
    {
      var result = await _queryBus.ExecuteAsync(new KolegijDetailsQuery(id));
      return Ok(result);
    }

    [HttpGet("sidebar/{id}")]
    public async Task<IActionResult> GetSidebarContents(int id)
    {
      var result = await _queryBus.ExecuteAsync(new KolegijSidebarQuery(id));
      return Ok(result);
    }
  }
}