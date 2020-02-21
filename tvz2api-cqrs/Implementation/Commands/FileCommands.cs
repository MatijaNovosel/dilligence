using tvz2api_cqrs.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace tvz2api_cqrs.Implementation.Commands
{
  public class FileUploadCommand : ICommand<List<int>>
  {
    public FileUploadCommand() { }
    public List<IFormFile> Files { get; set; }
  }
}
