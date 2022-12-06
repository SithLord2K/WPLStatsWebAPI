using XWPLStats.Models;

namespace XWPLStats.Services
{
    public interface IPlayerService
    {
        Task<List<Players>> GetAllPlayersAsync();
        Task<Players> GetSinglePlayer(int id);
        Task<List<Players>> GetAllBySingleId(int id);
        Task<int> SavePlayer(Players player);
        Task RemovePlayer(int Id);
        Task<List<int>> GetDistinctPlayerId();
    }
}
