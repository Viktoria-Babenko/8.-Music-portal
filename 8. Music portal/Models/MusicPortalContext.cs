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
            if (Database.EnsureCreated())
            {
                UserModel user = new UserModel();
                user.FirstName = "Виктория";
                user.LastName = "Бабенко";
                user.Login = "Admin";
                user.email = "babenko.viktoria.v@gmail.com";
                user.Password = "12E9FC7F69CE4CDD80FCD78DE721F62385768DE7EFA97EB1C3B046DD37AE2A9B";
                user.Salt = "F4A57E3EBC7C8A72CCFE77A0AB106A70";
                user.Status = true;
                Users?.Add(user); 

                GenreModel genre = new GenreModel();
                genre.Name = "POP";
                Genres?.Add(genre);

                GenreModel genre1 = new GenreModel();
                genre1.Name = "Rock and roll";
                Genres?.Add(genre1);

                PerformerModel performers = new PerformerModel();
                performers.Name = "Артем Пивоваров ";
                Performers?.Add(performers);

                SongsModel songs = new SongsModel();
                songs.Name = "Дежавю";
                songs.Genre = genre;
                songs.Performer = performers;
                songs.Track = "/Songs/Артем Пивоваров - Дежавю.mp3";
                Songs?.Add(songs);

                SongsModel songs1 = new SongsModel();
                songs1.Name = "Рандеву";
                songs1.Genre = genre;
                songs1.Performer = performers;
                songs1.Track = "/Songs/Артём Пивоваров - Рандеву.mp3";
                Songs?.Add(songs1);

                SongsModel songs2 = new SongsModel();
                songs2.Name = "Думи";
                songs2.Genre = genre;
                songs2.Performer = performers;
                songs2.Track = "/Songs/Артем Пивоваров feat. Dorofeeva - Думи.mp3";
                Songs?.Add(songs2);
                SaveChanges();
            }
        }
    }
}
