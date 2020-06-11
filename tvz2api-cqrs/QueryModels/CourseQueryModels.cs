using tvz2api_cqrs.Models.DTO;
using System.Collections.Generic;

namespace tvz2api_cqrs.QueryModels
{
  public class CourseQueryModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Isvu { get; set; }
    public int? Ects { get; set; }
    public int? SpecializationId { get; set; }
    public bool Subscribed { get; set; }
  }

  public class CourseDetailsQueryModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Isvu { get; set; }
    public int? Ects { get; set; }
    public string Password { get; set; }
    public string Specialization { get; set; }
  }

  public class CourseUserGradesQueryModel
  {
    public List<CourseGradesTaskDTO> Tasks { get; set; }
    public List<CourseGradesExamDTO> Exams { get; set; }
  }
}
