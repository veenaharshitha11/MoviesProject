//Veena Harshitha Gandhe
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MoviesProject.Models
{
    public class Actor : Person
    {
        public string Gender { get; set; }
        [Display(Name = "Native Language")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Native Language should contain letters only")]
        public string NativeLanguage { get; set; }
        [Display(Name = "Film Industry")]
        public string FilmIndustry { get; set; }
        public virtual ICollection<Cast> Cast { get; set;}
    }
}
