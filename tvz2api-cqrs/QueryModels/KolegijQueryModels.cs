using tvz2api_cqrs.Models.DTO;
using System.Collections.Generic;

namespace tvz2api_cqrs.QueryModels
{
  public class KolegijQueryModel
  {
    public int Id { get; set; }
    public string Naziv { get; set; }
    public string Isvu { get; set; }
    public int? Ects { get; set; }
    public string Smjer { get; set; }
  }

  public class KolegijDetailsQueryModel
  {
    public int Id { get; set; }
    public string Naziv { get; set; }
    public string Isvu { get; set; }
    public int? Ects { get; set; }
    public string Smjer { get; set; }
    public List<StudentDTO> Studenti { get; set; }
    public List<SidebarContentDTO> SidebarContents { get; set; }
  }
}
