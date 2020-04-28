using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models.DTO
{
  public class UserDTO
  {
    public int Id { get; set; }
    public string Username { get; set; }
    public DateTime? Created { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public int? ImageFileId { get; set; }
  }

  public class UserProfilePictureDTO
  {
    public UserProfilePictureDTO() { }
    public File Picture { get; set; }
  }
}
