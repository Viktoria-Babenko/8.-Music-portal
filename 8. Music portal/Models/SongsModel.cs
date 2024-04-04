namespace _8._Music_portal.Models
{
    public class SongsModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public int? GenreID { get; set; }

        public GenreModel? Genre { get; set; }
        public int? PerformerID { get; set; }

        public PerformerModel? Performer { get; set; }
        public string? Track { get; set; }

    }
}
