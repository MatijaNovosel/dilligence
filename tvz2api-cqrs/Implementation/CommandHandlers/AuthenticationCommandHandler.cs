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
  public class AuthenticationCommandHandler :
    ICommandHandlerAsync<AuthenticationRegisterCommand>
  {
    private readonly tvz2Context _context;

    public AuthenticationCommandHandler(tvz2Context context)
    {
      _context = context;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
      using (var hmac = new System.Security.Cryptography.HMACSHA512())
      {
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
      }
    }

    public async Task HandleAsync(AuthenticationRegisterCommand command)
    {
      if (await _context.Korisnik.AnyAsync(x => x.Username == command.Username))
      {
        throw new Exception("Username already exists!");
      }

      var newUser = new Korisnik() { Username = command.Username };

      byte[] passwordHash, passwordSalt;
      CreatePasswordHash(command.Password, out passwordHash, out passwordSalt);
      newUser.PasswordHash = passwordHash;
      newUser.PasswordSalt = passwordSalt;

      await _context.Korisnik.AddAsync(newUser);
      await _context.SaveChangesAsync();
    }
  }
}
