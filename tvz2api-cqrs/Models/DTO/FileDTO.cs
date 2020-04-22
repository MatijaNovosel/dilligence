using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models.DTO
{
  public class FileDTO
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string ContentType { get; set; }
    public byte[] Data { get; set; }
  }
}
