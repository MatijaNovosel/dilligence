using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using tvz2api_cqrs.Common;
using tvz2api_cqrs.Implementation.Commands;
using tvz2api_cqrs.Implementation.Queries;
using tvz2api_cqrs.Infrastructure.CommandHandlers;
using tvz2api_cqrs.Infrastructure.Commands;
using tvz2api_cqrs.Models;
using tvz2api_cqrs.QueryModels;

namespace tvz2api_cqrs.Implementation.CommandHandlers
{
  public class StudentCommandHandler :
    ICommandHandlerAsync<StudentSubscribeCommand>,
    ICommandHandlerAsync<StudentUnsubscribeCommand>
  {
    private readonly tvz2Context _context;

    public StudentCommandHandler(tvz2Context context)
    {
      _context = context;
    }

    public async Task HandleAsync(StudentSubscribeCommand command)
    {
      if (_context.Kolegij.Where(x => x.Id == command.KolegijId).FirstOrDefault().Lozinka != command.Password)
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

    public async Task HandleAsync(StudentUnsubscribeCommand command)
    {
      var pretplata = await _context.Pretplata.Where(x => x.KolegijId == command.KolegijId && x.StudentId == command.StudentId).FirstOrDefaultAsync();
      _context.Pretplata.Remove(pretplata);
      await _context.SaveChangesAsync();
    }
  }
}