using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LinqKit;
using tvz2api_cqrs.Enumerations;
using tvz2api_cqrs.Infrastructure.Specifications;
using tvz2api_cqrs.Models;
using tvz2api_cqrs.Models.DTO;

namespace tvz2api_cqrs.Implementation.Specifications
{
  public class CourseSpecification : ISpecification<Course>
  {
    public CourseSpecification(int? userId = null, List<SpecializationEnum> smjerIds = null, string name = null, bool subscribed = false, bool nonSubscribed = false)
    {
      SpecializationIds = smjerIds;
      Name = name;
      Subscribed = subscribed;
      NonSubscribed = nonSubscribed;
      UserId = userId;
    }

    public List<SpecializationEnum> SpecializationIds { get; }
    public string Name { get; }
    public bool Subscribed { get; }
    public bool NonSubscribed { get; }
    public int? UserId { get; }

    public Expression<Func<Course, bool>> Predicate
    {
      get
      {
        Expression<Func<Course, bool>> predicate = t => true;

        if (SpecializationIds != null && SpecializationIds.Count() > 0)
        {
          /*
          
            Inace linija ide: predicate = predicate.And(t => SmjerIds.Any(x => (int)x == t.SmjerId));
            EF ne moze prevesti taj query u SQL zbog problema na EFCore 3.1.0 (https://github.com/dotnet/efcore/issues/17342).

          */
          predicate = predicate.And(t => SpecializationIds.Select(x => (int)x).Contains((int)t.SpecializationId));
        }

        if (!string.IsNullOrWhiteSpace(Name))
        {
          predicate = predicate.And(t => t.Name.ToLower().Contains(Name.ToLower()));
        }

        // Ako je odabran ili Subscribed ili NonSubscribed, logicki XOR
        if (Subscribed != NonSubscribed)
        {
          if (Subscribed)
          {
            predicate = predicate.And(t => t.Subscription.Any(x => x.CourseId == t.Id && x.UserId == UserId));
          }

          if (NonSubscribed)
          {
            predicate = predicate.And(t => !t.Subscription.Any(x => x.CourseId == t.Id && x.UserId == UserId));
          }
        }

        return predicate.Expand();
      }
    }
  }

  public class CourseUserSpecification : ISpecification<User>
  {
    public CourseUserSpecification(String name, String surname, String username, int courseId)
    {
      Name = name;
      Surname = surname;
      Username = username;
      CourseId = courseId;
    }

    public string Name { get; }
    public string Surname { get; }
    public string Username { get; }
    public int CourseId { get; set; }

    public Expression<Func<User, bool>> Predicate
    {
      get
      {
        Expression<Func<User, bool>> predicate = t => true;

        if (!string.IsNullOrWhiteSpace(Name))
        {
          predicate = predicate.And(t => t.Name.ToLower().Contains(Name.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(Surname))
        {
          predicate = predicate.And(t => t.Surname.ToLower().Contains(Surname.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(Username))
        {
          predicate = predicate.And(t => t.Username.ToLower().Contains(Username.ToLower()));
        }

        predicate = predicate.And(t => t.Subscription.Any(x => x.CourseId == CourseId));

        return predicate.Expand();
      }
    }
  }

  public class CourseNotificationsSpecification : ISpecification<Notification>
  {
    public CourseNotificationsSpecification(int courseId, bool showArchived, bool showNonArchived)
    {
      CourseId = courseId;
      ShowArchived = showArchived;
      ShowNonArchived = showNonArchived;
    }

    public int CourseId { get; }
    public bool ShowArchived { get; }
    public bool ShowNonArchived { get; }

    public Expression<Func<Notification, bool>> Predicate
    {
      get
      {
        Expression<Func<Notification, bool>> predicate = t => true;

        predicate = predicate.And(t => t.CourseId == CourseId);

        if (ShowArchived != ShowNonArchived)
        {
          if (ShowArchived)
          {
            predicate = predicate.And(t => t.ExpiresAt <= DateTime.Now);
          }
          if (ShowNonArchived)
          {
            predicate = predicate.And(t => t.ExpiresAt > DateTime.Now);
          }
        }

        return predicate.Expand();
      }
    }
  }
}