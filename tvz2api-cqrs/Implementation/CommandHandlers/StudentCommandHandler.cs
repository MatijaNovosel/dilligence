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
      if (_context.Kolegij.Where(x => x.Id == command.KolegijId).FirstOrDefault().Password != command.Password)
      {
        throw new Exception("Wrong password!");
      }
      await _context.Pretplata.AddAsync(new Pretplata()
      {
        KolegijId = command.KolegijId,
        StudentId = command.StudentId
      });
      await _context.SaveChangesAsync();
    }
  }
}
