//Veena Harshitha Gandhe
namespace MoviesProject.Models
{
    public class Cast
    {
        public int CastID { get; set; }
        public string RolePlayed { get; set; }
        public int? ActorID { get; set; }
        public int? MovieID { get; set; }
        public virtual Actor Actor { get; set; }
        public virtual Movie Movie { get; set; }

    }
}
