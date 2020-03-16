using System.Security.AccessControl;
using tvz2api_cqrs.Common;
using tvz2api_cqrs.QueryModels;
using tvz2api_cqrs.Infrastructure.Queries;
using System.Collections.Generic;
using tvz2api_cqrs.Models.DTO;
using tvz2api_cqrs.Implementation.Specifications;

namespace tvz2api_cqrs.Implementation.Queries
{
  public class EmployeeQuery : IQuery<List<EmployeeQueryModel>>
  {
    public EmployeeQuery() { }
    public EmployeeQuery(EmployeeSpecification employeeSpecification) 
    {
      Specification = employeeSpecification;
    }
    public EmployeeSpecification Specification { get; set; }
  }
}
