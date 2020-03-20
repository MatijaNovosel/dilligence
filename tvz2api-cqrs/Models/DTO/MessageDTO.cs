using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models.DTO
{
    public partial class MessageDTO
    {
      public int Id { get; set; }
      public string Content { get; set; }
      public int UserId { get; set; }
      public DateTime? SentAt { get; set; }
    }
}
