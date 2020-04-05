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
  public class KorisnikCommandHandler :
    ICommandHandlerAsync<KorisnikSubscribeCommand>,
    ICommandHandlerAsync<KorisnikUnsubscribeCommand>,
    ICommandHandlerAsync<KorisnikUpdateSettingsCommand>
  {
    private readonly tvz2Context _context;

    public KorisnikCommandHandler(tvz2Context context)
    {
      _context = context;
    }

    public async Task HandleAsync(KorisnikSubscribeCommand command)
    {
      if (_context.Kolegij.Where(x => x.Id == command.KolegijId).FirstOrDefault().Lozinka != command.Password)
      {
        throw new Exception("Wrong password!");
      }
      await _context.Pretplata.AddAsync(new Pretplata()
      {
        KolegijId = command.KolegijId,
        StudentId = command.UserId
      });
      await _context.SaveChangesAsync();
    }

    public async Task HandleAsync(KorisnikUnsubscribeCommand command)
    {
      var pretplata = await _context.Pretplata.Where(x => x.KolegijId == command.KolegijId && x.StudentId == command.UserId).FirstOrDefaultAsync();
      _context.Pretplata.Remove(pretplata);
      await _context.SaveChangesAsync();
    }

    public async Task HandleAsync(KorisnikUpdateSettingsCommand command)
    {
      var settings = await _context.UserSettings.FirstOrDefaultAsync(x => x.UserId == command.UserId);
      settings.DarkMode = command.DarkMode;
      await _context.SaveChangesAsync();
    }
  }
}