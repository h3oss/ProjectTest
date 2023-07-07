using System.ComponentModel.DataAnnotations;

namespace LibraryGamesCRUD.Models
{
    public class GamesToGenres
    {
        [Key]
        public int Id_Games { get; set; }
        public int Id_Genres { get; set; }
    }
}
