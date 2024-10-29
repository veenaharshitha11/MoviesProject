//Veena Harshitha Gandhe
using System.ComponentModel.DataAnnotations;

namespace MoviesProject.Models
{
    public class Director : Person
    {
        [Display(Name = "University Attended")]
        [MaxLength(50, ErrorMessage = "University Attended must be less than 50 characters")]
        public string UniversityAttended { get; set; }
        [Display(Name = "Degree Level")]
        public string DegreeType { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }
    }
}
