using tvz2api_cqrs.Models;
using tvz2api_cqrs.Infrastructure;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace tvz2api_cqrs.Implementation.Specifications
{
  public class KolegijSpecification : ISpecification<Kolegij>
  {
    public KolegijSpecification(string clientName, string clientId, string clientVAT, string objectIdExternal, int? statementTypeId, string barcode, List<StatementLifeCycleEnum> statusIds = null, bool onlyActiveStatements = false)
    {
      smjerIDs = null,
      name = null,
      minECTS = 1,
      maxECTS = 6,
      isvu = null,
      skip = 0,
      take = null
    }

    public List<StatementLifeCycleEnum> StatusIds { get; }
    public bool OnlyActiveStatements { get; }
    public string ClientName { get; }
    public string ClientId { get; }
    public string ClientVAT { get; }
    public string ObjectIdExternal { get; }
    public int? StatementTypeId { get; }
    public string Barcode { get; }

    public Expression<Func<Statement, bool>> Predicate
    {
      get
      {
        Expression<Func<Statement, bool>> predicate = t => true;

        if (StatementTypeId.HasValue)
        {
          predicate = predicate.And(t => t.StatementTypeID == StatementTypeId);
        }

        if (StatusIds != null && StatusIds.Count() > 0)
        {
          predicate = predicate.And(t => StatusIds.Any(x => (int)x == t.LifeCycleID));
        }

        if (!string.IsNullOrWhiteSpace(ClientName))
        {
          predicate = predicate.And(t => t.ClientName.ToLower().Contains(ClientName.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(ClientId))
        {
          predicate = predicate.And(t => t.ClientID == ClientId);
        }

        if (!string.IsNullOrWhiteSpace(ClientVAT))
        {
          predicate = predicate.And(t => t.ClientVAT == ClientVAT);
        }

        if (!string.IsNullOrWhiteSpace(ObjectIdExternal))
        {
          predicate = predicate.And(t => t.ObjectIDExternal == ObjectIdExternal);
        }

        if (!string.IsNullOrWhiteSpace(Barcode))
        {
          predicate = predicate.And(t => t.DocumentBarcode == Barcode);
        }

        return predicate.Expand();
      }
    }
  }
}
