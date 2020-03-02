using tvz2api_cqrs.Common;
using tvz2api_cqrs.Implementation.Commands;
using tvz2api_cqrs.Implementation.Queries;
using tvz2api_cqrs.QueryModels;
using tvz2api_cqrs.Models;
using tvz2api_cqrs.Infrastructure.CommandHandlers;
using tvz2api_cqrs.Infrastructure.Commands;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace tvz2api_cqrs.Implementation.CommandHandlers
{
  public class StudentCommandHandler :
    ICommandHandlerAsync<StudentUpdatePretplataCommand>
  {
    private readonly tvz2Context _context;

    public StudentCommandHandler(tvz2Context context)
    {
      _context = context;
    }

    public async Task HandleAsync(StudentUpdatePretplataCommand command)
    {
      var oldSubscriptions = await _context.Pretplata.Where(x => x.StudentId == command.Id).ToListAsync();
      _context.Pretplata.RemoveRange(oldSubscriptions);
      List<Pretplata> newSubscriptions = command.KolegijIds
        .Select(x => new Pretplata
        {
          StudentId = command.Id,
          KolegijId = x
        })
        .ToList();
      _context.Pretplata.AddRange(newSubscriptions);
      await _context.SaveChangesAsync();
    }
  }
}
