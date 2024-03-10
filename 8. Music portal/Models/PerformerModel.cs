namespace _8._Music_portal.Models
{
    public class PerformerModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<SongsModel>? Songs { get; set; }
        public PerformerModel()
        {
            this.Songs = new HashSet<SongsModel>();
        }
    }
}
