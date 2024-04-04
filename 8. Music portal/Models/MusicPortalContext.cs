using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace _8._Music_portal.Models
{
    public class MusicPortalContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<GenreModel> Genres { get; set; }
        public DbSet<PerformerModel> Performers { get; set; }
        public DbSet<SongsModel> Songs { get; set; }
        public MusicPortalContext(DbContextOptions<MusicPortalContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
