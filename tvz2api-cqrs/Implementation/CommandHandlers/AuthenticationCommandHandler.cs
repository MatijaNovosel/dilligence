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
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using tvz2api_cqrs.Models.DTO;
using Newtonsoft.Json;
using tvz2api_cqrs.Enumerations;

namespace tvz2api_cqrs.Implementation.CommandHandlers
{
  public class AuthenticationCommandHandler :
    ICommandHandlerAsync<AuthenticationRegisterCommand>,
    ICommandHandlerAsync<AuthenticationLoginCommand, LoginUserDTO>
  {
    private readonly tvz2Context _context;
    private readonly IConfiguration _config;

    public AuthenticationCommandHandler(tvz2Context context, IConfiguration config)
    {
      _context = context;
      _config = config;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
      using (var hmac = new System.Security.Cryptography.HMACSHA512())
      {
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
      }
    }

    private bool verifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
      using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
      {
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        for (int i = 0; i < computedHash.Length; i++)
        {
          if (computedHash[i] != passwordHash[i]) return false;
        }
      }
      return true;
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

    public async Task<ICommandResult<LoginUserDTO>> HandleAsync(AuthenticationLoginCommand command)
    {
      var user = await _context.Korisnik.Include(p => p.UserPrivileges).FirstOrDefaultAsync(x => x.Username == command.Username);
      if (user == null || !verifyPasswordHash(command.Password, user.PasswordHash, user.PasswordSalt))
      {
        throw new Exception("User credentials are wrong or the hashing integrity is faulty!");
      }

      var privileges = user.UserPrivileges.Select(x => x.PrivilegeId).ToArray();

      var claims = new[] {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Username),
        new Claim("Privileges", JsonConvert.SerializeObject(privileges))
      };

      // In order to make sure the claims are valid, created a key and hash it
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
      var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

      // Create the token
      var tokenDescriptor = new SecurityTokenDescriptor()
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddDays(3),
        SigningCredentials = credentials
      };

      var tokenHandler = new JwtSecurityTokenHandler();
      var token = tokenHandler.CreateToken(tokenDescriptor);

      return CommandResult<LoginUserDTO>.Success(new LoginUserDTO(tokenHandler.WriteToken(token), user.Username, user.Id));
    }
  }
}
