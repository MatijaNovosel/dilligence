using tvz2api_cqrs.Models;
using tvz2api_cqrs.Infrastructure.Commands;
using tvz2api_cqrs.Implementation.Queries;
using tvz2api_cqrs.QueryModels;
using tvz2api_cqrs.Infrastructure.Messaging;
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
    public class KolegijController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;

        public KolegijController(ICommandBus commandBus, IQueryBus queryBus)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            // var queryOptions = QueryOptionsExtensions.GetFromRequest(Request);
            var result = await _queryBus.ExecuteAsync(new KolegijQuery());
            var count = await _queryBus.ExecuteAsync(new KolegijTotalQuery());
            return Ok(new PageableCollection<KolegijQueryModel>() { Results = result, Total = count });
        }

        [HttpGet("studenti/{id}")]
        public async Task<IActionResult> GetStudenti(int id)
        {
          var result = await _queryBus.ExecuteAsync(new StudentKolegijQuery(id));
          var count = await _queryBus.ExecuteAsync(new StudentKolegijTotalQuery(id));
          return Ok(new PageableCollection<StudentQueryModel>() { Results = result, Total = count });
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
          var result = await _queryBus.ExecuteAsync(new KolegijDetailsQuery(id));
          return Ok(result);
        }
    }
}