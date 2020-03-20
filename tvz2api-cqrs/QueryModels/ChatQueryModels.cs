using tvz2api_cqrs.Models.DTO;
using System.Collections.Generic;

namespace tvz2api_cqrs.QueryModels
{
  public class ChatQueryModel
  {
    public int Id { get; set; }
    public List<MessageDTO> Messages { get; set; }
  }
}
