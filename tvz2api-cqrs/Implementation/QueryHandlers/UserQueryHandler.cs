using tvz2api_cqrs.Models;
using tvz2api_cqrs.Implementation.Queries;
using tvz2api_cqrs.QueryModels;
using tvz2api_cqrs.Infrastructure.QueryHandlers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using tvz2api_cqrs.Models.DTO;
using System;

namespace tvz2api_cqrs.Implementation.QueryHandlers
{
  public class UserQueryHandler :
    IQueryHandlerAsync<UserQuery, List<UserQueryModel>>,
    IQueryHandlerAsync<UserChatQuery, List<UserChatQueryModel>>,
    IQueryHandlerAsync<UserSettingsQuery, UserSettingsQueryModel>,
    IQueryHandlerAsync<UserDetailsQuery, UserDetailsDTO>,
    IQueryHandlerAsync<UserTotalQuery, int>,
    IQueryHandlerAsync<UserSubscriptionQuery, List<int>>
  {
    private readonly lmsContext _context;

    public UserQueryHandler(lmsContext context)
    {
      _context = context;
    }

    public async Task<List<UserQueryModel>> HandleAsync(UserQuery query)
    {
      var korisnici = await _context.User
        .Where(query.Specification.Predicate)
        .Select(t => new UserQueryModel
        {
          Id = t.Id,
          Username = t.Username,
          Created = t.Created
        })
        .ToListAsync();
      return korisnici;
    }

    public async Task<int> HandleAsync(UserTotalQuery query)
    {
      var count = await _context.User
        .Where(query.Specification.Predicate)
        .Select(t => new UserQueryModel
        {
          Id = t.Id,
          Username = t.Username,
          Created = t.Created
        })
        .CountAsync();
      return count;
    }

    public async Task<List<UserChatQueryModel>> HandleAsync(UserChatQuery query)
    {
      var chats = await _context.Chat
        .Where(t => t.FirstParticipantId == query.Id || t.SecondParticipantId == query.Id)
        .Select(t => new UserChatQueryModel
        {
          Id = t.Id,
          FirstParticipant = new UserQueryModel()
          {
            Id = t.FirstParticipant.Id,
            Username = t.FirstParticipant.Username
          },
          SecondParticipant = new UserQueryModel()
          {
            Id = t.SecondParticipant.Id,
            Username = t.SecondParticipant.Username
          }
        })
        .ToListAsync();
      return chats;
    }

    public async Task<UserSettingsQueryModel> HandleAsync(UserSettingsQuery query)
    {
      var settings = await _context.UserSettings.FirstOrDefaultAsync(x => x.UserId == query.Id);
      return new UserSettingsQueryModel()
      {
        DarkMode = settings.DarkMode,
        Locale = settings.Locale
      };
    }

    public async Task<UserDetailsDTO> HandleAsync(UserDetailsQuery query)
    {
      var user = await _context.User.Include(t => t.ImageFile).FirstOrDefaultAsync(x => x.Id == query.Id);
      return new UserDetailsDTO()
      {
        Name = user.Name,
        Surname = user.Surname,
        Created = user.Created,
        Email = user.Email,
        Picture = user.ImageFile != null ? Convert.ToBase64String(user.ImageFile.Data) : null,
        Username = user.Username
      };
    }

    public async Task<List<int>> HandleAsync(UserSubscriptionQuery query)
    {
      var subscriptions = await _context.Subscription
        .Where(t => t.UserId == query.Id)
        .Select(t => (int)t.CourseId)
        .ToListAsync();
      return subscriptions;
    }
  }
}
