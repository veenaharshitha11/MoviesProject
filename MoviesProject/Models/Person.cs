//Veena Harshitha Gandhe
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MoviesProject.Models
{
    public class Person
    {
        public int ID { get; set; }
        [Display(Name = "First Name")]
        [MaxLength(15, ErrorMessage = "First Name must be 15 characters or less")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "First Name should contain letters only")]
        [Required(ErrorMessage = "*Required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(20, ErrorMessage = "Last Name must be 20 characters or less")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Last Name should contain letters only")]
        [Required(ErrorMessage = "*Required")]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        [Required(ErrorMessage = "*Required")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Contact Number")]
        [MaxLength(11, ErrorMessage = "Enter a valid contact number")]
        [Required(ErrorMessage ="*Required")]
        public string ContactNumber { get; set; }
        [Display(Name = "Email")]
        public string EmailID { get; set; }
        [Display(Name = "Street Address")]
        [MaxLength(50, ErrorMessage = "Street Address must be less than 50 characters")]

        public string StreetAddress { get; set; }
        [MaxLength(20, ErrorMessage = "City must be less than 20 characters")]

        public string City { get; set; }
        [MaxLength(15, ErrorMessage = "State must be less than 15 characters")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "State should contain letters only")]
        public string State { get; set; }
        [Required(ErrorMessage = "*Required")]
        public int ZipCode { get; set; }
    }
}
