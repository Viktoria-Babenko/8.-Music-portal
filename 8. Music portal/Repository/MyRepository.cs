using _8._Music_portal.Models;
using _8._Music_portal.NewFolder;
using Microsoft.EntityFrameworkCore;

namespace _8._Music_portal.Repository
{
    public class MyRepository : IRepository
    {
        private readonly MusicPortalContext _context;

        public MyRepository(MusicPortalContext context)
        {
            _context = context;
        }
        public async Task<List<UserModel>> GetAllUser()
        {
            return await _context.Users.ToListAsync();
        }
        public IQueryable<UserModel> UserWhere(string login)
        {
            return _context.Users.Where(a => a.Login == login);
        }
        public int GetUserCount()
        {
            return _context.Users.ToList().Count;
        }
        public async Task<UserModel> GetUser(string login)
        {
            return await _context.Users.FirstOrDefaultAsync(m => m.Login == login);
        }
        public async Task<UserModel> GetUser(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task CreateUser(UserModel c)
        {
            if(c.Login != "Admin")
                c.Status = false;
            else if(c.Login == "Admin")
                c.Status = true;
            await _context.Users.AddAsync(c);
            await _context.SaveChangesAsync();
        }

        public void UpdateUser(UserModel c)
        {
            _context.Entry(c).State = EntityState.Modified;
        }

        public async Task DeleteUser(int id)
        {
            UserModel? u = await _context.Users.FindAsync(id);
            if (u != null)
                _context.Users.Remove(u);
        }
        public async Task<List<SongsModel>> GetAllSong()
        {
            var musicPortalContext = _context.Songs.Include(s => s.Genre).Include(s => s.Performer);
            return await musicPortalContext.ToListAsync();
        }

        public async Task<SongsModel> GetSong(string name)
        {
            return await _context.Songs.FirstOrDefaultAsync(m => m.Name == name);
        }
        public async Task<SongsModel> GetSong(int id)
        {
            var songsModel = await _context.Songs
               .Include(s => s.Genre)
               .Include(s => s.Performer)
               .FirstOrDefaultAsync(m => m.Id == id);
            return songsModel;
        }

        public async Task CreateSong(SongsModel c)
        {
            _context.Add(c);
            await _context.SaveChangesAsync();
        }

        public void UpdateSong(SongsModel c)
        {
            _context.Entry(c).State = EntityState.Modified;
        }

        public async Task DeleteSong(int id)
        {
            SongsModel? u = await _context.Songs.FindAsync(id);
            if (u != null)
                _context.Songs.Remove(u);
        }
        public bool UserModelExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
        public bool SongsModelExists(int id)
        {
            return _context.Songs.Any(e => e.Id == id);
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
