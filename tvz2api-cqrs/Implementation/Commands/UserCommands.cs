using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tvz2api_cqrs.Infrastructure.Commands;
using tvz2api_cqrs.Models.DTO;

namespace tvz2api_cqrs.Implementation.Commands
{
  public class UserSubscribeCommand : ICommand
  {
    public UserSubscribeCommand() { }
    public int UserId { get; set; }
    public string Password { get; set; }
    public int CourseId { get; set; }
  }

  public class UserUnsubscribeCommand : ICommand
  {
    public UserUnsubscribeCommand() { }
    public int UserId { get; set; }
    public int CourseId { get; set; }
  }

  public class UserUpdateSettingsCommand : ICommand
  {
    public UserUpdateSettingsCommand() { }
    public int UserId { get; set; }
    public bool DarkMode { get; set; }
    public string Locale { get; set; }
  }

  public class UserUploadPictureCommand : ICommand<UserProfilePictureDTO>
  {
    public UserUploadPictureCommand() { }
    public UserUploadPictureCommand(int userId, IFormFile picture)
    {
      UserId = userId;
      Picture = picture;
    }
    public int UserId { get; set; }
    public IFormFile Picture { get; set; }
  }

  public class UserUpdatePersonalInformationCommand : ICommand
  {
    public UserUpdatePersonalInformationCommand() { }
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
  }
}