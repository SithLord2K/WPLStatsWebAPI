using System.Runtime.InteropServices;
using XWPLStats.Models;

namespace XWPLStats.Services
{
    public interface IRestService
    {
        Task<List<Players>> GetAllPlayers();
        Task<List<Players>> GetSinglePlayer(int id);
        Task<List<Players>> GetAllBySingleId(int id);
        Task<List<int>> GetDistinctPlayer();
        Task SavePlayer(Players player);
        Task DeletePlayer(int id);

        //Weeks

        Task<IEnumerable<Weeks>> GetAllWeeks([Optional]bool forceRefresh);
        Task AddWeeks(Weeks weeks);
        Task UpdateWeeks(Weeks weeks);
        Task RemoveWeeks(int id);

        //TeamDetails
        Task<List<TeamDetails>> GetTeamDetails();
            
    }
}
