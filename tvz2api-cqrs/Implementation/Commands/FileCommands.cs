using tvz2api_cqrs.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace tvz2api_cqrs.Implementation.Commands
{
  public class FileUploadCommand : ICommand<List<int>>
  {
    public FileUploadCommand(List<IFormFile> files)
    {
      Files = files;
    }
    public List<IFormFile> Files { get; set; }
  }

  public class FileDeleteCommand : ICommand
  {
    public FileDeleteCommand(int id)
    {
      Id = id;
    }
    public int Id { get; set; }
  }

  public class FileUploadSidebarCommand : ICommand<List<int>>
  {
    public FileUploadSidebarCommand(int sidebarId, List<IFormFile> files)
    {
      SidebarId = sidebarId;
      Files = files;
    }
    public int SidebarId { get; set; }
    public List<IFormFile> Files { get; set; }
  }
}
