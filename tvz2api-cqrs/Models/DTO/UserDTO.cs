using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models.DTO
{
  public class UserDetailsDTO
  {
    public int Id { get; set; }
    public string Username { get; set; }
    public DateTime? Created { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Picture { get; set; }
  }

  public class BlacklistDTO
  {
    public int? CourseId { get; set; }
    public bool? Blacklisted { get; set; }
    public string Name { get; set; }
  }

  public class UserCourseDetailsDTO
  {
    public int Id { get; set; }
    public string Username { get; set; }
    public DateTime? Created { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Picture { get; set; }
    public bool Admin { get; set; }
  }

  public class UserProfilePictureDTO
  {
    public UserProfilePictureDTO() { }
    public File Picture { get; set; }
  }
}
