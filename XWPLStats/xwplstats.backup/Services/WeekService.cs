using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XWPLStats.Models;
using XWPLStats.Services;

[assembly:Dependency(typeof(IWeekService))]
namespace XWPLStats.Services
{
    public class WeekService : IWeekService
    {
        SQLiteAsyncConnection db;
        async Task Init()
        {
            if (db != null)
                return;

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "WPLStats.db3");
            db = new SQLiteAsyncConnection(dbPath);
            await db.CreateTableAsync<Weeks>();
        }
        public async Task<IEnumerable<Weeks>> GetAllWeeksAsync()
        {
            await Init();
            var weeks = await db.Table<Weeks>().ToListAsync();
            return weeks;
        }

        public async Task<int> SaveWeeks(Weeks weeks)
        {
            await Init();
            if (weeks.Id != 0)
                return await db.UpdateAsync(weeks);
            else
                return await db.InsertAsync(weeks);
        }

        public async Task DeleteAllWeeks()
        {

            await db.DropTableAsync<Weeks>();
            
        }
    }
}
