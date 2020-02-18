using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
  public partial class SidebarContent
  {
    public SidebarContent()
    {
      SidebarContentFile = new HashSet<SidebarContentFile>();
    }

    public int Id { get; set; }
    public string Naslov { get; set; }
    public int? KolegijId { get; set; }

    public virtual Kolegij Kolegij { get; set; }
    public virtual ICollection<SidebarContentFile> SidebarContentFile { get; set; }
  }
}
