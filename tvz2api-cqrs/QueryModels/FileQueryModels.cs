using tvz2api_cqrs.Models.DTO;
using System.Collections.Generic;

namespace tvz2api_cqrs.QueryModels
{
  public class FileQueryModel
  {
    public int Id { get; set; }
    public string Naziv { get; set; }
    public string ContentType { get; set; }
    public byte[] Data { get; set; }
  }
}
