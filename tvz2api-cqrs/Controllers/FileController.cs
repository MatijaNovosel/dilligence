using tvz2api_cqrs.Models;
using tvz2api_cqrs.Infrastructure.Commands;
using tvz2api_cqrs.Implementation.Queries;
using tvz2api_cqrs.Implementation.Commands;
using tvz2api_cqrs.QueryModels;
using tvz2api_cqrs.Infrastructure.Messaging;
using tvz2api_cqrs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace tvz2api_cqrs.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class FileController : ControllerBase
  {
    private readonly ICommandBus _commandBus;
    private readonly IQueryBus _queryBus;

    public FileController(ICommandBus commandBus, IQueryBus queryBus)
    {
      _commandBus = commandBus;
      _queryBus = queryBus;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadFiles([FromBody]FileUploadCommand command)
    {
      var uploadedFileIds = await _commandBus.ExecuteAsync<List<int>>(command);
      return Ok(uploadedFileIds);
    }

    [HttpGet("sidebar/{id}")]
    public async Task<IActionResult> GetFiles(int id)
    {
      var files = await _queryBus.ExecuteAsync(new FileQuery(id));
      return Ok(files);
    }
  }
}