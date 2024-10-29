//Veena Harshitha Gandhe
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MoviesProject.Models
{
    public class StreamingPlatform
    {
        public int StreamingPlatformID { get; set; }
        [Display(Name = "Streaming Platform")]
        [Required(ErrorMessage = "*Required")]
        public string PlatformName { get;set; }
        [Display(Name = "Subscription Cost/Month(in $)")]
        [Required(ErrorMessage = "*Required")]
        public float SubscriptionCost { get; set;  }
        public virtual ICollection<Movie> Movies { get; set; }
    }
}
