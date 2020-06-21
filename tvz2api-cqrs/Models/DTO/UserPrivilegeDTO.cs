using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models.DTO
{
  public class UserPrivilegeDTO
  {
    public List<int> GeneralPrivileges { get; set; }
    public List<UserCoursePrivilegeDTO> Courses { get; set; }
  }

  public class UserCoursePrivilegeDTO
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public List<int> Privileges { get; set; }
  }
}
