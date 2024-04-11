using _8._Music_portal.Models;
using _8._Music_portal.NewFolder;
using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace _8._Music_portal.Repository
{
    public class GenreRepositori : IRepository<GenreModel> 
    {
        private readonly MusicPortalContext _context;
        public GenreRepositori(MusicPortalContext music) 
        {
            _context = music;
        }
        public async Task<List<GenreModel>> GetAll()
        {
            var item = await _context.Genres.ToListAsync();
            return item;
        }
        public async Task<GenreModel> Get(int id)
        {
            GenreModel? genreModel = await _context.Genres
                .FirstOrDefaultAsync(m => m.Id == id);
            return genreModel;
        }
        
        public async Task Create(GenreModel item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
        }
        public void Update(GenreModel item)
        {
            _context.Update(item);
        }
        public async Task Delete(int id)
        {
            GenreModel? genreModel = await _context.Genres
               .FirstOrDefaultAsync(m => m.Id == id);
            _context.Genres.Remove(genreModel);
            await _context.SaveChangesAsync();
        }
        public bool ModelExists(int id)
        {
            return _context.Genres.Any(e => e.Id == id);
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

    }
}
