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
using tvz2api_cqrs.Enumerations;

namespace tvz2api_cqrs.Implementation.QueryHandlers
{
  public class UserQueryHandler :
    IQueryHandlerAsync<UserQuery, List<UserQueryModel>>,
    IQueryHandlerAsync<UserChatQuery, List<UserChatQueryModel>>,
    IQueryHandlerAsync<UserGetAllQuery, List<UserQueryModel>>,
    IQueryHandlerAsync<UserSettingsQuery, UserSettingsQueryModel>,
    IQueryHandlerAsync<UserDetailsQuery, UserDetailsDTO>,
    IQueryHandlerAsync<UserBlacklistQuery, List<BlacklistDTO>>,
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
          Created = t.Created,
          Name = t.Name,
          Surname = t.Surname,
          Email = t.Email
        })
        .ToListAsync();
      return korisnici;
    }

    public async Task<List<UserQueryModel>> HandleAsync(UserGetAllQuery query)
    {
      var korisnici = await _context.User
        .Select(t => new UserQueryModel
        {
          Id = t.Id,
          Username = t.Username,
          Created = t.Created,
          Name = t.Name,
          Surname = t.Surname,
          Email = t.Email
        })
        .ToListAsync();
      return korisnici;
    }

    public async Task<List<BlacklistDTO>> HandleAsync(UserBlacklistQuery query)
    {
      var user = await _context
        .User
        .Include(x => x.UserCoursePrivilege)
        .Include(x => x.Subscription)
        .ThenInclude(x => x.Course)
        .FirstOrDefaultAsync(x => x.Id == query.UserId);

      var subscriptions = user.Subscription.ToList();
      var privileges = user.UserCoursePrivilege.ToList();
      var blacklist = new List<BlacklistDTO>();

      subscriptions.ForEach(x =>
      {
        var extractedPrivileges = privileges.Where(y => y.CourseId == x.CourseId);
        if (extractedPrivileges.Count() != 0 && extractedPrivileges != null)
        {
          if (!extractedPrivileges.Any(y => y.PrivilegeId == (int)PrivilegeEnum.IsInvolvedWithCourse))
          {
            blacklist.Add(new BlacklistDTO
            {
              Blacklisted = x.Blacklisted,
              CourseId = x.CourseId,
              Name = x.Course.Name
            });
          }
        }
        else
        {
          blacklist.Add(new BlacklistDTO
          {
            Blacklisted = x.Blacklisted,
            CourseId = x.CourseId,
            Name = x.Course.Name
          });
        }
      });

      return blacklist;
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
        .Include(t => t.FirstParticipant)
        .ThenInclude(t => t.ImageFile)
        .Include(t => t.SecondParticipant)
        .ThenInclude(t => t.ImageFile)
        .Where(t => t.FirstParticipantId == query.Id || t.SecondParticipantId == query.Id)
        .Select(t => new UserChatQueryModel
        {
          Id = t.Id,
          FirstParticipant = new UserQueryModel()
          {
            Id = t.FirstParticipant.Id,
            Username = t.FirstParticipant.Username,
            Picture = t.FirstParticipant.ImageFile != null ? Convert.ToBase64String(t.FirstParticipant.ImageFile.Data) : null,
          },
          SecondParticipant = new UserQueryModel()
          {
            Id = t.SecondParticipant.Id,
            Username = t.SecondParticipant.Username,
            Picture = t.SecondParticipant.ImageFile != null ? Convert.ToBase64String(t.SecondParticipant.ImageFile.Data) : null,
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
      var user = await _context
        .User
        .Include(t => t.ImageFile)
        .Include(p => p.UserPrivilege)
        .Include(p => p.UserCoursePrivilege)
        .ThenInclude(p => p.Course)
        .Include(p => p.Subscription)
        .FirstOrDefaultAsync(x => x.Id == query.Id);

      return new UserDetailsDTO()
      {
        Id = user.Id,
        Name = user.Name,
        Surname = user.Surname,
        Created = user.Created,
        Email = user.Email,
        Picture = user.ImageFile != null ? Convert.ToBase64String(user.ImageFile.Data) : null,
        Username = user.Username,
        Privileges = new UserPrivilegeDTO()
        {
          GeneralPrivileges = user.UserPrivilege.Select(x => x.PrivilegeId).ToList(),
          Courses = user.UserCoursePrivilege
          .GroupBy(x => x.CourseId)
          .Select(x => new UserCoursePrivilegeDTO()
          {
            Id = x.FirstOrDefault().CourseId,
            Name = x.FirstOrDefault().Course.Name,
            Privileges = user
              .UserCoursePrivilege
              .Where(y => y.CourseId == x.FirstOrDefault().CourseId)
              .Select(y => y.PrivilegeId)
              .ToList()
          })
          .ToList()
        }
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
