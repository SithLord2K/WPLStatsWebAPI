using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XWPLStats.Models;

namespace XWPLStats.Services
{
    public interface IWeekService
    {
        Task<IEnumerable<Weeks>> GetAllWeeksAsync();
        Task<int> SaveWeeks(Weeks weeks);
        Task DeleteAllWeeks();
    }
}
