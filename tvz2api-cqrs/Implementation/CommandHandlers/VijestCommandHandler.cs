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
  public class VijestCommandHandler :
    ICommandHandlerAsync<CreateVijestCommand, VijestQueryModel>
  {
    private readonly tvz2Context _context;

    public VijestCommandHandler(tvz2Context context)
    {
      _context = context;
    }

    public async Task<ICommandResult<VijestQueryModel>> HandleAsync(CreateVijestCommand command)
    {
      Vijest vijest = new Vijest()
      {
        KolegijId = 147,
        ObjavioId = 1,
        Naslov = command.Naslov,
        Opis = "Hardkodirani opis",
        Datum = DateTime.Now
      };
      await _context.Vijest.AddAsync(vijest);
      await _context.SaveChangesAsync();
      return CommandResult<VijestQueryModel>.Success(new VijestQueryModel()
      {
        Id = vijest.Id,
        KolegijId = vijest.KolegijId,
        ObjavioId = vijest.ObjavioId,
        Naslov = vijest.Naslov,
        Opis = vijest.Opis,
        Datum = vijest.Datum
      });
    }
  }
}
