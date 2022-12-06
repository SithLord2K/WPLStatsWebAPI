﻿using SQLite;
using XWPLStats.Models;

namespace XWPLStats.Services
{

    public class PlayerService : IPlayerService
    {
        SQLiteAsyncConnection db;


        async Task Init()
        {
            if (db != null)
                return;

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "WPLStats.db3");
            db = new SQLiteAsyncConnection(dbPath);
            await db.CreateTableAsync<Players>();
        }
        public async Task<List<Players>> GetAllPlayersAsync()
        {
            await Init();
            var players = await db.Table<Players>().ToListAsync();
            return players;
        }
        public async Task<List<int>> GetDistinctPlayerId()
        {
            await Init();
            var playersId = await db.Table<Players>().ToListAsync();
            return playersId.Select(x => x.Id).Distinct().ToList();
        }

        public async Task<Players> GetSinglePlayer(int id)
        {
            await Init();

            var player = await db.Table<Players>()
                .FirstOrDefaultAsync(c => c.Id == id);

            return player;
        }

        public async Task<List<Players>> GetAllBySingleId(int id)
        {
            await Init();
            var singlePlayerId = await db.Table<Players>().Where(s => s.Id == id).ToListAsync();
            return singlePlayerId;
        }

        public async Task RemovePlayer(int Id)
        {
            await Init();

            await db.DeleteAsync<Players>(Id);
        }

        public async Task<int> SavePlayer(Players player)
        {
            await Init();
            return await db.InsertAsync(player);
        }


    }
}
