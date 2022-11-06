using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XWPLStats.Models;

namespace XWPLStats.Services
{
    public interface IPlayerService
    {
        Task<IEnumerable<Players>> GetAllPlayersAsync();
        Task<Players> GetSinglePlayer(int id);
        Task<int> SavePlayer(Players player);
        Task RemovePlayer(int Id);
    }
}
