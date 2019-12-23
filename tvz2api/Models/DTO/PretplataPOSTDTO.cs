using System.Collections.Generic;

namespace tvz2api.Models.DTO
{
    public class PretplataPOSTDTO
    {
        public int StudentId { get; set; }
        public List<int?> PretplataIDs { get; set; }
    }
}