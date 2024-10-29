//Veena Harshitha Gandhe
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MoviesProject.Models
{
    public class Movie
    {
        public int MovieID { get; set; }
        [MaxLength(30, ErrorMessage = "Movie Title must be less than 30 characters")]
        [Required(ErrorMessage = "*Required")]
        public string Title { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        [Required(ErrorMessage = "*Required")]
        public DateTime ReleaseDate { get; set; }
        [Required(ErrorMessage = "*Required")]
        public string Genre { get; set; }
        public int? DirectorID { get; set; }
        public virtual Director Director { get; set; }
        public int? StreamingPlatformID { get; set; }
        public virtual StreamingPlatform StreamingPlatform { get; set; }
        public virtual ICollection<Cast> Cast { get; set;}

    }
}
