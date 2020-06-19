using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tvz2api_cqrs.Implementation.Queries;
using tvz2api_cqrs.Infrastructure.QueryHandlers;
using tvz2api_cqrs.Models;
using tvz2api_cqrs.Models.DTO;
using tvz2api_cqrs.QueryModels;
using System;

namespace tvz2api_cqrs.Implementation.QueryHandlers
{
  public class AuthenticationQueryHandler :
    IQueryHandlerAsync<AuthenticationCheckUsernameQuery, bool>
  {
    private readonly lmsContext _context;

    public AuthenticationQueryHandler(lmsContext context)
    {
      _context = context;
    }

    public async Task<bool> HandleAsync(AuthenticationCheckUsernameQuery query)
    {
      var check = await _context.User.AnyAsync(x => String.Equals(x.Username.ToLower(), query.Username.ToLower()));
      return check;
    }
  }
}