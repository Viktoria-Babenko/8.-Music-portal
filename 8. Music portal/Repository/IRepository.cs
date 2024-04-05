using _8._Music_portal.Models;

namespace _8._Music_portal.NewFolder
{
    public interface IRepository
    {
        Task<List<UserModel>> GetAllUser();
        int GetUserCount();
        IQueryable<UserModel> UserWhere(string login);
        Task<UserModel> GetUser(string login);
        Task<UserModel> GetUser(int id);
        Task CreateUser(UserModel item);
        void UpdateUser(UserModel item);
        Task DeleteUser(int id);
        Task<List<SongsModel>> GetAllSong();
        Task<SongsModel> GetSong(string login);
        Task<SongsModel> GetSong(int id);
        Task CreateSong(SongsModel item);
        void UpdateSong(SongsModel item);
        Task DeleteSong(int id);
        bool UserModelExists(int id);
        bool SongsModelExists(int id);
        Task Save();
    }
}
