using tvz2api_cqrs.Enumerations;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Newtonsoft.Json;

namespace tvz2api_cqrs.Custom
{
  public class CustomController : ControllerBase
  {
    private bool CheckPrivileges(params PrivilegeEnum[] requestedPrivileges)
    {
      int[] privileges = JsonConvert.DeserializeObject<int[]>(User.FindFirst("Privileges").Value);
      if (!privileges.Any(userPrivilege => requestedPrivileges.ToList().Contains((PrivilegeEnum)userPrivilege)))
      {
        return false;
      }
      return true;
    }
  }
}