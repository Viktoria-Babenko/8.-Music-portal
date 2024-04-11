using _8._Music_portal.Models;
using Microsoft.EntityFrameworkCore;

namespace _8._Music_portal.Repository
{
    public class PerformerRepositori : IRepository<PerformerModel>
    {
        private readonly MusicPortalContext _context;
        public PerformerRepositori(MusicPortalContext music)
        {
            _context = music;
        }
        public async Task<List<PerformerModel>> GetAll()
        {
            var item = await _context.Performers.ToListAsync();
            return item;
        }

        public async Task<PerformerModel> Get(int id)
        {
            PerformerModel? performerModel = await _context.Performers
                .FirstOrDefaultAsync(m => m.Id == id);
            return performerModel;
        }
        public async Task Create(PerformerModel item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
        }
        public void Update(PerformerModel item)
        {
            _context.Update(item);
        }
        public async Task Delete(int id)
        {
            PerformerModel? performerModel = await _context.Performers
               .FirstOrDefaultAsync(m => m.Id == id);
            _context.Performers.Remove(performerModel);
            await _context.SaveChangesAsync();
        }
        public bool ModelExists(int id)
        {
            return _context.Performers.Any(e => e.Id == id);
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
