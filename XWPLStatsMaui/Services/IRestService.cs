using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWPLStats.Models;

namespace XWPLStats.Services
{
    public interface IRestService
    {
        Task<List<Players>> GetAllPlayers();
        Task<Players> GetSinglePlayer(int id);
        Task<List<Players>> GetAllBySingleId(int id);
        Task<List<int>> GetDistinctPlayer();
        Task SavePlayer(Players player);
        Task DeletePlayer(int id);

        //Weeks

        Task<IEnumerable<Weeks>> GetAllWeeks();
        Task AddWeeks(Weeks weeks);
        Task UpdateWeeks(Weeks weeks);
        Task RemoveWeeks(Weeks weeks);
            
    }
}
