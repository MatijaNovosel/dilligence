using tvz2api_cqrs.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tvz2api_cqrs.Models.DTO;

namespace tvz2api_cqrs.Implementation.Commands
{
  public class SendMessageCommand : ICommand<MessageDTO>
  {
    public SendMessageCommand() { }
    public SendMessageCommand(int userId, int chatId, string content)
    {
      UserId = userId;
      ChatId = chatId;
      Content = content;
    }
    public int UserId { get; set; }
    public int ChatId { get; set; }
    public string Content { get; set; }
  }
}
