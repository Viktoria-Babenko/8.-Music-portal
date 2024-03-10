namespace _8._Music_portal.Models
{
    public class GenreModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<SongsModel>? Songs { get; set; }
        public GenreModel()
        {
            this.Songs = new HashSet<SongsModel>();
        }
    }
}
