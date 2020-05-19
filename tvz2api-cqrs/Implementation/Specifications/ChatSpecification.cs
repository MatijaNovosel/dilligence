using tvz2api_cqrs.Models;
using tvz2api_cqrs.Enumerations;
using tvz2api_cqrs.Infrastructure.Specifications;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace tvz2api_cqrs.Implementation.Specifications
{

  public class ChatSpecification : ISpecification<User>
  {
    public ChatSpecification() { }

    public int Id { get; set; }
    public string SearchText { get; set; }

    public Expression<Func<User, bool>> Predicate
    {
      get
      {
        Expression<Func<User, bool>> predicate = t => true;

        predicate = predicate.And(t => t.Id != Id);
        predicate = predicate.And(t => !t.ChatFirstParticipant.Any(x => x.FirstParticipantId == Id || x.SecondParticipantId == Id));
        predicate = predicate.And(t => !t.ChatSecondParticipant.Any(x => x.FirstParticipantId == Id || x.SecondParticipantId == Id));

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
          predicate = predicate.And(t => (t.Name + ' ' + t.Surname + ' ' + t.Username).ToLower().Contains(SearchText.ToLower()));
        }

        return predicate.Expand();
      }
    }
  }

}
