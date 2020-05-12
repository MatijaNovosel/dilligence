using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace tvz2api_cqrs.Models.DTO
{
  public class SidebarContentDTO
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public List<FileDTO> Files { get; set; }
  }
}
